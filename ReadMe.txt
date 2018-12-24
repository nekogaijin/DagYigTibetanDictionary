README DAG YIG Version 1.0

This is an application which I created for my own use in order to help me with my Tibetan Language studies. I am a web developer, primarily coding back end processes to retrieve home valuations or credit scores, so creating an install program for windows was a new challenge. Any issues, please contact me at nekogaijin@gmail.com.

WebSite TibetanLanguage.BlogSpot.com

Files included with this install:
DagYig Database package for installing the DagYig database.
DagYig.exe which contains a clipboard scanner for dictionary lookups, and a front end to the dictionary.
DagYig database files
License.Rtf
ReadMe.Rtf
DagYigHelpFile.pdf

The DagYigDatabase.dll in the setup directory is needed only for the initial install. If the setup did not remove it, feel free to delete the setup folder.  Log files for errors during install or run  will be found in the users folder  users\yourname\

Prerequisites which will be installed if they are not on your system:
WindowsInstaller
.Net 4.0
SqlExpress

This database will run on SqlServer. If you already have a Sql Server with a named instance, and the app is not able to connect to your sql server,  there is an entry for the database connection string in the application config file named DagYig.exe.config   located in the application directory.

 <appSettings>
      <add key="ConnectionString"  value="Server=.\sqlexpress;Database=DagYig;Trusted_Connection=True"/> 
    </appSettings>

Make a backup copy of the file, and then change the ConnectionString key Server= to your sql server name. Save and restart the application.

You may write to the database, adding your own entries.. however, if you reinstall the application, these entries will be overwritten.  In a future release I hope to give you the ability to backup your database before reinstalling.

Foxfire has an AutoCopy  Add on that enables you to copy a word to the clipboard by selecting text.
https://addons.mozilla.org/en-US/firefox/addon/383/

I recommend Jomolhari Font created by Chris Fynn and made available by him for free.
http://sites.google.com/site/chrisfynn2/fonts/jomolhari

DagYig may be uninstalled from the Add Remove Programs in Windows Control Panel.

 Some of the data for the dictionary belongs to Jim Valby jimvalby@gmail.com and was used with his permission.
    Other data was taken from various glossaries Rangjung Yeshe (Erik Pema Kunsang), Ives Waldo, Jeffery Hopkins and Richard Barron culled from the Ry Dic file.
    Other data came from the Star Dict tibetan dictionary thanks to  DigitalTibetan.org  
    Other data came from various translations I have read.


                Thanks to Johannes Nestler for the Keyboard Hook code.
	Ross Donald for the ClipBoard code.
