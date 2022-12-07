using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AlgorithmsLaba4.Task2
{
    internal class NaturalMerge
    {
        private Logger logger;
        private int columNumSort;
        public NaturalMerge()
        {
            logger = new Logger("Естественная");
            IMessageHandler fileHandler = new FileHandler("NaturalMergeLog");
            IMessageHandler consoleHandler = new ConsoleHandler();
            logger.addMessageHandler(fileHandler);
            logger.addMessageHandler(consoleHandler);
            logger.SetLevel(Level.INFO);
        }
        private bool CheckExit(string readFileName)
        {
            logger.Log(Level.INFO, $"Проверка выхода");
            StreamReader streamReader = new StreamReader($"..\\..\\..\\..\\TestMerge\\{readFileName}.txt");
            string current = streamReader.ReadLine();
            string prev = null;
            while (!streamReader.EndOfStream)
            {
                prev = current;
                current = streamReader.ReadLine();
                var a = prev.Split("|")[columNumSort - 1];
                var b = current.Split("|")[columNumSort - 1];
                if (a.CompareTo(b).Equals(1))
                {
                    streamReader.Close();
                    return false;
                }
            }
            streamReader.Close();
            return true;
        }
        private bool DistributionFiles(string readFileName)
        {
            logger.Log(Level.INFO, $"Проверка выхода");
            StreamReader streamReader = new StreamReader($"..\\..\\..\\..\\TestMerge\\{readFileName}.txt");
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
                            streamReader.Close();
                            return exit;
                        }
                        break;
                    }
                    else
                    {
                        Write(current, "B", true);
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
                        if (!streamReader.EndOfStream)
                        {
                            current = next;
                            next = streamReader.ReadLine();
                            logger.Log(Level.INFO, $"Считываем из А {next}");
                        }
                        else
                        {
                            Write(next, "B", true);
                            streamReader.Close();
                            return exit;
                        }
                        break;
                    }
                    else
                    {
                        Write(current, "C", true);
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
        public void Sorting(int columNum)
        {
            string readFileName = "A";
            string writeFileName = "D";
            ClearFile(writeFileName);
            columNumSort = columNum;
            do
            {
                if (CheckExit(readFileName))
                {
                    ClearFile("B");
                    if (!readFileName.Equals("A"))
                    {
                        StreamReader streamReader = new StreamReader($"..\\..\\..\\..\\TestMerge\\" + readFileName + ".txt");
                        while (!streamReader.EndOfStream)
                        {
                            Write(streamReader.ReadToEnd(), "A", true);
                        }
                        streamReader.Close();
                        ClearFile(readFileName);
                    }
                    break;// проверка если при распределении по файлам файл C пустой то значит файл A отсортированн
                }
                StreamReader streamReaderA = new StreamReader($"..\\..\\..\\..\\TestMerge\\" + readFileName + ".txt");
                string prev = null;
                string current = null;
                string prevString = null;
                string currentString = null;
                if (!streamReaderA.EndOfStream)
                {
                    currentString = streamReaderA.ReadLine();
                    logger.Log(Level.INFO, $"Считываем строку {currentString} с {readFileName}");
                }
                do
                {
                    ClearFile("B");
                    ClearFile("C");
                    if (currentString != null)
                    {
                        Write(currentString, "B", true);
                    }
                    prevString = currentString;
                    if (!streamReaderA.EndOfStream)
                    {
                        currentString = streamReaderA.ReadLine();
                        logger.Log(Level.INFO, $"Считываем строку {currentString} с {readFileName}");
                        prev = prevString.Split('|')[columNumSort - 1];
                        current = currentString.Split('|')[columNumSort - 1];
                    }
                    else
                    {
                        currentString = null;
                    }
                    while (!prev.CompareTo(current).Equals(1))
                    {
                        if (currentString != null)
                        {
                            Write(currentString, "B", true);
                        }
                        prevString = currentString;
                        prev = current;
                        if (!streamReaderA.EndOfStream)
                        {
                            currentString = streamReaderA.ReadLine();
                            logger.Log(Level.INFO, $"Считываем строку {currentString} с {readFileName}");
                            current = currentString.Split('|')[columNumSort - 1];
                        }
                        else
                        {
                            currentString = null;
                            break;
                        }
                    }
                    if (currentString != null)
                    {
                        Write(currentString, "C", true);
                    }
                    prevString = currentString;
                    prev = current;
                    if (!streamReaderA.EndOfStream)
                    {
                        currentString = streamReaderA.ReadLine();
                        logger.Log(Level.INFO, $"Считываем строку {currentString} с {readFileName}");
                        current = currentString.Split('|')[columNumSort - 1];
                    }
                    else
                    {
                        currentString = null;
                    }
                    while (!prev.CompareTo(current).Equals(1))
                    {
                        if (currentString != null)
                        {
                            Write(currentString, "C", true);
                        }
                        prevString = currentString;
                        prev = current;
                        if (!streamReaderA.EndOfStream)
                        {
                            currentString = streamReaderA.ReadLine();
                            logger.Log(Level.INFO, $"Считываем строку {currentString} с {readFileName}");
                            current = currentString.Split('|')[columNumSort - 1];
                        }
                        else
                        {
                            currentString = null;
                            break;
                        }
                    }
                    logger.Log(Level.INFO, $"Сливаем В и С");
                    Megring(writeFileName);
                } while (!streamReaderA.EndOfStream);
                streamReaderA.Close();
                ClearFile(readFileName);
                var temp = readFileName;
                readFileName = writeFileName;
                writeFileName = temp;
            } while (true);
            //ClearFile("B");
            //ClearFile("C");
            //bool exit = DistributionFiles();
            //if (exit)
            //{
            //    ClearFile("B");
            //    break;// проверка если при распределении по файлам файл C пустой то значит файл A отсортированн
            //}
            //ClearFile("A");
            //StreamReader streamReaderB = new StreamReader($"..\\..\\..\\..\\TestMerge\\B.txt");
            //StreamReader streamReaderC = new StreamReader($"..\\..\\..\\..\\TestMerge\\C.txt");
            //string stringCurrentB = streamReaderB.ReadLine();
            //logger.Log(Level.INFO, $"Считываем строку {stringCurrentB} с C");
            //string stringPrevB = null;
            //string stringCurrentC = streamReaderC.ReadLine();
            //logger.Log(Level.INFO, $"Считываем строку {stringCurrentC} с B");
            //string stringPrevC = null;
            //string currentC = stringCurrentC.Split('|')[columNumSort - 1];
            //string prevC = null;
            //string currentB = stringCurrentB.Split('|')[columNumSort - 1];
            //string prevB = null;
            //do
            //{
            //    if (currentB.CompareTo(currentC).Equals(-1))
            //    {
            //        if (stringCurrentB != null)
            //        {
            //            Write(stringCurrentB, "A", true);
            //            stringCurrentB = null;
            //        }
            //    }
            //    else if (currentB.CompareTo(currentC).Equals(0))
            //    {
            //        if (stringCurrentB != null)
            //        {
            //            Write(stringCurrentB, "A", true);
            //            stringCurrentB = null;
            //        }
            //        if (stringCurrentC != null)
            //        {
            //            Write(stringCurrentC, "A", true);
            //            stringCurrentC = null;
            //        }
            //    }
            //    else
            //    {
            //        if (stringCurrentC != null)
            //        {
            //            Write(stringCurrentC, "A", true);
            //            stringCurrentC = null;
            //        }
            //    }
            //    if (stringCurrentB == null)
            //    {
            //        if (!streamReaderB.EndOfStream)
            //        {
            //            stringPrevB = stringCurrentB;
            //            prevB = currentB;
            //            stringCurrentB = streamReaderB.ReadLine();
            //            currentB = stringCurrentB.Split('|')[columNumSort - 1];
            //            logger.Log(Level.INFO, $"Считываем строку {stringCurrentB} с А");
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }
            //    if (stringCurrentC == null)
            //    {
            //        if (!streamReaderC.EndOfStream)
            //        {
            //            stringPrevC = stringCurrentC;
            //            prevC = currentC;
            //            stringCurrentC = streamReaderC.ReadLine();
            //            currentC = stringCurrentC.Split('|')[columNumSort - 1];
            //            logger.Log(Level.INFO, $"Считываем строку {stringCurrentC} с А");
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }
            //    if (prevB != null)
            //    {
            //        if (prevB.CompareTo(currentB).Equals(1))
            //        {
            //            if (prevC == null)
            //            {
            //                prevC = currentC;
            //            }
            //            while (!prevC.CompareTo(currentC).Equals(1))
            //            {
            //                Write(stringCurrentC, "A", true);
            //                if (!streamReaderC.EndOfStream)
            //                {
            //                    stringPrevC = stringCurrentC;
            //                    prevC = currentC;
            //                    stringCurrentC = streamReaderC.ReadLine();
            //                    currentC = stringCurrentC.Split('|')[columNumSort - 1];
            //                    logger.Log(Level.INFO, $"Считываем строку {stringCurrentC} с А");
            //                }
            //                else
            //                {
            //                    stringCurrentC = null;
            //                    break;
            //                }
            //            }
            //            prevC = null;
            //            stringPrevC = null;
            //            continue;
            //        }
            //    }
            //    if (prevC != null)
            //    {
            //        if (prevC.CompareTo(currentC).Equals(1))
            //        {
            //            if (prevB == null)
            //            {
            //                prevB = currentB;
            //            }
            //            while (!prevB.CompareTo(currentB).Equals(1))
            //            {
            //                Write(stringCurrentB, "A", true);
            //                if (!streamReaderB.EndOfStream)
            //                {
            //                    stringPrevB = stringCurrentB;
            //                    prevB = currentB;
            //                    stringCurrentB = streamReaderB.ReadLine();
            //                    currentB = stringCurrentB.Split('|')[columNumSort - 1];
            //                    logger.Log(Level.INFO, $"Считываем строку {stringCurrentB} с А");
            //                }
            //                else
            //                {
            //                    stringCurrentB = null;
            //                    break;
            //                }
            //            }
            //            prevB = null;
            //            stringPrevB = null;
            //        }
            //    }
            //} while (true);
            //while (true)
            //{
            //    if (stringCurrentB != null)
            //    {
            //        Write(stringCurrentB, "A", true);
            //        stringCurrentB = null;
            //    }
            //    if (!streamReaderB.EndOfStream)
            //    {
            //        stringCurrentB = streamReaderB.ReadLine();
            //        logger.Log(Level.INFO, $"Считываем строку {stringCurrentB} с А");
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            //while (true)
            //{
            //    if (stringCurrentC != null)
            //    {
            //        Write(stringCurrentC, "A", true);
            //        stringCurrentC = null;
            //    }
            //    if (!streamReaderC.EndOfStream)
            //    {
            //        stringCurrentC = streamReaderC.ReadLine();
            //        logger.Log(Level.INFO, $"Считываем строку {stringCurrentC} с А");
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            //streamReaderB.Close();
            //streamReaderC.Close();
            /*} while (true)*/;
            logger.Log(Level.INFO, $"Отсортированно");
            Console.WriteLine("Готово, нажмите Enter чтобы продолжить");
        }
        private void Megring(string writeFileName)
        {
            StreamReader streamReaderB = new StreamReader($"..\\..\\..\\..\\TestMerge\\B.txt");
            StreamReader streamReaderC = new StreamReader($"..\\..\\..\\..\\TestMerge\\C.txt");
            string stringCurrentB = streamReaderB.ReadLine();
            string currentB = stringCurrentB.Split('|')[columNumSort - 1];
            logger.Log(Level.INFO, $"Считываем строку {stringCurrentB} с B");
            string stringCurrentC = null;
            string currentC = null;
            if (!streamReaderC.EndOfStream)
            {
                stringCurrentC = streamReaderC.ReadLine();
                currentC = stringCurrentC.Split('|')[columNumSort - 1];
                logger.Log(Level.INFO, $"Считываем строку {stringCurrentC} с C");
                do
                {
                    logger.Log(Level.INFO, $"Сравниваем строку {currentB} и {currentC}");
                    if (currentB.CompareTo(currentC).Equals(-1))
                    {
                        if (stringCurrentB != null)
                        {
                            Write(stringCurrentB, writeFileName, true);
                            stringCurrentB = null;
                        }
                    }
                    else if (currentB.CompareTo(currentC).Equals(0))
                    {
                        if (stringCurrentB != null)
                        {
                            Write(stringCurrentB, writeFileName, true);
                            stringCurrentB = null;
                        }
                        if (stringCurrentC != null)
                        {
                            Write(stringCurrentC, writeFileName, true);
                            stringCurrentC = null;
                        }
                    }
                    else
                    {
                        if (stringCurrentC != null)
                        {
                            Write(stringCurrentC, writeFileName, true);
                            stringCurrentC = null;
                        }
                    }
                    if (stringCurrentB == null)
                    {
                        if (!streamReaderB.EndOfStream)
                        {
                            stringCurrentB = streamReaderB.ReadLine();
                            currentB = stringCurrentB.Split('|')[columNumSort - 1];
                            logger.Log(Level.INFO, $"Считываем строку {stringCurrentB} с А");
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (stringCurrentC == null)
                    {
                        if (!streamReaderC.EndOfStream)
                        {
                            stringCurrentC = streamReaderC.ReadLine();
                            currentC = stringCurrentC.Split('|')[columNumSort - 1];
                            logger.Log(Level.INFO, $"Считываем строку {stringCurrentC} с А");
                        }
                        else
                        {
                            break;
                        }
                    }
                } while (true);
            }
            while (true)
            {
                if (stringCurrentB != null)
                {
                    Write(stringCurrentB, writeFileName, true);
                    stringCurrentB = null;
                }
                if (!streamReaderB.EndOfStream)
                {
                    stringCurrentB = streamReaderB.ReadLine();
                    logger.Log(Level.INFO, $"Считываем строку {stringCurrentB} с А");
                }
                else
                {
                    break;
                }
            }
            while (true)
            {
                if (stringCurrentC != null)
                {
                    Write(stringCurrentC, writeFileName, true);
                    stringCurrentC = null;
                }
                if (!streamReaderC.EndOfStream)
                {
                    stringCurrentC = streamReaderC.ReadLine();
                    logger.Log(Level.INFO, $"Считываем строку {stringCurrentC} с А");
                }
                else
                {
                    break;
                }
            }
            streamReaderB.Close();
            streamReaderC.Close();
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
        //private int Read(string namePath, long position = 0)
        //{
        //    string path = $"..\\..\\..\\..\\TestMerge\\{namePath}.txt";
        //    string result = "";
        //    try
        //    {
        //        using (FileStream sr = new FileStream(path, FileMode.Open))
        //        {
        //            byte[] buffer = new byte[1];
        //            sr.Seek(position, SeekOrigin.Current);
        //            string temp = "";
        //            do
        //            {
        //                var a = sr.Read(buffer, 0, 1);
        //                char converToChar = (char)buffer[0];
        //                if (converToChar == '\0')
        //                {
        //                    throw new Exception();
        //                }
        //                temp += converToChar;
        //            } while (buffer[0] != 32);
        //            result = temp;
        //            logger.Log(Level.INFO, $"Элемент {result} считался из файла {namePath}");
        //            if (namePath.Equals("A"))
        //            {
        //                positionA = sr.Position;
        //                maxPositionA = sr.Length;
        //            }
        //            else if (namePath.Equals("B"))
        //            {
        //                positionB = sr.Position;
        //                maxPositionB = sr.Length;
        //            }
        //            else if (namePath.Equals("C"))
        //            {
        //                positionC = sr.Position;
        //                maxPositionC = sr.Length;
        //            }
        //            sr.Close();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("The file could not be read:");
        //        Console.WriteLine(e.Message);
        //        throw new Exception("Неверный формат");
        //    }
        //    return int.Parse(result);
        //}
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
