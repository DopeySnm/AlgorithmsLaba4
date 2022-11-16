using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4.Task3
{
    internal class MenuTask3
    {
        private int count;
        private int countPoint;
        private int sizeWordMax;
        private int sizeWordMin;
        public void MainMenu()
        {
            string[] options = { "BubbleSortString", "MSDSortString", "Back" };
            string contents = "Тесты Сортировки строк";
            do
            {
                WriteResult writeResult = new WriteResult();
                Test test;
                (double[], int[]) write;
                Console.Clear();
                MenuRendering menu = new MenuRendering(options, contents);
                int selectedIndex = menu.Run();
                switch (selectedIndex)
                {
                    case 0:
                        Console.Clear();
                        SetData();
                        BubbleSortString bubbleSortString = new BubbleSortString();
                        test = new Test();
                        write = test.RunTest(bubbleSortString, count ,countPoint, sizeWordMin, sizeWordMax);
                        PrintWord(bubbleSortString.GetUniqueElements());
                        writeResult.WriteFileResult("BubbleSortString", write.Item1, write.Item2);
                        Console.ReadLine();
                        break;
                    case 1:
                        Console.Clear();
                        SetData();
                        MSDSortString mSDSortString = new MSDSortString();
                        test = new Test();
                        write = test.RunTest(mSDSortString, count, countPoint, sizeWordMin, sizeWordMax);
                        PrintWord(mSDSortString.GetUniqueElements());
                        writeResult.WriteFileResult("MSDSortString", write.Item1, write.Item2);
                        Console.ReadLine();
                        break;
                    case 2:
                        return;
                }
            } while (true);
        }
        private void PrintWord(Dictionary<string, int> data)
        {
            foreach (var item in data)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
        }
        private void SetData()
        {
            //Console.WriteLine("Введите количество тестируемых данных");
            //count = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество точек");
            countPoint = int.Parse(Console.ReadLine());
            //Console.WriteLine("Введите минимальный размер слова");
            //sizeWordMin = int.Parse(Console.ReadLine());
            //Console.WriteLine("Введите максимальный размер слова");
            //sizeWordMax = int.Parse(Console.ReadLine());
        }
    }
}
