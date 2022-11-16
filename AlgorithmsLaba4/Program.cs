using AlgorithmsLaba4.Task_1;
using AlgorithmsLaba4.Task2;
using AlgorithmsLaba4.Task3;
using System.Text;
using System.Transactions;

namespace AlgorithmsLaba4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] options = { "Вывод сортировок", "Внешние сортировки","Сортировки строк", "Exit" };
            string contents = "Лабароторная работа 4";
            do
            {
                Console.Clear();
                MenuRendering menu = new MenuRendering(options, contents);
                int selectedIndex = menu.Run();
                switch (selectedIndex)
                {
                    case 0:
                        Console.Clear();
                        MenuTask1 menuTask1 = new MenuTask1();
                        menuTask1.MainMenu();
                        Console.ReadLine();
                        break;
                    case 1:
                        Console.Clear();
                        MenuTask2 menuTask2 = new MenuTask2();
                        menuTask2.MainMenu();
                        break;
                    case 2:
                        Console.Clear();
                        MenuTask3 menuTask3 = new MenuTask3();
                        menuTask3.MainMenu();
                        return;
                    case 3:
                        return;
                }
            } while (true);
        }
    }
}