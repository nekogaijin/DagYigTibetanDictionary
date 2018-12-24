/* File: ClipBoardPreference.cs
 * Date: 12.10.2010
 * Desc: 
 * Auth: © Al Gallo 2009 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace DagYig
{
    [Serializable]
    class ClipBoardPreference
    {
        internal string font;
        public string Font
        {
            get { return font; }
            set { font = value; }
        }

        internal float fontSize;
        public float FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }

        internal string windowSize;
        public string WindowSize
        {
            get { return windowSize; }
            set { windowSize = value; }
        }

        public void SavePreference()
        {
            ClipBoardPreference cp = new ClipBoardPreference();

            XmlSerializer mySerializer = new XmlSerializer(typeof(ClipBoardPreference));
            StreamWriter myWriter = new StreamWriter("d:/prefs.xml");
            mySerializer.Serialize(myWriter, cp);
            myWriter.Close();
        }

        public void GetPreferences()
        {
            ClipBoardPreference cp;

            XmlSerializer mySerializer = new XmlSerializer(typeof(ClipBoardPreference));
            FileStream myFileStream = new FileStream("d:/prefs.xml", FileMode.Open);

            cp = (ClipBoardPreference)mySerializer.Deserialize(myFileStream);
        }
    }
}
