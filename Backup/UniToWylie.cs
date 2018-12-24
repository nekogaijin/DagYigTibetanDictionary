/* File: UniToWylie.cs
 * Date: 06.10.2009
 * Desc: Unicode To Wylie character conversion. 
 * Auth: © Al Gallo 2009 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DagYig
{
    public  class UniToWylie
    {
        public  IDictionary<string, string> UWDic;
       
        public  UniToWylie()
        {
           UWDic = new Dictionary<string, string>();
           UWDic.Add("0F40","k");
          UWDic.Add("0F41","kh");
          UWDic.Add("0F42","g");
          UWDic.Add("0F43","g+h");
          UWDic.Add("0F44","ng");
          UWDic.Add("0F45","c");
          UWDic.Add("0F46","ch");
          UWDic.Add("0F47","j");
          UWDic.Add("0F49","ny");
          UWDic.Add("0F4a","T");
          UWDic.Add("0F4b","Th");
          UWDic.Add("0F4c","D");
          UWDic.Add("0F4d","D+h");
          UWDic.Add("0F4e","N");
          UWDic.Add("0F4f","t");
          UWDic.Add("0F50","th");
          UWDic.Add("0F51","d");
          UWDic.Add("0F52","d+h");
          UWDic.Add("0F53","n");
          UWDic.Add("0F54","p");
          UWDic.Add("0F55","ph");
          UWDic.Add("0F56","b");
          UWDic.Add("0F57","b+h");
          UWDic.Add("0F58","m");
          UWDic.Add("0F59","ts");
          UWDic.Add("0F5a","tsh");
          UWDic.Add("0F5b","dz");
          UWDic.Add("0F5c","dz+h");
          UWDic.Add("0F5d","w");
          UWDic.Add("0F5e","zh");
          UWDic.Add("0F5f","z");
          UWDic.Add("0F60","'");
          UWDic.Add("0F61","y");
          UWDic.Add("0F62","r");
          UWDic.Add("0F63","l");
          UWDic.Add("0F64","sh");
          UWDic.Add("0F65","Sh");
          UWDic.Add("0F66","s");
          UWDic.Add("0F67","h");
          UWDic.Add("0F68","a");
          UWDic.Add("0F69","k+Sh");
          UWDic.Add("0F6a","R");
          //     our %tib_subjoined = (
          UWDic.Add("0F90","k");
          UWDic.Add("0F91","kh");
          UWDic.Add("0F92","g");
          UWDic.Add("0F93","g+h");
          UWDic.Add("0F94","ng");
          UWDic.Add("0F95","c");
          UWDic.Add("0F96","ch");
          UWDic.Add("0F97","j");
          UWDic.Add("0F99","ny");
          UWDic.Add("0F9a","T");
          UWDic.Add("0F9b","Th");
          UWDic.Add("0F9c","D");
          UWDic.Add("0F9d","D+h");
          UWDic.Add("0F9e","N");
          UWDic.Add("0F9f","t");
          UWDic.Add("0Fa0","th");
          UWDic.Add("0Fa1","d");
          UWDic.Add("0Fa2","d+h");
          UWDic.Add("0Fa3","n");
          UWDic.Add("0Fa4","p");
          UWDic.Add("0Fa5","ph");
          UWDic.Add("0Fa6","b");
          UWDic.Add("0Fa7","b+h");
          UWDic.Add("0Fa8","m");
          UWDic.Add("0Fa9","ts");
          UWDic.Add("0Faa","tsh");
          UWDic.Add("0Fab","dz");
          UWDic.Add("0Fac","dz+h");
          UWDic.Add("0Fad","w");
          UWDic.Add("0Fae","zh");
          UWDic.Add("0Faf","z");
          UWDic.Add("0Fb0","'");
          UWDic.Add("0Fb1","y");
          UWDic.Add("0Fb2","r");
          UWDic.Add("0Fb3","l");
          UWDic.Add("0Fb4","sh");
          UWDic.Add("0Fb5","Sh");
          UWDic.Add("0Fb6","s");
          UWDic.Add("0Fb7","h");
          UWDic.Add("0Fb8","a");
          UWDic.Add("0Fb9","k+Sh");
          UWDic.Add("0Fba","W");
          UWDic.Add("0Fbb","Y");
          UWDic.Add("0Fbc","R");
       
          UWDic.Add("0F71","A");
          UWDic.Add("0F72","i");
          UWDic.Add("0F73","I");
          UWDic.Add("0F74","u");
          UWDic.Add("0F75","U");
          UWDic.Add("0F7a","e");
          UWDic.Add("0F7b","ai");
          UWDic.Add("0F7c","o");
          UWDic.Add("0F7d","au");
          UWDic.Add("0F80","-i");


          UWDic.Add("0F35","~X");
          UWDic.Add("0F37","X");
          UWDic.Add("0F39","^");
          UWDic.Add("0F7e","M");
          UWDic.Add("0F7f", "H");
          UWDic.Add("0F82","~M`");
          UWDic.Add("0F83","~M");  
          UWDic.Add("0F84","?"); 
  
          UWDic.Add("a0","_");
          UWDic.Add("0F04","@");
          UWDic.Add("0F05","#");
          UWDic.Add("0F06","$");
          UWDic.Add("0F07","%");
          UWDic.Add("0F08","!");
          UWDic.Add("0F0b"," ");
          UWDic.Add("0F0c","*");
          UWDic.Add("0F0d","/");
          UWDic.Add("0F0e","//");
          UWDic.Add("0F0F",";");
          UWDic.Add("0F11","|");
          UWDic.Add("0F14",":");
          UWDic.Add("0F20","0");
          UWDic.Add("0F21","1");
          UWDic.Add("0F22","2");
          UWDic.Add("0F23","3");
          UWDic.Add("0F24","4");
          UWDic.Add("0F25","5");
          UWDic.Add("0F26","6");
          UWDic.Add("0F27","7");
          UWDic.Add("0F28","8");
          UWDic.Add("0F29","9");
          UWDic.Add("0F34","=");
          UWDic.Add("0F3a","<");
          UWDic.Add("0F3b",">");
          UWDic.Add("0F3c","(");
          UWDic.Add("0F3d",")");
        }

        public string Convert(string uni)
        {
            string wylie = "";
            UWDic.TryGetValue(uni, out wylie);
            return wylie;
        }
        public string DicContains(string uni)
        {
            if (UWDic.ContainsKey(uni))
            {
                return UWDic[uni].ToString();
            }
            return "";
        }
    }
}
