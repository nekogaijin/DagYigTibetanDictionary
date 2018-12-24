/* File: frmDagYig.cs
 * Date: 12.26.2010
 * Desc: Tibetan Dictionary 
 * Auth: © Al Gallo 2009 */
using System;
using System.IO;

namespace DagYig
{
    

    public static class ErrorLog
    {
        public static string strLogFile = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath + "\\DagYigLogFile.txt";

        public static void WriteLog(string data)
        {
            try
            {
                if (!File.Exists(strLogFile))
                {
                    using (StreamWriter sw = File.CreateText(strLogFile))
                    {
                        sw.WriteLine(data);

                    }

                }
                else
                {
                    FileInfo f = new FileInfo(strLogFile);
                    if (f.Length > 1048576)
                    {
                        try
                        {
                            f.Delete();
                            using (StreamWriter sw = File.CreateText(strLogFile))
                            {
                                sw.WriteLine(data);

                            }
                        }
                        catch { }
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(strLogFile))
                        {
                            sw.WriteLine(data);
                        }
                    }

                    f = null;
                }
            }
            catch (Exception wlfExc)
            {
               // MessageBox.Show("Error writing to log file " + wlfExc);
            }

        }
    }
}
