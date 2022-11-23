using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4.Task2
{
    internal class NaturalMerge
    {
        private long maxPositionA = -1;
        private long maxPositionB = 0;
        private long maxPositionC = -1;
        private long positionA = 0;
        private long positionB = 0;
        private long positionC = 0;
        private long countB = 0;
        private long countC = 0;
        private Logger logger;
        public NaturalMerge()
        {
            logger = new Logger("Прямая сортировка");
            IMessageHandler fileHandler = new FileHandler("NaturalMergeLog");
            logger.addMessageHandler(fileHandler);
            logger.SetLevel(Level.INFO);
        }
        public bool Distribution()
        {
            bool exit = true;
            int current = Read("A", positionA);
            int next = 0;
            if (positionA == maxPositionA)
            {
                Write(current, "B", true);
                countB++;
            }
            else
            {
                next = Read("A", positionA);
            }
            do
            {
                do
                {
                    if (positionA == maxPositionA)
                    {
                        if (current.CompareTo(next).Equals(-1) || current.CompareTo(next).Equals(0))
                        {
                            Write(current, "B", true);
                            countB++;
                            Write(next, "B", true);
                            countB++;
                            break;
                        }
                        else
                        {
                            Write(current, "B", true);
                            countB++;
                            Write(next, "C", true);
                            countC++;
                            exit = false;
                            break;
                        }
                    }
                    if (current.CompareTo(next).Equals(-1) || current.CompareTo(next).Equals(0))
                    {
                        Write(current, "B", true);
                        countB++;
                        current = next;
                        next = Read("A", positionA);
                    }
                    else
                    {
                        Write(current, "B", true);
                        countB++;
                        current = next;
                        next = Read("A", positionA);
                        break;
                    }
                } while (positionA != maxPositionA);
                if (positionA == maxPositionA)
                {
                    if (current.CompareTo(next).Equals(-1) || current.CompareTo(next).Equals(0))
                    {
                        Write(current, "B", true);
                        countB++;
                        Write(next, "B", true);
                        countB++;
                        break;
                    }
                    else
                    {
                        Write(current, "B", true);
                        countB++;
                        Write(next, "C", true);
                        countC++;
                        exit = false;
                        break;
                    }
                }
                do
                {
                    if (positionA == maxPositionA)
                    {
                        if (current.CompareTo(next).Equals(-1))
                        {
                            Write(current, "C", true);
                            countC++;
                            Write(next, "C", true);
                            countC++;
                            exit = false;
                            break;
                        }
                        else
                        {
                            Write(current, "C", true);
                            countC++;
                            Write(next, "B", true);
                            countB++;
                            exit = false;
                            break;
                        }
                    }
                    if (current.CompareTo(next).Equals(-1))
                    {
                        Write(current, "C", true);
                        countC++;
                        current = next;
                        next = Read("A", positionA);
                        exit = false;
                    }
                    else
                    {
                        Write(current, "C", true);
                        countC++;
                        current = next;
                        next = Read("A", positionA);
                        exit = false;
                        break;
                    }
                } while (positionA != maxPositionA);
                if (positionA == maxPositionA)
                {
                    if (current.CompareTo(next).Equals(-1))
                    {
                        Write(current, "C", true);
                        countC++;
                        Write(next, "C", true);
                        countC++;
                        exit = false;
                        break;
                    }
                    else
                    {
                        Write(current, "C", true);
                        countC++;
                        Write(next, "B", true);
                        countB++;
                        exit = false;
                        break;
                    }
                }
            } while (positionA != maxPositionA);
            return exit;
        }
        public void Sorting()
        {
            do
            {
                ClearFile("B");
                ClearFile("C");
                bool exit = Distribution();
                if (exit)
                {
                    break;// проверка если при распределении по файлам файл C пустой то значит файл A отсортированн
                }
                ClearFile("A");
                int currentB = Read("B", positionB);
                int currentC = Read("C", positionC);
                do
                {
                    if (currentB.CompareTo(currentC).Equals(-1))
                    {
                        if (countB != 0)
                        {
                            Write(currentB, "A", true);
                            countB--;
                        }
                        if (positionB != maxPositionB)
                        {
                            currentB = Read("B", positionB);
                        }
                    }
                    else
                    {
                        if (countC != 0)
                        {
                            Write(currentC, "A", true);
                            countC--;
                        }
                        if (positionC != maxPositionC)
                        {
                            currentC = Read("C", positionC);
                        }
                    }

                } while ((countC != 0) && (countB != 0));
                if (countB != 0)
                {
                    for (int i = 0; i < countB; i++)
                    {
                        Write(currentB, "A", true);
                        if (positionB != maxPositionB)
                        {
                            currentB = Read("B", positionB);
                        }
                    }
                    countB = 0;
                }
                if ((countC != 0))
                {
                    for (int i = 0; i < countC; i++)
                    {
                        Write(currentC, "A", true);
                        if (positionC != maxPositionC)
                        {
                            currentC = Read("C", positionC);
                        }
                    }
                    countC = 0;
                }
                positionA = 0;
                positionB = 0;
                positionC = 0;
            } while (true);
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
        private int Read(string namePath, long position = 0)
        {
            string path = $"..\\..\\..\\..\\TestMerge\\{namePath}.txt";
            string result = "";
            try
            {
                using (FileStream sr = new FileStream(path, FileMode.Open))
                {
                    byte[] buffer = new byte[1];
                    sr.Seek(position, SeekOrigin.Current);
                    string temp = "";
                    do
                    {
                        var a = sr.Read(buffer, 0, 1);
                        char converToChar = (char)buffer[0];
                        if (converToChar == '\0')
                        {
                            throw new Exception();
                        }
                        temp += converToChar;
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
    }
}
