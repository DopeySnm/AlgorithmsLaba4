using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4.Task3
{
    internal class MSDSortString : IAlgorithms
    {
        private string[] data;
        private int Char_at(string str, int d)
        {
            if (str.Length <= d)
                return -1;
            else
                return (int)(str[d]);
        }
        public Dictionary<string, int> GetUniqueElements()
        {
            Dictionary<string, int> uniqueElements = new Dictionary<string, int>();
            for (int i = 0; i < data.Length; i++)
            {
                if (uniqueElements.ContainsKey(data[i]))
                {
                    uniqueElements[key: data[i]] += 1;
                }
                else
                {
                    uniqueElements.Add(data[i], 1);
                }
            }
            return uniqueElements;
        }
        public void SetData(string[] data)
        {
            this.data = data;
        }
        public string[] GetData()
        {
            return data;
        }
        public void Sort()
        {
            int n = data.Length;
            MSD_sort(data, 0, n - 1, 0);
        }
        private void MSD_sort(string[] str, int lo, int hi, int d)
        {
            if (hi <= lo)
            {
                return;
            }
            int[] count = new int[256 + 1];
            Dictionary<int,string> temp = new Dictionary<int,string>();
            for (int i = lo; i <= hi; i++)
            {
                int c = Char_at(str[i], d);
                count[c + 2]++;
            }
            for (int r = 0; r < 256; r++)
            { 
                count[r + 1] += count[r]; 
            }
            for (int i = lo; i <= hi; i++)
            {
                int c = Char_at(str[i], d);
                temp.Add(count[c + 1]++, str[i]);
            }
            for (int i = lo; i <= hi; i++)
            {
                str[i] = temp[i - lo];
            }
            for (int r = 0; r < 256; r++)
            {
                MSD_sort(str, lo + count[r], lo + count[r + 1] - 1, d + 1);
            }
        }
        public void Test()
        {
            Sort();
        }
    }
}
