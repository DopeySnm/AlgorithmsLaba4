using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
        private int columNumSort;
        public DirectMerge()
        {
            logger = new Logger("Прямая сортировка");
            IMessageHandler fileHandler = new FileHandler("DirectMergeLog");
            IMessageHandler consoleHandler = new ConsoleHandler();
            logger.addMessageHandler(fileHandler);
            logger.addMessageHandler(consoleHandler);
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
        private void DistributionFiles(int sizeTempData)
        {
            StreamReader streamReader = new StreamReader($"..\\..\\..\\..\\TestMerge\\A.txt");
            string tempDataWrite;
            do
            {
                for (int j = 0; j < sizeTempData / 2; j++)
                {
                    if (!streamReader.EndOfStream)
                    {
                        tempDataWrite = streamReader.ReadLine();
                        logger.Log(Level.INFO, $"Считываем из А {tempDataWrite}");
                        Write(tempDataWrite, "B", true);
                        countB++;
                    }
                }
                for (int j = 0; j < sizeTempData / 2; j++)
                {
                    if (!streamReader.EndOfStream)
                    {
                        tempDataWrite = streamReader.ReadLine();
                        logger.Log(Level.INFO, $"Считываем из А {tempDataWrite}");
                        Write(tempDataWrite, "C", true);
                        countC++;
                    }
                }
            } while (!streamReader.EndOfStream);
            streamReader.Close();
        }
        private void Merging(int sizeTempData, int i)
        {
            StreamReader streamReaderB = new StreamReader($"..\\..\\..\\..\\TestMerge\\B.txt");
            StreamReader streamReaderC = new StreamReader($"..\\..\\..\\..\\TestMerge\\C.txt");
            string currentB = streamReaderB.ReadLine();
            logger.Log(Level.INFO, $"Считываем из А {currentB}");
            string currentC = streamReaderC.ReadLine();
            logger.Log(Level.INFO, $"Считываем из А {currentC}");
            while ((!streamReaderB.EndOfStream) || (countB != 0))
            {
                if ((!streamReaderC.EndOfStream) || countC != 0)// если в C закончились элементы, то дозаписываем все элементы из B
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
                        var a = currentB.Split("|")[columNumSort - 1];
                        var b = currentC.Split("|")[columNumSort - 1];
                        logger.Log(Level.INFO, $"Сравниваем {a} и {b}");
                        if (a.CompareTo(b).Equals(-1))
                        {
                            if (currentB != null)
                            {
                                Write(currentB, "A", true);
                                currentB = null;
                            }
                            countB--;
                            countOpB--;
                            if (!streamReaderB.EndOfStream)
                            {
                                currentB = streamReaderB.ReadLine();
                                logger.Log(Level.INFO, $"Считываем из А {currentB}");
                            }
                        }
                        else
                        {
                            if (currentC != null)
                            {
                                Write(currentC, "A", true);
                                currentC = null;
                            }
                            countC--;
                            countOpC--;
                            if (!streamReaderC.EndOfStream)
                            {
                                currentC = streamReaderC.ReadLine();
                                logger.Log(Level.INFO, $"Считываем из А {currentC}");
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
                            if (currentB != null)
                            {
                                Write(currentB, "A", true);
                                currentB = null;
                            }
                            countB--;
                            countOpB--;
                            if (!streamReaderB.EndOfStream)
                            {
                                currentB = streamReaderB.ReadLine();
                                logger.Log(Level.INFO, $"Считываем из А {currentB}");
                            }
                        }
                        else if (countOpC > 0)
                        {
                            if (currentC != null)
                            {
                                Write(currentC, "A", true);
                                currentC = null;
                            }
                            countC--;
                            countOpC--;
                            if (!streamReaderC.EndOfStream)
                            {
                                currentC = streamReaderC.ReadLine();
                                logger.Log(Level.INFO, $"Считываем из А {currentC}");
                            }
                        }
                        countAllOp--;
                    }
                }
                else
                {
                    if (currentB != null)
                    {
                        Write(currentB, "A", true);
                        currentB = null;
                    }
                    countB--;
                    if (!streamReaderB.EndOfStream)
                    {
                        currentB = streamReaderB.ReadLine();
                        logger.Log(Level.INFO, $"Считываем из А {currentB}");
                    }
                }
            }
            streamReaderB.Close();
            streamReaderC.Close();
        }
        public void Sorting(int columNum)
        {
            columNumSort = columNum;
            bool temp = true;
            countOp = new List<int>();
            countOp.Add(2);
            int sizeTempData = 2;
            ClearFile("B");
            ClearFile("C");
            for (int i = 0; i < countOp.Count; i++)
            {
                logger.Log(Level.INFO, $"Распределение по файлам");
                DistributionFiles(sizeTempData);
                countA = countB + countC;
                if (temp)
                {
                    CountOpSet();
                    temp = false;
                }
                ClearFile("A");
                logger.Log(Level.INFO, $"Слияние В и С");
                Merging(sizeTempData, i);
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
            logger.Log(Level.INFO, $"Отсортированно");
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
        private void Write(string data, string namePath, bool append)
        {
            string path = $"..\\..\\..\\..\\TestMerge\\{namePath}.txt";
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path, append))
                {
                    streamWriter.WriteLine(data);
                    streamWriter.Close();
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
        private string Read(string namePath, long position = 0)
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
                    } while (buffer[0] != 10);
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
            return result;
        }
        private void ClearFile(string namePath)
        {
            string path = $"..\\..\\..\\..\\TestMerge\\{namePath}.txt";
            try
            {
                using (StreamWriter sr = new StreamWriter(path, false))
                {
                    sr.Write("");
                    sr.Close();
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
