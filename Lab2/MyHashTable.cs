using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class MyHashTable
    {
        public static Hashtable values;
        public static Hashtable formulas;
        public static Hashtable references;

        public Hashtable Values
        {
            get { return values; }
        }

        public Hashtable Formulas
        {
            get { return formulas; }
        }

        public Hashtable References
        {
            get { return references; }
        }

        public MyHashTable()
        {
            values = new Hashtable();
            formulas = new Hashtable();
            references = new Hashtable();
        }

        public void AddFormula(string cell, string formula)
        {
            if (formulas.Contains(cell))
            {
                formulas[cell] = formula;
                return;
            }
            formulas.Add(cell, formula);
        }

        public void AddValue(string cell, string value)
        {
            if (values.Contains(cell))
            {
                values[cell] = value;
                return;
            }
            values.Add(cell, value);
        }

        public void AddReference(string cellIndex, string reference)
        {
            if (references.Contains(cellIndex))
            {
                string st = References[cellIndex].ToString();
                if (!st.Contains(reference))
                    references[cellIndex] += (" " + reference);
            }
            else
            {
                references.Add(cellIndex, reference);
            }
        }

        public void DeleteHash(string key)
        {
            formulas.Remove(key);
            values.Remove(key);

        }

        public void AddBoth(string cell, string formula, string value)
        {
            AddFormula(cell, formula);
            AddValue(cell, value);
        }
    }
}
