/* File: WordDefCollection.cs
 * Date: 06.10.2009
 * Desc: Collection holding tibetan definitions retrieved from the db. 
 * Auth: © Al Gallo 2009 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DagYig
{
    class WordDefCollection
    {
        public ArrayList WordDefArray;
        WordComparer comparer = new WordComparer();
        int index = -1;
        int scrollIndex = -1;
        public WordDefCollection()
        {
            WordDefArray = new ArrayList();
            WordComparer comparer = new WordComparer();
            comparer.ComparisonMethod = WordComparer.ComparisonType.trans;
            WordDef.SetSortOrder(WordDef.SortOrder.Ascending);
        }
        //public IEnumerator GetEnumerator()
        //{
        //    return (WordDefArray as IEnumerable).GetEnumerator();

        //}

        public void Add(string wyl, string tib, string tr)
        {


            comparer.ComparisonMethod = WordComparer.ComparisonType.tibetan;
            WordDefArray.Add(new WordDef(wyl, tib, tr));
          //  WordDefArray.Sort(comparer);
            //IEnumerable<string> filteredNames = names.OrderBy(n => n.Length);
            index++;
            scrollIndex = index;
        }
        public void Add(WordDef wd)
        {


            comparer.ComparisonMethod = WordComparer.ComparisonType.tibetan;
            WordDefArray.Add(wd);
        //    WordDefArray.Sort(comparer);
            //IEnumerable<string> filteredNames = names.OrderBy(n => n.Length);
            index++;
            scrollIndex = index;
        }

        public void RemoveLink(string trans)
        {
            foreach (WordDef l in WordDefArray)
            {
                if (l.Trans == trans)
                {

                    WordDefArray.Remove(l);
                    return;
                }
            }
        }

        public string GetTrans(string tib)
        {
            foreach (WordDef l in WordDefArray)
            {
                if (l.Tibetan == tib)
                {
                    return l.Trans;
                }
            }
            return "";
        }

        public string GetTibetan(string trans)
        {
            foreach (WordDef l in WordDefArray)
            {
                if (l.Trans == trans)
                {
                    return l.Tibetan;
                }
            }
            return "";
        }

        public bool ContainsWylie(string wyl)
        {
            foreach (WordDef l in WordDefArray)
            {
                if (l.Wylie == wyl)
                {

                    return true;
                }
            }

            return false;
        }

        public bool ContainsTrans(string trans)
        {
            foreach (WordDef l in WordDefArray)
            {
                if (l.Trans == trans)
                {

                    return true;
                }
            }

            return false;
        }

        public bool ContainsTib(string tib)
        {
            foreach (WordDef l in WordDefArray)
            {
                if (l.Tibetan == tib)
                {

                    return true;
                }
            }

            return false;
        }

        public bool MoveNext()
        {
            scrollIndex++;
            if (scrollIndex >= WordDefArray.Count)
                return false;
            else
                return true;

        }
      
        public object Current
        {
            get
            {
                return WordDefArray[index];
            }
        }
        public void Reset()
        {
            scrollIndex = -1;
        }

        //private int GetNextElement(ArrayList objArrayList, ref int index, ref int origCount)
        //{
        //    int nextElement = -1;

        //    int count = objArrayList.Count;
        //    if (count == 0)
        //        return nextElement;

        //    if ((count != origCount) && ((count - 1) < index))
        //    {
        //        origCount = count;
        //        index = count - 1;
        //    }
        //    nextElement = (int)objArrayList[index];

        //    return nextElement;
        //}

        public object GetPriorElement() 
        {
            int c = WordDefArray.Count;
            if (c < 1)
            {
                return null;
            }
            scrollIndex--;

            if (scrollIndex < 0)
            {
                scrollIndex = WordDefArray.Count - 1;
            }

            return WordDefArray[scrollIndex];
           
          
        }

        public int count()
        {
            return WordDefArray.Count;
        }
    }
}
