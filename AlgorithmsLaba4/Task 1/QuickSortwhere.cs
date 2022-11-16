using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4.Task_1
{
    internal class QuickSortwhere<T> where T : IComparable
    {
        private T[] data;
        private int timeSleep;
        public int d;
        public int a = 1;
        public Logger logger;

        public QuickSortwhere()
        {
            logger = new Logger("Быстрая сортировка");
            IMessageHandler fileHandler = new FileHandler("QuickSortwhereLog");
            logger.addMessageHandler(fileHandler);
            logger.SetLevel(Level.INFO);
        }
        public void Sort(T[] data, int timeSleep)
        {
            this.timeSleep = timeSleep;
            this.data = data;
            QuickSort(0, data.Length - 1);

            for (int j = 0; j < data.Length - 1; j++)
            {
                //Thread.Sleep(timeSleep);
                if (data[j].CompareTo(data[j + 1]).Equals(1))
                {
                    Swop(j, j + 1);
                }
            }
        }
        private int Partition(int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (data[i].CompareTo(data[maxIndex]).Equals(-1))
                {
                    pivot++;
                    Swop(pivot, i);
                    Thread.Sleep(timeSleep);
                    OutputData(pivot, i);
                }
            }
            pivot++;
            Swop(pivot, maxIndex);
            return pivot;
        }
        private void QuickSort(int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return;
            }
            var pivotIndex = Partition(minIndex, maxIndex);
            a++;
            OutputData(pivotIndex, maxIndex);
            QuickSort(minIndex, pivotIndex - 1);
            QuickSort(pivotIndex + 1, maxIndex);
            QuickSort(pivotIndex + 1, pivotIndex - 1);
        }
        private void Swop(int indexA, int indexB)
        {
            var temp = data[indexA];
            data[indexA] = data[indexB];
            data[indexB] = temp;
        }
        private void OutputData(int support, int maxind)
        {
            Console.WriteLine();
            Console.Write("[ ");
            int e = 0;

            string basePath = Environment.CurrentDirectory;
            basePath += @"\content.txt";

            for (int i = 0; i < data.Length; i++)
            {
                if (i == support || i == maxind)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (data.Length - 1 == i)
                    {
                        Console.Write(data[i] + " ");
                        string text2 = $"{data[i] + " "}";
                        //File.AppendAllText(basePath, text2);
                    }
                    else
                    {
                        Console.Write(data[i] + ", ");
                        string text2 = $"{data[i] + ", "}";
                        //File.AppendAllText(basePath, text2);
                    }

                    if (e == 0)
                    {
                        e = i;
                        e += 1;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (data.Length - 1 == i)
                    {
                        Console.Write(data[i] + " ");
                        string text2 = $"{data[i] + " "}";
                        // File.AppendAllText(basePath, text2);
                    }
                    else
                    {
                        Console.Write(data[i] + ", ");
                        string text2 = $"{data[i] + ", "}";
                        //File.AppendAllText(basePath, text2);
                    }
                }
            }
            e--;
            Console.Write("]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" - поменяли элемент {data[e]} под индексом {e} и элемент {data[maxind]} под индексом {maxind}");
            string text = $" поменяли элемент {data[e]} под индексом {e} и элемент {data[maxind]} под индексом {maxind}";
            logger.Log(Level.INFO, text);
            File.AppendAllText(basePath, text + (Environment.NewLine));
        }
    }
}
