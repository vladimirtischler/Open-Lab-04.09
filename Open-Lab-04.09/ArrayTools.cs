using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Open_Lab_04._09
{
    public class ArrayTools
    {
        public string[] RemoveDups(string[] strings)
        {
            List<string> singleWords = new List<string>();
            foreach (var c in strings)
            {
                if (!singleWords.Contains(c))
                {
                    singleWords.Add(c);
                }
            }
            return singleWords.ToArray();
        }
    }
}
