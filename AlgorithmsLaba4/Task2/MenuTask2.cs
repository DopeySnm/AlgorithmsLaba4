using AlgorithmsLaba4.Task3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AlgorithmsLaba4.Task2
{
    internal class MenuTask2
    {
        public void MainMenu()
        {
            string[] options = { "Прямая", "Естественная","Трёх путевое" ,"Back" };
            string contents = "Внешние сортировки";
            do
            {
                int num;
                Console.Clear();
                MenuRendering menu = new MenuRendering(options, contents);
                int selectedIndex = menu.Run();
                switch (selectedIndex)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Таблица\n");
                        PrintTable();
                        Console.WriteLine("Выберете столбец сортировки");
                        num = int.Parse(Console.ReadLine());
                        DirectMerge directMerge = new DirectMerge();
                        directMerge.Sorting(num);
                        Console.ReadLine();
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Таблица\n");
                        PrintTable();
                        Console.WriteLine("Выберете столбец сортировки");
                        num = int.Parse(Console.ReadLine());
                        NaturalMerge naturalMerge = new NaturalMerge();
                        naturalMerge.Sorting(num);
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Таблица\n");
                        PrintTable();
                        Console.WriteLine("Выберете столбец сортировки");
                        num = int.Parse(Console.ReadLine());
                        MultipathMerging multipathMerging = new MultipathMerging();
                        multipathMerging.Sorting(num);
                        Console.ReadLine();
                        break;
                    case 3:
                        return;
                }
            } while (true);
        }
        private void PrintTable()
        {
            StreamReader streamReader = new StreamReader($"..\\..\\..\\..\\TestMerge\\A.txt");
            while (!streamReader.EndOfStream)
            {
                var a = streamReader.ReadLine();
                Console.WriteLine(a);
            }
            streamReader.Close();
        }
    }
}
