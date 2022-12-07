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
            string[] options = { "Прямая", "Естественная", "Трёх путевое", "Back" };
            string contents = "Внешние сортировки";
            do
            {
                int num;
                Console.Clear();
                MenuRendering menu = new MenuRendering(options, contents);
                int selectedIndex = menu.Run();
                string table;
                switch (selectedIndex)
                {
                    case 0:
                        Console.Clear();
                        table = SelectSortFile();
                        Console.WriteLine("Таблица\n");
                        PrintTable();
                        Console.WriteLine("Выберете столбец сортировки");
                        num = int.Parse(Console.ReadLine());
                        DirectMerge directMerge = new DirectMerge();
                        directMerge.Sorting(num);
                        Copy($"..\\..\\..\\..\\TestMerge\\A.txt", $"..\\..\\..\\..\\TestMerge\\Table\\{table}");
                        Console.ReadLine();
                        break;
                    case 1:
                        Console.Clear();
                        table = SelectSortFile();
                        Console.WriteLine("Таблица\n");
                        PrintTable();
                        Console.WriteLine("Выберете столбец сортировки");
                        num = int.Parse(Console.ReadLine());
                        NaturalMerge naturalMerge = new NaturalMerge();
                        naturalMerge.Sorting(num);
                        Copy($"..\\..\\..\\..\\TestMerge\\A.txt", $"..\\..\\..\\..\\TestMerge\\Table\\{table}");
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                        table = SelectSortFile();
                        Console.WriteLine("Таблица\n");
                        PrintTable();
                        Console.WriteLine("Выберете столбец сортировки");
                        num = int.Parse(Console.ReadLine());
                        MultipathMerging multipathMerging = new MultipathMerging();
                        multipathMerging.Sorting(num);
                        Copy($"..\\..\\..\\..\\TestMerge\\A.txt", $"..\\..\\..\\..\\TestMerge\\Table\\{table}");
                        Console.ReadLine();
                        break;
                    case 3:
                        return;
                }
            } while (true);
        }
        private string SelectSortFile()
        {
            Console.WriteLine("Выберите файл ");
            string[] allfiles = Directory.GetFiles($"..\\..\\..\\..\\TestMerge\\Table");
            for (int i = 0; i < allfiles.Length; i++)
            {
                var temp = allfiles[i].Split(@"\");
                allfiles[i] = temp[temp.Length - 1];
            }
            string[] options = allfiles;
            string contents = "Выбор файла";
            do
            {
                int num;
                Console.Clear();
                MenuRendering menu = new MenuRendering(options, contents);
                int selectedIndex = menu.Run();
                Console.Clear();
                Console.WriteLine(options[selectedIndex]);
                Copy($"..\\..\\..\\..\\TestMerge\\Table\\{options[selectedIndex]}", $"..\\..\\..\\..\\TestMerge\\A.txt");
                return options[selectedIndex];
            } while (true);
        }
        private void Copy(string pathRead, string pathWrite)
        {
            StreamReader streamReader = new StreamReader(pathRead);
            ClearFile(pathWrite);
            while (!streamReader.EndOfStream)
            {
                var data = streamReader.ReadLine();
                if (data.Equals(""))
                {
                    break;
                }
                Write(data, pathWrite, true);
            }
            streamReader.Close();
        }
        private void ClearFile(string namePath)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(namePath, false))
                {
                    streamWriter.Write("");
                    streamWriter.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be write:");
                Console.WriteLine(e.Message);
            }
        }
        private void Write(string data, string namePath, bool append)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(namePath, append))
                {
                    streamWriter.WriteLine(data);
                    streamWriter.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be write:");
                Console.WriteLine(e.Message);
            }
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
