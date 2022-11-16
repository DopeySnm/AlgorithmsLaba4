using AlgorithmsLaba4.Task3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4.Task2
{
    internal class MenuTask2
    {
        public void MainMenu()
        {
            string[] options = { "Прямая", "Естественная", "Back" };
            string contents = "Внешние сортировки";
            do
            {
                Console.Clear();
                MenuRendering menu = new MenuRendering(options, contents);
                int selectedIndex = menu.Run();
                switch (selectedIndex)
                {
                    case 0:
                        Console.Clear();
                        DirectMerge directMerge = new DirectMerge();
                        directMerge.Sorting();
                        Console.ReadLine();
                        break;
                    case 1:
                        Console.Clear();
                        NaturalMerge naturalMerge = new NaturalMerge();
                        naturalMerge.Sorting();
                        Console.ReadLine();
                        break;
                    case 2:
                        return;
                }
            } while (true);
        }
    }
}
