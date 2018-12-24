/* File: WylieToTib.cs
 * Date: 06.10.2009
 * Desc: Wylie to Tibetan Unicode conversion. 
 * Auth: © Al Gallo 2009 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DagYig
{
    public class WylieToTib
    {
        string ds = ""; //debug string
        private string SUB = "rylw";
        private string[] SUB_UNI = { "\u0FB2", "\u0FB1", "\u0FB3", "\u0FAD" };
        private string[] SUB_UNIDebug = { "0FB2", "0FB1", "0FB3", "0FAD" };
        // long-to-short to avoid ambiguity in matching
        private string[] MAINS = { "k+Sh", "dz+h", "g+h", "d+h", "b+h", "D+h", "tsh", "M^", "Sh", "Th", "kh", "ng", "ch", "ny", "th", "ph", "ts", "dz", "zh", "~^", "sh", "k", "g", "c", "j", "t", "d", "n", "p", "b", "m", "w", "z", "'", "y", "r", "l", "s", "h", "T", "D", "N", "H", "M", "~", "?", "&", "." };
        private string[] MAINS_UNI = { "\u0F69", "\u0F5C", "\u0F43", "\u0F52", "\u0F57", "\u0F4D", "\u0F5A", "\u0F83", "\u0F65", "\u0F4B", "\u0F41", "\u0F44", "\u0F46", "\u0F49", "\u0F50", "\u0F55", "\u0F59", "\u0F5B", "\u0F5E", "\u0F82", "\u0F64", "\u0F40", "\u0F42", "\u0F45", "\u0F47", "\u0F4F", "\u0F51", "\u0F53", "\u0F54", "\u0F56", "\u0F58", "\u0F5D", "\u0F5F", "\u0F60", "\u0F61", "\u0F62", "\u0F63", "\u0F66", "\u0F67", "\u0F4A", "\u0F4C", "\u0F4E", "\u0F7F", "\u0F7E", "\u0F82", "\u0F84", "\u0F85", "" };
        private string[] MAINS_UNIDebug = { "0F69", "0F5C", "0F43", "0F52", "0F57", "0F4D", "0F5A", "0F83", "0F65", "0F4B", "0F41", "0F44", "0F46", "0F49", "0F50", "0F55", "0F59", "0F5B", "0F5E", "0F82", "0F64", "0F40", "0F42", "0F45", "0F47", "0F4F", "0F51", "0F53", "0F54", "0F56", "0F58", "0F5D", "0F5F", "0F60", "0F61", "0F62", "0F63", "0F66", "0F67", "0F4A", "0F4C", "0F4E", "0F7F", "0F7E", "0F82", "0F84", "0F85", "" };


        private string[] MAINS_SUB = { "\u0FB9", "\u0FAC", "\u0F93", "\u0FA2", "\u0FA7", "\u0F9D", "\u0FAA", "", "\u0FB9", "\u0F9B", "\u0F91", "\u0F94", "\u0F96", "\u0F99", "\u0FA0", "\u0FA5", "\u0FA9", "\u0FAB", "\u0FAE", "", "\u0FB4", "\u0F90", "\u0F92", "\u0F95", "\u0F97", "\u0F9F", "\u0FA1", "\u0FA3", "\u0FA4", "\u0FA6", "\u0FA8", "\u0FAD", "\u0FAF", "\u0F71", "\u0FB1", "\u0FB2", "\u0FB3", "\u0FB6", "\u0FB7", "\u0F9A", "\u0F9C", "\u0F9E", "", "", "", "\u0F9E", "\u0FB9", "" };
        private string[] MAINS_SUBDebug = { "0FB9", "0FAC", "0F93", "0FA2", "0FA7", "0F9D", "0FAA", "", "0FB9", "0F9B", "0F91", "0F94", "0F96", "0F99", "0FA0", "0FA5", "0FA9", "0FAB", "0FAE", "", "0FB4", "0F90", "0F92", "0F95", "0F97", "0F9F", "0FA1", "0FA3", "0FA4", "0FA6", "0FA8", "0FAD", "0FAF", "0F71", "0FB1", "0FB2", "0FB3", "0FB6", "0FB7", "0F9A", "0F9C", "0F9E", "", "", "", "0F9E", "0FB9", "" };

        private string SUPER = "rls";

        private string[] VOWELS = { "i", "u", "e", "o", "I", "U", "ai", "au", "A", "-i", "-I", "a" };
        private string[] VOWELS_UNI = { "\u0F72", "\u0F74", "\u0F7A", "\u0F7C", "\u0F73", "\u0F75", "\u0F7B", "\u0F7D", "\u0F71", "\u0F80", "\u0F81", "" };
        private string[] VOWELS_UNIDebug = { "0F72", "0F74", "0F7A", "0F7C", "0F73", "0F75", "0F7B", "0F7D", "0F71", "0F80", "0F81", "" };

        // these characters are the only ones that make up syllables when clumped together
        private string SYLLABLE_CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.'+^&~?-";
        private int PlusSign = 0;
        private bool blnBeenPlus = false; // has already hit a plus sign and gained a super

      
        
        public string ConvertWord(string input)
        {
        
            string result = Trans(input + " ");

           // result.Replace(" ", "");

            return result;
	    }

        public string Trans(String source)
        {
            // TODO - change this to array
            // loop through string. If there's a blank or NON Syllable char
            // then check that you've got data to process in the syllable string
            // and if so, process as a block
            StringBuilder syllable = new StringBuilder(); // holds the source chars
            StringBuilder result = new StringBuilder(); // holds the Unicode result

            // find out if this bit of text contains plus signs (complex stacking)
            PlusSign = CharacterOccurance(source, '+');

            for (int i = 0; i < source.Length; i++)
            {
                char c = Convert.ToChar(source.Substring(i, 1));

                if (SYLLABLE_CHARS.IndexOf(c) == -1) // if we have data in syllable string, process it
                {
                    if (syllable.Length > 0)
                    {
                        result.Append(ProcessSyllable(syllable.ToString()));
                        syllable.Remove(0, syllable.Length); // clear out this block in preparation for the next one
                        if (c == ' ')
                        {
                            result.Append("\u0F0B"); //tsheg
                        }
                        else
                        {
                            result.Append(c);
                        }
                    }
                    else // syllable is empty so throw this non syll char into the result
                    {
                        result.Append(c);
                    }
                }
                else // valid syllable char so throw in to syllable for next process
                {
                    syllable.Append(c);
                }
            }

            // we've still got stuff
            if (syllable.Length > 0)
            {
                result.Append(ProcessSyllable(syllable.ToString()));
            }
            //  return result.ToString();
            return ProcessPunctuation(result.ToString());
        }

        private string ProcessSyllable(string s)
        {
            ds += " ProcessSyllable String " + s + Environment.NewLine;
            //get these diff chars out of way.
            // TODO create way for user to enter special stacks that don't work
            // store in xml
            if (s.Equals("oM"))
            {
                return "\u0F00";
            }
            if (s.Equals("hRI") || s.Equals("hrI"))
            {
                return "\u0F67" + "\u0F77";

            }

            StringBuilder carryPS = new StringBuilder();
            // scan for vowels repeatedly until none are left.
            StringBuilder nonVowels = new StringBuilder();
            int vowelAt = 99;
            while (s.Length > 0)
            {

                vowelAt = -1;
                for (int v = 0; v < VOWELS.Length && vowelAt == -1; v++)
                {

                    if (s.StartsWith(VOWELS[v]))
                    {
                        vowelAt = v;
                        //ds += VOWELS_UNIDebug[v];
                    }
                }

                if (vowelAt == -1) // not a vowel. Add it to NonVowel string for holding
                {
                    nonVowels.Append(Convert.ToChar(s.Substring(0, 1)));
                    s = s.Substring(1); // shorten the string by 1 char
                }
                else  // there is a vowel append the non and vowel
                {
                    carryPS.Append(ProcessMainSubSup(nonVowels.ToString())).Append(VOWELS_UNI[vowelAt]);
                    // shortent the string by the length of the vowel 
                    //(could be u (1char) or ai (2chars)
                    s = s.Substring(VOWELS[vowelAt].Length);
                    nonVowels.Remove(0, nonVowels.Length); // we've processed a nonVowel/vowel pair so clear out this section
                }
            }
            // left with non vowels with no partnered vowels. Process them.
            if (nonVowels.Length > 0)
            {
                carryPS.Append(ProcessNormal(nonVowels.ToString()));
            }
            // add chars to the right of the last vowel (if any)
            return carryPS.ToString(); // send our tibetan string back to trans

        }

        // break down into main sub super
        private string ProcessMainSubSup(string s)
        {
            ds += " ProcessMainSubSup string: " + s + Environment.NewLine;
            if (s.Length == 0)
            {
                return "\u0F68"; // tsheg
            }
            StringBuilder carryPM = new StringBuilder();
            // filter out dots (g.ya vs. gya) so they aren't appended as a Main/Sub
            // filter out plus - we are looking for main/sub pairs
            // we are looking for pairs of main/sub so only look at s.length>1
            if (s.Length > 1)
            {
                // grab the 2 far right chars 
                char pre = Convert.ToChar(s.Substring(s.Length - 2, 1));
                char c = Convert.ToChar(s.Substring(s.Length - 1, 1));
                String end = s.Substring(s.Length - 2);

                int i = SUB.IndexOf(c); //rylw
                if (i > -1 && pre != '.' && pre != '+' && (!end.Equals("ny")))
                {
                    // chop off far right char which would be the sub (rylw)
                    s = s.Substring(0, s.Length - 1);
                    // append the unicode for sub rylw
                    carryPM.Append(SUB_UNI[i]);
                    ds += SUB_UNIDebug[i] + " ";
                }
            }
            // if we found subs, then main character should be directly to the left of what we cut off
            // if we didn't find subs, main char is where it always was, no cutting needed
            String main = "";
            int mainIdx = -1;
            if (s.Length > 0)
            {
                for (int i = 0; i < MAINS.Length && mainIdx == -1; i++)
                {
                    if (s.EndsWith(MAINS[i])) // if we found a main for the sub
                    {
                        mainIdx = i;
                        //take away number of chars = to length of main
                        s = s.Substring(0, s.Length - MAINS[i].Length);
                    }
                }
            }
            // we found a main. Let's see if there's a super
            // super should be to left of main
            if (mainIdx > -1)
            {
                main = MAINS_UNI[mainIdx]; // get the unicode of the char
                String sup = "";
                if (s.Length > 0)
                {
                    // get the char next to last
                    char c = Convert.ToChar(s.Substring(s.Length - 1, 1));
                    // is it in list of supers? rls
                    int i = SUPER.IndexOf(c);
                    // if we have a super and a main sub with the same char as main exists 
                    // (if not valid, then it won't have an entry - entry will be replaced by "")
                    // if it doesn't exist, it wasn't valid, so the super isn't really a super

                    if (i > -1 && MAINS_SUB[mainIdx].Length > 0)
                    { // the main is a legal sub, so let's grab the mainSub unicode 
                        // and find the super unicode for this super char
                        main = MAINS_SUB[mainIdx];
                        s = s.Substring(0, s.Length - 1);// chop off the super length
                        sup = ProcessSuper(c.ToString());
                        ds += MAINS_SUBDebug[mainIdx] + "  ";
                    }
                }
                // now look for special case of using plus sign to force super
                // if we don't have a super, and we have at least 2 chars
                // and we have a main that can be a valid SUB 
                if (sup.Length == 0 && s.EndsWith("+") && s.Length >= 2 && MAINS_SUB[mainIdx].Length > 0)
                {
                    blnBeenPlus = true; // in the event of multiple plusses - we want the following plusses
                    // to be subs not supers.

                    main = MAINS_SUB[mainIdx];
                    ds += MAINS_SUBDebug[mainIdx] + " ";
                    // grab char to left of the plus and test for super
                    sup = ProcessSuper(s.Substring(0, s.IndexOf("+")));

                    // sup = xscribeSuper(s.Substring(s.IndexOf("+"), s.Length - 1));
                    // CHECK!!:
                    //  chnaged   s = s.Substring(0, s.Length - (s.IndexOf("+") + 1));

                    // chop off anything up to one char past the right of the plus
                    s = s.Substring(s.IndexOf("+") + 1);
                    // let's populate our return with sup and main
                    carryPM.Insert(0, sup + main);

                    //added 12/16
                    // let's say we have multiple plusses - complex stacks
                    // gats+tshagats+tshasitset+kAn+tapan+thAnaH
                    //གཙྪགཙྪསིཙེཏྐཱནྟཔནྠཱནཿ་
                    if (PlusSign > 1)
                    {
                        // try it here!! CHECK:

                        string complex = ComplexStack(s);
                        //changd result.Insert(0, yadda);
                        //CHANGED 12/16
                        carryPM.Append(complex);

                        // ComplexStack will loop through all remaining stacks
                        // so let's pull out the index of the lastplus in the string
                        int lastplus = s.LastIndexOf("+");

                        // the our string is > than 1 more than the plus,
                        // we can chop, if it's not, we set to ""
                        if (lastplus + 1 > s.Length)
                        {
                            s = "";

                        }
                        else
                        {
                            s = s.Substring(s.LastIndexOf("+") + 1);
                        }
                    }

                    // end add
                    // s = s.Substring(0, s.Length - 2);// orig
                    //  s = s.Substring(s.IndexOf("+"), s.Length - 1);
                    // s = s.Substring(0, s.IndexOf("+"));
                }
                // no plusses, just return our sup and main
                else
                {
                    carryPM.Insert(0, sup + main);
                }
            }
            else
            {
                //+?
                //***************************************
                //***************************************
                //al add
                if (s.EndsWith("+"))
                {

                    String sup = "";
                    // main = MAINS_SUB[mainIdx];
                    //int plusCount = CharacterOccurance(s, '+');
                    if (!blnBeenPlus)
                    {
                        sup = ProcessSuper(s.Substring(0, s.IndexOf("+")));
                        // sup = xscribeSuper(s.Substring(s.IndexOf("+"), s.Length - 1));
                        //in+d+ra
                        PlusSign--;
                        carryPM.Insert(0, sup);
                        blnBeenPlus = true; // we are setting all following plusses to subs, 
                        //if we don't set blnBeenPlus to false, they will
                        // be set to supers. This is a quick fix, 
                        // needs to be redone
                    }

                    int plus = s.IndexOf("+");

                    // if our string is > than 1 more than the plus,
                    // we can chop, if it's not, we set to ""
                    // do we use this?:
                    //if (plus + 1 > s.Length)
                    //{
                    //    s = "";

                    //}
                    //else
                    //{
                    //    s = s.Substring(s.LastIndexOf("+") + 1);
                    //}
                    // or this?:
                    s = s.Substring(0, s.Length - (s.IndexOf("+") + 1));


                }
                // end add 
                //***************************************
                //***************************************

            }
            // everything left over gets done as whole characters
            // but check again for complex stacks
            if (s.Length > 0)
            {
                if (s.EndsWith("+"))
                {
                    if (!blnBeenPlus)
                    {
                        string sup = ProcessSuper(s.Substring(0, s.IndexOf("+")));
                        // sup = xscribeSuper(s.Substring(s.IndexOf("+"), s.Length - 1));
                        //in+d+ra
                        PlusSign--;
                        carryPM.Insert(0, sup);
                        blnBeenPlus = true;
                    }
                }
                //if ( PlusSign > 0)
                // {
                //     string yadda = ComplexStack(s, PlusSign);
                //     result.Append( yadda);
                //     s = s.Substring(0, s.LastIndexOf("+"));
                // }


                carryPM.Insert(0, ProcessNormal(s));

            }

            return carryPM.ToString();
        }

        private object ProcessNormal(string s)
        {
            ds += " ProcessNormal + String: " + s + Environment.NewLine;
            bool blnFound = true;
            StringBuilder result = new StringBuilder();
            while (s.Length > 0 && blnFound)
            {
                blnFound = false;
                // important to end the loop on didAny, 
                //to start looking for longest combos first on subsequent passes
                for (int i = 0; i < MAINS.Length && !blnFound; i++)
                {
                    if (s.StartsWith(MAINS[i]))
                    {
                        blnFound = true;
                        s = s.Substring(MAINS[i].Length);
                        result.Append(MAINS_UNI[i]);
                        ds += MAINS_UNIDebug[i] + " ";
                    }
                }
            }
            // if we have anything left over, perhaps it's a complex stack
            if (s.Length > 0)
            {
                if (CharacterOccurance(s, '+') > 1)
                {
                    string cs = ComplexStack(s);

                    result.Append(cs);
                    // whatever is left has to be punctuation
                    result.Append(ProcessPunctuation(s));
                    s = "";
                }
            }
            return result.ToString() + s; // tacking on s to reflect any couldn't-be-transcribed contents

        }

        private string ProcessSuper(string s)
        {
            ds += " ProcessSuper string: " + s + Environment.NewLine;
            //  apparently superscripted glyphs are just the normal one.  the logic requires the subjoined glyphs to trigger 
            // transformations in the superjoined ones
            // bring back the super unicode
            for (int i = 0; i < MAINS.Length; i++)
            {
                if (MAINS[i].Equals(s))
                {
                    ds += MAINS_UNIDebug[i] + " ";
                    return MAINS_UNI[i];

                }
            }
            return "?";
        }

        private string ComplexStack(string ss)
        {
            ds += " ComplexStack string: " + ss + Environment.NewLine;
            if (String.IsNullOrEmpty(ss)) { return ""; }
            string[] stacks = ss.Split('+');

            StringBuilder sb = new StringBuilder();
            int mainIdx = -1;
            foreach (string cc in stacks)
            {
                mainIdx = -1;
                if (!String.IsNullOrEmpty(cc))
                {
                    for (int j = 0; j < MAINS.Length && mainIdx == -1; j++)
                    {

                        if (cc.Equals(MAINS[j]))
                        {
                            mainIdx = j;
                            ds += MAINS_SUBDebug[j] + " ";
                            sb.Append(MAINS_SUB[j]);
                        }
                        PlusSign--;
                    }

                }
            }
            return sb.ToString();
        }

        public string ProcessPunctuation(String source)
        {
            ds += " Punctuation string: " + source + Environment.NewLine;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                char c = Convert.ToChar(source.Substring(i, 1));//source.charAt(i);
                if (c == '*') { sb.Append("\u0F0C"); }
                else if (c == '_') { sb.Append(" "); }
                else if (c == '/') { sb.Append("\u0F0D"); }
                else if (c == ',') { sb.Append("\u0F0D"); }
                else if (c == ';') { sb.Append("\u0F0F"); }
                else if (c == '|') { sb.Append("\u0F11"); }
                else if (c == '!') { sb.Append("\u0F08"); }
                else if (c == ':') { sb.Append("\u0F14"); }
                else if (c == '[') { sb.Append("\u0F10"); }
                else if (c == ']') { sb.Append("\u0F12"); }
                else if (c == '`') { sb.Append("\u0F13"); }
                else if (c == '@') { sb.Append("\u0F04"); }
                else if (c == '#') { sb.Append("\u0F05"); }
                else if (c == '$') { sb.Append("\u0F06"); }
                else if (c == '%') { sb.Append("\u0F07"); }
                else if (c == '=') { sb.Append("\u0F34"); }
                else if (c == '<') { sb.Append("\u0F3A"); }
                else if (c == '>') { sb.Append("\u0F3B"); }
                else if (c == '(') { sb.Append("\u0F3C"); }
                else if (c == ')') { sb.Append("\u0F3D"); }
                else if (c == '{') { sb.Append("\u0F3F"); }
                else if (c == '}') { sb.Append("\u0F3E"); }
                else if (c == '0') { sb.Append("\u0F20"); }
                else if (c == '1') { sb.Append("\u0F21"); }
                else if (c == '2') { sb.Append("\u0F22"); }
                else if (c == '3') { sb.Append("\u0F23"); }
                else if (c == '4') { sb.Append("\u0F24"); }
                else if (c == '5') { sb.Append("\u0F25"); }
                else if (c == '6') { sb.Append("\u0F26"); }
                else if (c == '7') { sb.Append("\u0F27"); }
                else if (c == '8') { sb.Append("\u0F28"); }
                else if (c == '9') { sb.Append("\u0F29"); }
                else { sb.Append(c); }
            }
            return sb.ToString();
        }

        // UTILS
        private int CharacterOccurance(string word, char letter)
        {
            char[] split = word.ToCharArray(); // split the word into a character array
            int count = 0; // count of the numbers a character occurs
            foreach (char character in split) // for each character in the array split..
            {
                if (character.Equals(letter)) // if the character in the array equals the letter you want to check for..
                    count++; // add one to count.
            }
            return count; // return how many times the character occured
        }

    }
}
