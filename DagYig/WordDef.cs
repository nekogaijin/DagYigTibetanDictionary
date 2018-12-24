/* File: WordDef.cs
 * Date: 06.10.2009
 * Desc: tibetan definitions retrieved from the db. 
 * Auth: © Al Gallo 2009 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections;

namespace DagYig
{
    public class WordDef : System.IComparable
    {

        private int id;
        private string wylie;
        private string tibetan;
        private string trans;
        private static SortOrder order;

        public enum SortOrder
        {
            Ascending = 0,
            Descending = 1
        }
       
        public WordDef(string wylie, string tibetan, string trans)
        {
            this.wylie = wylie;
            this.tibetan = tibetan;
            this.trans = trans;
        }
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Wylie
        {
            get { return this.wylie; }
            set { this.wylie = value; }
        }

        public string Trans
        {
            get { return this.trans; }
            set { this.trans = value; }
        }
        public string Tibetan
        {
            get { return this.tibetan; }
            set { this.tibetan = value; }
        }

        public static void SetSortOrder(SortOrder so)
        {
            order = so;
        }
        public static SortOrder Order
        {
            get { return order; }
            set { order = value; }
        }

        public override bool Equals(Object obj)
        {
            bool retVal = false;
            if (obj != null)
            {
                WordDef lObj = (WordDef)obj;
                if (lObj.Trans == this.Trans)
                {
                    retVal = true;
                }
            }
            return retVal;
        }

        public override string ToString()
        {
            return this.wylie + " : " + this.trans;
        }
        public int CompareTo(WordDef p2, WordComparer.ComparisonType comparisonMethod)
        {
            switch (comparisonMethod)
            {
                case WordComparer.ComparisonType.tibetan:
                    return tibetan.CompareTo(p2.tibetan);
                case WordComparer.ComparisonType.trans:
                    return trans.CompareTo(p2.trans);
                case WordComparer.ComparisonType.wylie:
                    return trans.CompareTo(p2.wylie);
                default:
                    return trans.CompareTo(p2.trans);
            }
        }
        public int CompareTo(Object obj)
        {
            switch (order)
            {
                case SortOrder.Ascending:

                    if (this.tibetan.Length <= (((WordDef)obj).tibetan).Length)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                case SortOrder.Descending:
                    if (this.tibetan.Length <= (((WordDef)obj).tibetan).Length)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                default:
                    if (this.tibetan.Length <= (((WordDef)obj).tibetan).Length)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
            }
        }

       
        public override int GetHashCode()
        {
            int hashCode = trans.GetHashCode();
            hashCode ^= trans.GetHashCode(); // Xor (eXclusive OR)
            return hashCode;
        }


    }

    public class WordComparer : IComparer
    {
        public enum ComparisonType
        { wylie = 1, tibetan = 2, trans = 3 }

        private ComparisonType _comparisonType;

        public ComparisonType ComparisonMethod
        {
            get { return _comparisonType; }
            set { _comparisonType = value; }
        }

        #region IComparer Members

        public int Compare(object x, object y)
        {
            WordDef p1;
            WordDef p2;

            if (x is WordDef)
                p1 = x as WordDef;
            else
                throw new ArgumentException("Object is not of type Link.");

            if (y is WordDef)
                p2 = y as WordDef;
            else
                throw new ArgumentException("Object is not of type Link.");

            return p1.CompareTo(p2);
        }

        #endregion
    }
}
