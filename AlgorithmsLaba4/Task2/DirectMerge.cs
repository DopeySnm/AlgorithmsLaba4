using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4.Task2
{
    internal class DirectMerge
    {
        private long maxPositionA = -1;
        private long maxPositionB = 0;
        private long maxPositionC = -1;
        private long positionA = 0;
        private long positionB = 0;
        private long positionC = 0;
        private int countA = 0;
        private int countB = 0;
        private int countC = 0;
        private List<int> countOp;
        private Logger logger;
        public DirectMerge()
        {
            logger = new Logger("Прямая сортировка");
            IMessageHandler fileHandler = new FileHandler("DirectMergeLog");
            logger.addMessageHandler(fileHandler);
            logger.SetLevel(Level.INFO);
        }
        private void CountOpSet()
        {
            while (true)
            {
                for (int i = 0; i < countOp.Count; i++)
                {
                    if (countOp[countOp.Count - 1] < countA)
                    {
                        countOp.Add(countOp[countOp.Count - 1] * 2);
                        break;
                    }
                    else
                    {
                        countOp[countOp.Count - 1] = countA;
                        return;
                    }
                }
            }
        }
        public void Sorting()
        {
            bool temp = true;
            countOp = new List<int>();
            countOp.Add(2);
            int sizeTempData = 2;
            //NaturalMerge naturalMerge = new NaturalMerge();
            //bool exit = naturalMerge.Distribution();
            //if (exit)
            //{
            //    countOp.Clear();
            //}
            bool readB = false;
            bool readC = false;
            ClearFile("B");
            ClearFile("C");
            int tempDataWrite = 0;
            for (int i = 0; i < countOp.Count; i++)
            {
                //если последовательность остортированна то выходим (добавить метод)
                do
                {
                    for (int j = 0; j < sizeTempData / 2; j++)
                    {
                        tempDataWrite = Read("A", positionA);
                        Write(tempDataWrite, "B", true);
                        countB++;
                        if (positionA == maxPositionA)
                        {
                            break;
                        }
                    }
                    if (maxPositionA == positionA)
                    {
                        break;
                    }
                    for (int j = 0; j < sizeTempData / 2; j++)
                    {
                        tempDataWrite = Read("A", positionA);
                        Write(tempDataWrite, "C", true);
                        countC++;
                        if (positionA == maxPositionA)
                        {
                            break;
                        }
                    }
                } while (maxPositionA != positionA);
                countA = countB + countC;
                if (temp) // убрать куда-то в начало и переработать
                {
                    CountOpSet();
                    temp = false;
                }
                ClearFile("A");
                if (i == 4)
                {
                    Console.WriteLine();
                }
                //int firstRead = Read("B", positionB);
                List<int> listB = new List<int>();
                List<int> listC = new List<int>();
                listB.Add(Read("B", positionB));
                listC.Add(Read("C", positionC));
                //int secondRead = Read("C", positionC);
                while ((maxPositionB != positionB) || (countB != 0))
                {
                    if (maxPositionC != positionC || countC != 0)// если в C закончились элементы, то дозаписываем все элементы из B
                    {
                        int countAllOp = countOp[i];// количесво операций считования
                        int countOpB = sizeTempData / 2;// количесво операций считования с B
                        int countOpC = sizeTempData / 2;// количесво операций считования с С
                        if (i == countOp.Count - 1 && countA > 2)
                        {
                            countOpC = countOp[i] - countOp[i - 1];
                            // если финальные сравнения,
                            // то кол-во операций C будет равно
                            // кол-во всех предпоследних оперций - кол-во всех финальных операций
                        }
                        while (countAllOp > 0 && countC != 0)
                        {
                            if (listB[0].CompareTo(listC[0]).Equals(-1))
                            {
                                if (listB.Count != 0)
                                {
                                    Write(listB[0], "A", true);
                                    listB.Clear();
                                }
                                countB--;
                                countOpB--;
                                if (maxPositionB != positionB)
                                {
                                    listB.Add(Read("B", positionB));
                                }
                            }
                            else
                            {
                                if (listC.Count != 0)
                                {
                                    Write(listC[0], "A", true);
                                    listC.Clear();
                                }
                                countC--;
                                countOpC--;
                                if (positionC != maxPositionC)
                                {
                                    listC.Add(Read("C", positionC));
                                }
                            }
                            countAllOp--;
                            if (countOpB == 0 || countOpC == 0)
                            {
                                break;
                            }
                        }
                        while (countAllOp > 0)
                        {
                            if (countOpB > 0)
                            {
                                if (listB.Count != 0)
                                {
                                    Write(listB[0], "A", true);
                                    listB.Clear();
                                }
                                countB--;
                                countOpB--;
                                if (maxPositionB != positionB)
                                {
                                    listB.Add(Read("B", positionB));
                                }
                            }
                            else if (countOpC > 0)
                            {
                                if (listC.Count != 0)
                                {
                                    Write(listC[0], "A", true);
                                    listC.Clear();
                                }
                                countC--;
                                countOpC--;
                                if (positionC != maxPositionC)
                                {
                                    listC.Add(Read("C", positionC));
                                }
                            }
                            countAllOp--;
                        }
                    }
                    else
                    {
                        if (listB.Count != 0)
                        {
                            Write(listB[0], "A", true);
                            listB.Clear();
                        }
                        countB--;
                        if (maxPositionB != positionB)
                        {
                            listB.Add(Read("B", positionB));
                        }
                    }
                }
                sizeTempData *= 2;
                positionA = 0;
                positionB = 0;
                positionC = 0;
                ClearFile("B");
                ClearFile("C");
                countA = 0;
                countB = 0;
                countC = 0;
            }
            Console.WriteLine("Готово, нажмите Enter чтобы продолжить");
        }
        private void Write(int[] data, string namePath, bool append)
        {
            string path = $"..\\..\\..\\..\\TestMerge\\{namePath}.txt";
            try
            {
                using (StreamWriter sr = new StreamWriter(path, append))
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        sr.Write(data[i] + " ");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be write:");
                Console.WriteLine(e.Message);
            }
        }
        private void Write(int data, string namePath, bool append)
        {
            string path = $"..\\..\\..\\..\\TestMerge\\{namePath}.txt";
            try
            {
                using (StreamWriter sr = new StreamWriter(path, append))
                {
                    sr.Write(data + " ");
                    sr.Close();
                    logger.Log(Level.INFO, $"Элемент {data} записался в файл {namePath}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be write:");
                Console.WriteLine(e.Message);
            }
        }
        private int[] Read(string namePath, long position = 0, int count = 1)
        {
            string path = $"..\\..\\..\\..\\TestMerge\\{namePath}.txt";
            List<int> result = new List<int>();
            try
            {
                using (FileStream sr = new FileStream(path, FileMode.Open))
                {
                    byte[] buffer = new byte[1];
                    sr.Seek(position, SeekOrigin.Current);
                    for (int i = 0; i < count; i++)
                    {
                        string temp = "";
                        do
                        {
                            var a = sr.Read(buffer, 0, 1);
                            char converToChar = (char)buffer[0];
                            if (buffer[0] != 0)
                            {
                                temp += converToChar;
                            }
                        } while (buffer[0] != 32);
                        if (temp != " ")
                        {
                            result.Add(int.Parse(temp));
                        }
                    }
                    if (namePath.Equals("A"))
                    {
                        positionA = sr.Position;
                        maxPositionA = sr.Length;
                    }
                    else if (namePath.Equals("B"))
                    {
                        positionB = sr.Position;
                        maxPositionB = sr.Length;
                    }
                    else if (namePath.Equals("C"))
                    {
                        positionC = sr.Position;
                        maxPositionC = sr.Length;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return result.ToArray();
        }
        private int Read(string namePath, long position = 0)
        {
            string path = $"..\\..\\..\\..\\TestMerge\\{namePath}.txt";
            string result = "";
            try
            {
                using (FileStream sr = new FileStream(path, FileMode.Open))
                {
                    byte[] buffer = new byte[1];
                    string temp = "";
                    sr.Seek(position, SeekOrigin.Current);
                    do
                    {
                        var a = sr.Read(buffer, 0, 1);
                        char convertToChar = (char)buffer[0];
                        if (convertToChar == '\0')
                        {
                            throw new Exception();
                        }
                        temp += convertToChar;
                    } while (buffer[0] != 32);
                    result = temp;
                    logger.Log(Level.INFO, $"Элемент {result} считался из файла {namePath}");
                    if (namePath.Equals("A"))
                    {
                        positionA = sr.Position;
                        maxPositionA = sr.Length;
                    }
                    else if (namePath.Equals("B"))
                    {
                        positionB = sr.Position;
                        maxPositionB = sr.Length;
                    }
                    else if (namePath.Equals("C"))
                    {
                        positionC = sr.Position;
                        maxPositionC = sr.Length;
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                throw new Exception("Неверный формат");
            }
            return int.Parse(result);
        }
        private void ClearFile(string namePath)
        {
            string path = $"..\\..\\..\\..\\TestMerge\\{namePath}.txt";
            try
            {
                using (StreamWriter sr = new StreamWriter(path, false))
                {
                    sr.Write("");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be write:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
