using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4.Task3
{
    internal class WriteResult
    {
        public void WriteFileResult(string nameFile, double[] data, int[] dataResult)
        {
            string[] writeData = new string[data.Length];
            for (int i = 0; i < writeData.Length; i++)
            {
                writeData[i] = $"{dataResult[i]}_{data[i]}";
            }
            File.WriteAllLines($"..\\..\\..\\..\\TestTime\\{nameFile}.csv", writeData);
        }
    }
}
