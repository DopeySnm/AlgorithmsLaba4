using AlgorithmsLaba4.Task2;
using AlgorithmsLaba4.Task3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4.Task_1
{
    internal class MenuTask1
    {
        public void MainMenu()
        {
            string[] options = { "Сортировка пузырьком", "Быстрая сортировка", "Back" };
            string contents = "Вывод сортировок";
            do
            {
                string[] dataString;
                int[] data;
                int time;
                Console.Clear();
                MenuRendering menu = new MenuRendering(options, contents);
                int selectedIndex = menu.Run();
                switch (selectedIndex)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Введите элементы массива(числа) через пробел");
                        dataString = Console.ReadLine().Split(" ");
                        data = new int[dataString.Length];
                        for (int i = 0; i < data.Length; i++)
                        {
                            data[i] = int.Parse(dataString[i]);
                        }
                        Console.WriteLine("Введите время задержки в мс");
                        time =int.Parse( Console.ReadLine());
                        BubbleSort<int> bubbleSort = new BubbleSort<int>();
                        bubbleSort.Sort(data, time);
                        Console.ReadLine();
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Введите элементы массива(числа) через пробел");
                        dataString = Console.ReadLine().Split(" ");
                        data = new int[dataString.Length];
                        for (int i = 0; i < data.Length; i++)
                        {
                            data[i] = int.Parse(dataString[i]);
                        }
                        Console.WriteLine("Введите время задержки в мс");
                        time = int.Parse(Console.ReadLine());
                        QuickSortwhere<int> quickSortwhere = new QuickSortwhere<int>();
                        quickSortwhere.Sort(data, time);
                        Console.ReadLine();
                        break;
                    case 2:
                        return;
                }
            } while (true);
        }
    }
}
