using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
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
        private int columNumSort;
        public NaturalMerge()
        {
            logger = new Logger("Естественная");
            IMessageHandler fileHandler = new FileHandler("NaturalMergeLog");
            logger.addMessageHandler(fileHandler);
            logger.SetLevel(Level.INFO);
        }
        private bool DistributionFiles()
        {
            StreamReader streamReader = new StreamReader($"..\\..\\..\\..\\TestMerge\\A.txt");
            bool exit = true;
            string current = streamReader.ReadLine();
            logger.Log(Level.INFO, $"Считываем из А {current}");
            string next = streamReader.ReadLine();
            logger.Log(Level.INFO, $"Считываем из А {next}");
            do
            {
                do
                {
                    var a = current.Split("|")[columNumSort - 1];
                    var b = next.Split("|")[columNumSort - 1];
                    if (b.CompareTo(a).Equals(-1))
                    {
                        Write(current, "B", true);
                        countB++;
                        exit = false;
                        if (!streamReader.EndOfStream)
                        {
                            current = next;
                            next = streamReader.ReadLine();
                            logger.Log(Level.INFO, $"Считываем из А {next}");
                        }
                        else
                        {
                            Write(next, "C", true);
                            countC++;
                            streamReader.Close();
                            return exit;
                        }
                        break;
                    }
                    else
                    {
                        Write(current, "B", true);
                        countB++;
                    }
                    if (!streamReader.EndOfStream)
                    {
                        current = next;
                        next = streamReader.ReadLine();
                        logger.Log(Level.INFO, $"Считываем из А {next}");
                    }
                    else
                    {
                        Write(next, "B", true);
                        countB++;
                        streamReader.Close();
                        return exit;
                    }
                } while (true);
                do
                {
                    var a = current.Split("|")[columNumSort - 1];
                    var b = next.Split("|")[columNumSort - 1];
                    if (b.CompareTo(a).Equals(-1))
                    {
                        Write(current, "C", true);
                        countC++;
                        if (!streamReader.EndOfStream)
                        {
                            current = next;
                            next = streamReader.ReadLine();
                            logger.Log(Level.INFO, $"Считываем из А {next}");
                        }
                        else
                        {
                            Write(next, "B", true);
                            countB++;
                            streamReader.Close();
                            return exit;
                        }
                        break;
                    }
                    else
                    {
                        Write(current, "C", true);
                        countC++;
                    }
                    if (!streamReader.EndOfStream)
                    {
                        current = next;
                        next = streamReader.ReadLine();
                        logger.Log(Level.INFO, $"Считываем из А {next}");
                    }
                    else
                    {
                        Write(next, "C", true);
                        countC++;
                        streamReader.Close();
                        return exit;
                    }
                } while (true);
            } while (true);
            //int current = Read("A", positionA);
            //int next = 0;
            //if (positionA == maxPositionA)
            //{
            //    Write(current, "B", true);
            //    countB++;
            //}
            //else
            //{
            //    next = Read("A", positionA);
            //}
            //do
            //{
            //    do
            //    {
            //        if (positionA == maxPositionA)
            //        {
            //            if (current.CompareTo(next).Equals(-1) || current.CompareTo(next).Equals(0))
            //            {
            //                Write(current, "B", true);
            //                countB++;
            //                Write(next, "B", true);
            //                countB++;
            //                break;
            //            }
            //            else
            //            {
            //                Write(current, "B", true);
            //                countB++;
            //                Write(next, "C", true);
            //                countC++;
            //                exit = false;
            //                break;
            //            }
            //        }
            //        if (current.CompareTo(next).Equals(-1) || current.CompareTo(next).Equals(0))
            //        {
            //            Write(current, "B", true);
            //            countB++;
            //            current = next;
            //            next = Read("A", positionA);
            //        }
            //        else
            //        {
            //            Write(current, "B", true);
            //            countB++;
            //            current = next;
            //            next = Read("A", positionA);
            //            break;
            //        }
            //    } while (positionA != maxPositionA);
            //    if (positionA == maxPositionA)
            //    {
            //        if (current.CompareTo(next).Equals(-1) || current.CompareTo(next).Equals(0))
            //        {
            //            Write(current, "B", true);
            //            countB++;
            //            Write(next, "B", true);
            //            countB++;
            //            break;
            //        }
            //        else
            //        {
            //            Write(current, "B", true);
            //            countB++;
            //            Write(next, "C", true);
            //            countC++;
            //            exit = false;
            //            break;
            //        }
            //    }
            //    do
            //    {
            //        if (positionA == maxPositionA)
            //        {
            //            if (current.CompareTo(next).Equals(-1))
            //            {
            //                Write(current, "C", true);
            //                countC++;
            //                Write(next, "C", true);
            //                countC++;
            //                exit = false;
            //                break;
            //            }
            //            else
            //            {
            //                Write(current, "C", true);
            //                countC++;
            //                Write(next, "B", true);
            //                countB++;
            //                exit = false;
            //                break;
            //            }
            //        }
            //        if (current.CompareTo(next).Equals(-1))
            //        {
            //            Write(current, "C", true);
            //            countC++;
            //            current = next;
            //            next = Read("A", positionA);
            //            exit = false;
            //        }
            //        else
            //        {
            //            Write(current, "C", true);
            //            countC++;
            //            current = next;
            //            next = Read("A", positionA);
            //            exit = false;
            //            break;
            //        }
            //    } while (positionA != maxPositionA);
            //    if (positionA == maxPositionA)
            //    {
            //        if (current.CompareTo(next).Equals(-1))
            //        {
            //            Write(current, "C", true);
            //            countC++;
            //            Write(next, "C", true);
            //            countC++;
            //            exit = false;
            //            break;
            //        }
            //        else
            //        {
            //            Write(current, "C", true);
            //            countC++;
            //            Write(next, "B", true);
            //            countB++;
            //            exit = false;
            //            break;
            //        }
            //    }
            //} while (positionA != maxPositionA);
        }
        public T GetValue<T>(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        public void Sorting(int columNum)
        {
            columNumSort = columNum;
            do
            {
                ClearFile("B");
                ClearFile("C");
                bool exit = DistributionFiles();
                if (exit)
                {
                    ClearFile("B");
                    break;// проверка если при распределении по файлам файл C пустой то значит файл A отсортированн
                }
                ClearFile("A");
                StreamReader streamReaderB = new StreamReader($"..\\..\\..\\..\\TestMerge\\B.txt");
                StreamReader streamReaderC = new StreamReader($"..\\..\\..\\..\\TestMerge\\C.txt");
                string currentB = null;
                string currentC = null;
                string c = null;
                string b = null;
                int flagExit = 0;
                do
                {
                    if (currentB == null)
                    {
                        if (!streamReaderB.EndOfStream)
                        {
                            currentB = streamReaderB.ReadLine();
                            b = currentB.Split('|')[columNumSort - 1];
                            logger.Log(Level.INFO, $"Считываем строку {currentB} с А");
                        }
                        else
                        {
                            flagExit++;
                        }
                    }
                    if (currentC == null)
                    {
                        if (!streamReaderC.EndOfStream)
                        {
                            currentC = streamReaderC.ReadLine();
                            c = currentC.Split('|')[columNumSort - 1];
                            logger.Log(Level.INFO, $"Считываем строку {currentC} с А");
                        }
                        else
                        {
                            flagExit++;
                        }
                    }
                    if (flagExit >= 1)
                    {
                        break;
                    }
                    flagExit = 0;
                    //T b1 = GetValue<T>(b);
                    //T c1 = GetValue<T>(c);
                    logger.Log(Level.INFO, $"Сравниваем {b} и {c}");
                    if (b.CompareTo(c).Equals(-1))
                    {
                        if (currentB != null)
                        {
                            Write(currentB, "A", true);
                            currentB = null;
                            countB--;
                        }
                    }
                    else if (b.CompareTo(c).Equals(0))
                    {
                        if (currentB != null)
                        {
                            Write(currentB, "A", true);
                            currentB = null;
                            countB--;
                        }
                        if (currentC != null)
                        {
                            Write(currentC, "A", true);
                            currentC = null;
                            countC--;
                        }
                    }
                    else
                    {
                        if (currentC != null)
                        {
                            Write(currentC, "A", true);
                            currentC = null;
                            countC--;
                        }
                    }
                } while (true);
                //while (!streamReaderB.EndOfStream)
                //{
                //    if (currentB != null)
                //    {
                //        Write(currentB, "A", true);
                //        currentB = null;
                //    }
                //    if (!streamReaderB.EndOfStream)
                //    {
                //        currentB = streamReaderB.ReadLine();
                //    }
                //}
                while (true)
                {
                    if (currentB != null)
                    {
                        Write(currentB, "A", true);
                        currentB = null;
                    }
                    if (!streamReaderB.EndOfStream)
                    {
                        currentB = streamReaderB.ReadLine();
                        logger.Log(Level.INFO, $"Считываем строку {currentB} с А");
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    if (currentC != null)
                    {
                        Write(currentC, "A", true);
                        currentC = null;
                    }
                    if (!streamReaderC.EndOfStream)
                    {
                        currentC = streamReaderC.ReadLine();
                        logger.Log(Level.INFO, $"Считываем строку {currentC} с А");
                    }
                    else
                    {
                        break;
                    }
                }
                streamReaderB.Close();
                streamReaderC.Close();
                positionA = 0;
                positionB = 0;
                positionC = 0;
            } while (true);
            logger.Log(Level.INFO, $"Отсортированно");
            Console.WriteLine("Готово, нажмите Enter чтобы продолжить");
        }
        private void ClearFile(string namePath)
        {
            logger.Log(Level.INFO, $"Очищаем файл{namePath}");
            string path = $"..\\..\\..\\..\\TestMerge\\{namePath}.txt";
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path, false))
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
    }
}
