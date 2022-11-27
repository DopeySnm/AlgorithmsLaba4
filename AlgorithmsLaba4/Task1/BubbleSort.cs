using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4.Task_1
{
    internal class BubbleSort<T> where T : IComparable
    {
        private T[] data;
        public Logger logger;

        public BubbleSort()
        {
            logger = new Logger("Сортировка пузырьком");
            IMessageHandler fileHandler = new FileHandler("BubbleSortLog");
            logger.addMessageHandler(fileHandler);
            logger.SetLevel(Level.INFO);

        }
        public void Sort(T[] data, int timeOutput)
        {
            this.data = data;
            Console.WriteLine("Сортировка пузырьком");
            Console.WriteLine("Начальные данные: ");
            OutputData();
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data.Length - 1 - i; j++)
                {
                    Thread.Sleep(timeOutput);
                    if (data[j].CompareTo(data[j + 1]).Equals(1))
                    {
                        Swop(j, j + 1);
                        ConsoleOutput(j, j + 1, true);
                    }
                    else
                    {
                        ConsoleOutput(j, j + 1, false);
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Итоговые данные");
            OutputData();
        }
        private void OutputData()
        {
            Console.WriteLine();
            Console.Write("[ ");
            for (int i = 0; i < data.Length; i++)
            {
                if (data.Length - 1 == i)
                {
                    Console.Write(data[i] + " ");
                }
                else
                {
                    Console.Write(data[i] + ", ");
                }
            }
            Console.Write("]");
            Console.WriteLine();
        }
        private void ConsoleOutput(int indexA, int indexB, bool swop)
        {
            string basePath = Environment.CurrentDirectory;
            basePath += @"\content.txt";
            Console.WriteLine();
            Console.Write("[ ");
            string text2 = "";
            for (int i = 0; i < data.Length; i++)
            {
                if (i == indexA || i == indexB)
                {
                    if (swop)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    if (data.Length - 1 == i)
                    {
                        Console.Write(data[i]);

                        Console.ResetColor();
                        Console.Write(" ");
                        text2 += (data[i] + ", ");
                        // File.AppendAllText(basePath, text2);

                    }
                    else
                    {
                        Console.Write(data[i]);
                        Console.ResetColor();
                        Console.Write(", ");
                        text2 += data[i] + ", ";
                        //File.AppendAllText(basePath, text2);

                    }
                }
                else
                {
                    if (data.Length - 1 == i)
                    {
                        text2 += (data[i] + " ");
                        Console.Write(data[i] + " ");
                        //File.AppendAllText(basePath, text2);

                    }
                    else
                    {
                        Console.Write(data[i] + ", ");
                        text2 += (data[i] + ", ");
                        // File.AppendAllText(basePath, text2);

                    }
                }
            }
            //logger.Log(Level.INFO, text2);
            Console.Write("]");
            text2 += "]";
            if (swop)
            {
                Console.Write($" - меняем элемент {data[indexA]} под индексом {indexA} и элемент {data[indexB]} под индексом {indexB}");
                string text = $"меняем элемент {data[indexA]} под индексом {indexA} и элемент {data[indexB]} под индексом {indexB}";
                //File.AppendAllText(basePath, text + (Environment.NewLine));
                logger.Log(Level.INFO, text);
            }
            Console.WriteLine();
        }
        public void Swop(int indexA, int indexB)
        {
            var temp = data[indexA];
            data[indexA] = data[indexB];
            data[indexB] = temp;
        }
    }
}
