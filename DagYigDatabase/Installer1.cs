using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data.SqlClient;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Text;

namespace DagYigDatabase
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        
        string DYLogFile = "";

        public Installer1()
        {
            InitializeComponent();
            SetLogFilePath();
        }

        
        
        public void RestoreDatabase(String databaseName, String filePath, String serverName,
            String userName, String password, String dataFilePath, String logFilePath)
        {

            WriteLogFile("Start DB Restore: " + DateTime.Now.ToString());
            WriteLogFile("dataFilePath: " + dataFilePath + " logFilePath " + logFilePath); 
            // Create Restore instance
            Restore sqlRestore = new Restore();

            // Point to database
            BackupDeviceItem deviceItem = new BackupDeviceItem(filePath, DeviceType.File);
            sqlRestore.Devices.Add(deviceItem);
            sqlRestore.Database = databaseName;

            // Connect to DB Server
            ServerConnection connection;

            if (userName == "") // for Windows Authentication
            {
                SqlConnection sqlCon = new SqlConnection(@"Data Source=" + serverName + @"; 
                    Integrated Security=True;");
                connection = new ServerConnection(sqlCon);
            }
            else // for Server Authentication
                connection = new ServerConnection(serverName, userName, password);

            // Restoring
            Server sqlServer = new Server(connection);
            Database db = sqlServer.Databases[databaseName];
            sqlRestore.Action = RestoreActionType.Database;
            String dataFileLocation = dataFilePath + databaseName + ".mdf";
            String logFileLocation = logFilePath + databaseName + "_log.ldf";
            db = sqlServer.Databases[databaseName];
            RelocateFile rf = new RelocateFile(databaseName, dataFileLocation);
            sqlRestore.RelocateFiles.Add(new RelocateFile(databaseName + "", dataFileLocation));
            sqlRestore.RelocateFiles.Add(new RelocateFile(databaseName + "_log", logFileLocation));
            sqlRestore.ReplaceDatabase = true;
            sqlRestore.PercentCompleteNotification = 10;
            sqlRestore.PercentComplete += new PercentCompleteEventHandler(myRestore_PercentComplete);
	        sqlRestore.Complete += new ServerMessageEventHandler(myRestore_Complete);

            try
            {
                sqlRestore.SqlRestore(sqlServer);
                db = sqlServer.Databases[databaseName];
                db.SetOnline();
            }
            catch (Exception ex)
            {
                WriteLogFile("error on restore " + ex.Message);
                MessageBox.Show("Error occurred on db restore. LogFile: " + DYLogFile);
            }
          
            sqlServer.Refresh();

        }

       

        static void myRestore_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
           // WriteLogFile(e.Percent.ToString() + "% Complete");
        }

        void myRestore_Complete(object sender, Microsoft.SqlServer.Management.Common.ServerMessageEventArgs e)
        {
            WriteLogFile("Restore of Database completed");
            RemoveFiles();
        }

        public override void Commit(System.Collections.IDictionary savedState)
        {
            
            try
            {
                DirectorySecurity dirSec = Directory.GetAccessControl(Context.Parameters["TargetDir"]);
                FileSystemAccessRule fsar = new FileSystemAccessRule
                (@"NT AUTHORITY\NETWORK SERVICE"
                , FileSystemRights.FullControl
                , InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit
                , PropagationFlags.None
                , AccessControlType.Allow);
                dirSec.AddAccessRule(fsar);
                Directory.SetAccessControl(Context.Parameters["TargetDir"], dirSec);
            }
            catch (Exception ex)
            {
                WriteLogFile("error on setting access control " + ex.Message);
                MessageBox.Show("Error occurred on Setting Access Control for DB Restore. LogFile: " + DYLogFile);
            }

            // Parameters from setup project  
            RestoreDatabase(Context.Parameters["databaseName"].ToString(),
                Context.Parameters["filePath"].ToString(), Context.Parameters
            ["serverName"].ToString(), Context.Parameters["userName"].ToString(),
                Context.Parameters["password"].ToString(), Context.Parameters
            ["dataFilePath"].ToString(), Context.Parameters["logFilePath"].ToString());


             base.Commit(savedState);
        }


      


        public override void Uninstall(IDictionary savedState)
        {
            WriteLogFile("Begin uninstall" + DateTime.Now.ToString()); 
            try
            {
                DirectorySecurity dirSec = Directory.GetAccessControl(Context.Parameters["TargetDir"].ToString());
                FileSystemAccessRule fsar = new FileSystemAccessRule(@"NT AUTHORITY\SERVICE"
                                              , FileSystemRights.FullControl
                                              , InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit
                                              , PropagationFlags.None
                                              , AccessControlType.Allow);
                dirSec.AddAccessRule(fsar);
                Directory.SetAccessControl(Context.Parameters["TargetDir"].ToString(), dirSec);
            }
            catch (Exception uinExc)
            {

                WriteLogFile("uininstall error on setting access control " + uinExc.Message);
                MessageBox.Show("Error occurred on Setting Access Control for Uninstall. LogFile: " + DYLogFile);
            }
           
            // Connect to DB Server
            ServerConnection connection;

            if (Context.Parameters["userName"].ToString() == "") // for Windows Authentication
            {
                SqlConnection sqlCon = new SqlConnection(@"Data Source=" + Context.Parameters
            ["serverName"].ToString() + @"; 
Integrated Security=True;");
                connection = new ServerConnection(sqlCon);
            }
            else // for Server Authentication
                connection = new ServerConnection(Context.Parameters
            ["serverName"].ToString(), Context.Parameters["userName"].ToString(), Context.Parameters["password"].ToString());

            string databaseName = Context.Parameters["databaseName"].ToString();
            Server sqlServer = new Server(connection);
            try
            {
                sqlServer.KillAllProcesses(databaseName);
                sqlServer.Databases[databaseName].SetOffline();
                sqlServer.Databases[databaseName].SetOnline();
                sqlServer.DetachDatabase(databaseName, false);
            }
            catch (Exception uninstexca)
            {
                WriteLogFile("uininstall error detaching database " + uninstexca.Message);
                MessageBox.Show("Uninstall Error occurred detaching database. LogFile: " + DYLogFile);
            }

            String dataFileLocation = Context.Parameters["dataFilePath"].ToString() + Context.Parameters["databaseName"].ToString() + "_data.mdf";
            String logFileLocation = Context.Parameters["logFilePath"].ToString() + Context.Parameters["databaseName"].ToString() + "_log.ldf";

            String setupLocation = Context.Parameters["TargetDir"];

            WriteLogFile ("Uninstall: deleting db files");

            try
            {
                File.Delete(dataFileLocation);
                File.Delete(logFileLocation);
                // Get the new file foler

                WriteLogFile("Deleting program files");

                DirectoryInfo dr = new DirectoryInfo(setupLocation);
              
                foreach (FileInfo tempfiles in dr.GetFiles("*.*"))
                {
                    try
                    {
                        WriteLogFile(tempfiles.Name);
                        File.Delete(tempfiles.Name);
                     

                    }
                    catch (Exception e)
                    {
                        WriteLogFile("Uninstall: deleting program files " + e.Message);
                        
                    }

                }

            }

            catch (Exception ex)
            {
                WriteLogFile( "Uninstall: deleting files: " + ex.Message);
                MessageBox.Show("Uninstall Error occurred deleting DagYig program files. LogFile: " + DYLogFile);
            }

            try
            {
                WriteLogFile("Uninstall: deleting directory: " + setupLocation);
                Directory.Delete(setupLocation, true);
            }
            catch (Exception ee)
            {
                WriteLogFile("Uninstall Error: deleting directory: " + ee.Message);
              //  MessageBox.Show("Uninstall Error occurred deleting DagYig Directory. LogFile: " + DYLogFile);
            }

            WriteLogFile("Uninstall Finished");
            base.Uninstall(savedState);

        }
        
        
        #region helpers
        
        private void SetLogFilePath()
        {
            string TempDir = System.IO.Path.GetTempPath();
            DirectorySecurity tdirSec = Directory.GetAccessControl(TempDir);
            FileSystemAccessRule tfsar = new FileSystemAccessRule(@"NT AUTHORITY\SERVICE"
                                          , FileSystemRights.FullControl
                                          , InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit
                                          , PropagationFlags.None
                                          , AccessControlType.Allow);
            tdirSec.AddAccessRule(tfsar);
            Directory.SetAccessControl(TempDir, tdirSec);
            DYLogFile = TempDir + "\\" + "DagYigInstall.log";
        }
        private void WriteLogFile( string data)
        {
            try
            {
                if (!File.Exists(DYLogFile))
                {
                    using (StreamWriter sw = File.CreateText(DYLogFile))
                    {
                        sw.WriteLine(data);

                    }

                }
                else
                {
                    using (StreamWriter sw = File.AppendText(DYLogFile))
                    {
                        sw.WriteLine(data);
                    }
                }
            }
            catch (Exception wlfExc)
            {
                MessageBox.Show("Error writing to log file " + wlfExc);
            }

        }
        
        private void  RemoveFiles()
        {
            WriteLogFile("Begin install cleanup" + DateTime.Now.ToString());
            try
            {
                DirectorySecurity dirSec = Directory.GetAccessControl(Context.Parameters["TargetDir"].ToString());
                FileSystemAccessRule fsar = new FileSystemAccessRule(@"NT AUTHORITY\SERVICE"
                                              , FileSystemRights.FullControl
                                              , InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit
                                              , PropagationFlags.None
                                              , AccessControlType.Allow);
                dirSec.AddAccessRule(fsar);
                Directory.SetAccessControl(Context.Parameters["TargetDir"].ToString(), dirSec);
            }
            catch (Exception ex)
            {
                WriteLogFile("cleanup error on setting access control " + ex.Message);
                MessageBox.Show("Error occurred on Setting Access Control for DB Restore. LogFile: " + DYLogFile);
            }
            String setupLocation = Context.Parameters["TargetDir"] + "Setup";

            DirectoryInfo dr = new DirectoryInfo(setupLocation);
        
            foreach (FileInfo tempfiles in dr.GetFiles("*.*"))
            {
                try
                {
                    WriteLogFile(tempfiles.Name);
                    File.Delete(tempfiles.Name);

                }
                catch (Exception e)
                {
                    WriteLogFile("Error Deleting set up  files. " + e.Message);
                    MessageBox.Show("Error Deleting set up  files. All is probably still well.  LogFile: " + DYLogFile);
                }

            }
            try
            {
                Directory.Delete(setupLocation, true);
            }
            catch (Exception ee)
            {
                WriteLogFile("Error Deleting set up  directory. " + ee.Message);
            
                // MessageBox.Show(ee.Message);
            }

            WriteLogFile("Setup Cleanup Finished");

        }
        #endregion


        #region save
      
        //protected override void OnAfterInstall(System.Collections.IDictionary savedState)
        //{

        //    base.OnAfterInstall(savedState);

        //    // Add steps to be done after the installation is over.

        //    RemoveFiles();

        //}

        //private void RemoveFiles()
        //{

        //    DirectorySecurity dirSec = Directory.GetAccessControl(Context.Parameters["TargetDir"].ToString());
        //    FileSystemAccessRule fsar = new FileSystemAccessRule(@"NT AUTHORITY\SERVICE"
        //                                  , FileSystemRights.FullControl
        //                                  , InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit
        //                                  , PropagationFlags.None
        //                                  , AccessControlType.Allow);
        //    dirSec.AddAccessRule(fsar);
        //    Directory.SetAccessControl(Context.Parameters["TargetDir"].ToString(), dirSec);

        //    String setupLocation = Context.Parameters["TargetDir"] + "Setup";

        //    DirectoryInfo dr = new DirectoryInfo(setupLocation);



        //    System.Windows.Forms.MessageBox.Show(setupLocation); //even user can change the install path u get the right path.

        //     foreach (FileInfo tempfiles in dr.GetFiles("*.*"))
        //     {
        //          try
        //            {
        //                System.Windows.Forms.MessageBox.Show(tempfiles.Name);
        //                 File.Delete(tempfiles.Name);

        //             }
        //            catch (Exception e)
        //          {
        //              MessageBox.Show(e.Message);
        //          }

        //     }
        //     try
        //     {
        //         Directory.Delete(setupLocation, true);
        //     }
        //     catch (Exception ee)
        //     {
        //         MessageBox.Show(ee.Message);
        //     }



        //}
        #endregion
    }
}
