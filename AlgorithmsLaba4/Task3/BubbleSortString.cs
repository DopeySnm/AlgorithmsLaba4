using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4.Task3
{
    internal class BubbleSortString : IAlgorithms
    {
        private string[] data;
        public void SetData(string[] data)
        {
            this.data = data;
        }
        public string[] GetData()
        {
            return data;
        }
        public Dictionary<string, int> GetUniqueElements()
        {
            Dictionary<string, int> uniqueElements = new Dictionary<string, int>();
            for (int i = 0; i < data.Length; i++)
            {
                if (uniqueElements.ContainsKey(data[i]))
                {
                    uniqueElements[key:data[i]] += 1;
                }
                else
                {
                    uniqueElements.Add(data[i], 1);
                }
            }
            return uniqueElements;
        }
        public void SortStrings()
        {
            if (data == null)
            {
                return;
            }
            for (int j = 0; j < data.Length - 1; j++)
            {
                for (int i = j + 1; i < data.Length; i++)
                {
                    if (data[j].CompareTo(data[i]) > 0)
                    {
                        Swop(i, j);
                    }
                }
            }
        }
        private void Swop(int i, int j)
        {
            string temp = data[j];
            data[j] = data[i];
            data[i] = temp;
        }
        public void Test()
        {
            SortStrings();
        }
    }
}
