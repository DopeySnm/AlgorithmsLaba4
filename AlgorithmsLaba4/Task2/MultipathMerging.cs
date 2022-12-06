using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4.Task2
{
    internal class MultipathMerging
    {
        private Logger logger;
        private long countB;
        private long countC;
        private long countD;
        private int columNumSort;
        public MultipathMerging()
        {
            logger = new Logger("Трёх путевая");
            IMessageHandler fileHandler = new FileHandler("MultipathMergingLog");
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
                            Write(next, "D", true);
                            countD++;
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
                do
                {
                    var a = current.Split("|")[columNumSort - 1];
                    var b = next.Split("|")[columNumSort - 1];
                    if (b.CompareTo(a).Equals(-1))
                    {
                        Write(current, "D", true);
                        countD++;
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
                        Write(current, "D", true);
                        countD++;
                    }
                    if (!streamReader.EndOfStream)
                    {
                        current = next;
                        next = streamReader.ReadLine();
                        logger.Log(Level.INFO, $"Считываем из А {next}");
                    }
                    else
                    {
                        Write(next, "D", true);
                        countD++;
                        streamReader.Close();
                        return exit;
                    }
                } while (true);
            } while (true);

        }
        public void Sorting(int columNumSort)
        {
            this.columNumSort = columNumSort;
            do
            {
                ClearFile("B");
                ClearFile("C");
                ClearFile("D");
                logger.Log(Level.INFO, $"Распределяем файл A");
                bool exit = DistributionFiles();
                if (exit)
                {
                    ClearFile("B");
                    break;// проверка если при распределении по файлам файл C пустой то значит файл A отсортированн
                }
                ClearFile("A");
                StreamReader streamReaderB = new StreamReader($"..\\..\\..\\..\\TestMerge\\B.txt");
                StreamReader streamReaderC = new StreamReader($"..\\..\\..\\..\\TestMerge\\C.txt");
                StreamReader streamReaderD = new StreamReader($"..\\..\\..\\..\\TestMerge\\D.txt");
                string stringPrevB = null;
                string stringPrevC = null;
                string stringPrevD = null;
                string stringCurrentB = streamReaderB.ReadLine();
                logger.Log(Level.INFO, $"Считываем строку {stringCurrentB} с C");
                string stringCurrentC = streamReaderC.ReadLine();
                logger.Log(Level.INFO, $"Считываем строку {stringCurrentC} с C");
                string stringCurrentD = null;
                string prevC = null;
                string prevB = null;
                string prevD = null;
                string currentC = stringCurrentC.Split('|')[columNumSort - 1];
                string currentB = stringCurrentB.Split('|')[columNumSort - 1];
                string currentD = null;
                int valueExit = 2;
                if (streamReaderD.EndOfStream)
                {
                    valueExit = 1;
                    stringCurrentD = streamReaderD.ReadLine();
                    logger.Log(Level.INFO, $"Считываем строку {stringCurrentD} с C");
                    currentD = stringCurrentD.Split('|')[columNumSort - 1];
                }
                int flagExit = 0;
                do
                {
                    if (currentB.CompareTo(currentC).Equals(-1))
                    {
                        if (currentD != null)
                        {
                            logger.Log(Level.INFO, $"Сравниваем {currentB} и {currentC}");
                            if (currentB.CompareTo(currentD).Equals(-1))
                            {
                                if (stringCurrentB != null)
                                {
                                    Write(stringCurrentB, "A", true);
                                    stringCurrentB = null;
                                    countB--;
                                }
                            }
                            else if (currentB.CompareTo(currentD).Equals(0))
                            {
                                if (stringCurrentB != null)
                                {
                                    Write(stringCurrentB, "A", true);
                                    stringCurrentB = null;
                                    countB--;
                                }
                                if (stringCurrentD != null)
                                {
                                    Write(stringCurrentD, "A", true);
                                    stringCurrentD = null;
                                    countD--;
                                }
                            }
                            else
                            {
                                if (stringCurrentD != null)
                                {
                                    Write(stringCurrentD, "A", true);
                                    stringCurrentD = null;
                                    countD--;
                                }
                            }
                        }
                        else
                        {
                            if (stringCurrentB != null)
                            {
                                Write(stringCurrentB, "A", true);
                                stringCurrentB = null;
                                countB--;
                            }
                        }
                    }
                    else if (currentB.CompareTo(currentC).Equals(0))
                    {
                        if (currentD != null)
                        {
                            logger.Log(Level.INFO, $"Сравниваем {currentB} и {currentD}");
                            if (currentB.CompareTo(currentD).Equals(0))
                            {
                                if (stringCurrentD != null)
                                {
                                    Write(stringCurrentD, "A", true);
                                    stringCurrentD = null;
                                    countD--;
                                }
                            }
                        }
                        if (stringCurrentB != null)
                        {
                            Write(stringCurrentB, "A", true);
                            stringCurrentB = null;
                            countB--;
                        }
                        if (stringCurrentC != null)
                        {
                            Write(stringCurrentC, "A", true);
                            stringCurrentC = null;
                            countC--;
                        }
                    }
                    else
                    {
                        if (currentD != null)
                        {
                            logger.Log(Level.INFO, $"Сравниваем {currentC} и {currentD}");
                            if (currentC.CompareTo(currentD).Equals(-1))
                            {
                                if (stringCurrentC != null)
                                {
                                    Write(stringCurrentC, "A", true);
                                    stringCurrentC = null;
                                    countC--;
                                }
                            }
                            else if (currentC.CompareTo(currentD).Equals(0))
                            {
                                if (stringCurrentC != null)
                                {
                                    Write(stringCurrentC, "A", true);
                                    stringCurrentC = null;
                                    countC--;
                                }
                                if (stringCurrentD != null)
                                {
                                    Write(stringCurrentD, "A", true);
                                    stringCurrentD = null;
                                    countD--;
                                }
                            }
                            else
                            {
                                if (stringCurrentD != null)
                                {
                                    Write(stringCurrentD, "A", true);
                                    stringCurrentD = null;
                                    countD--;
                                }
                            }
                        }
                        else
                        {
                            if (stringCurrentC != null)
                            {
                                Write(stringCurrentC, "A", true);
                                stringCurrentC = null;
                                countC--;
                            }
                        }
                    }
                    if (valueExit == 2)
                    {
                        if (stringCurrentD == null)
                        {
                            if (!streamReaderD.EndOfStream)
                            {
                                prevD = currentD;
                                stringCurrentD = streamReaderD.ReadLine();
                                currentD = stringCurrentD.Split('|')[columNumSort - 1];
                                logger.Log(Level.INFO, $"Считываем строку {stringCurrentD}");
                            }
                            else
                            {
                                currentD = null;
                                countD = -1;
                                flagExit++;
                            }
                        }
                    }
                    if (stringCurrentB == null)
                    {
                        if (!streamReaderB.EndOfStream)
                        {
                            prevB = currentB;
                            stringCurrentB = streamReaderB.ReadLine();
                            currentB = stringCurrentB.Split('|')[columNumSort - 1];
                            logger.Log(Level.INFO, $"Считываем строку {stringCurrentB}");
                        }
                        else
                        {
                            stringCurrentB = stringCurrentD;
                            stringCurrentD = null;
                            countB = countD;
                            currentB = currentD;
                            currentD = null;
                            flagExit++;
                        }
                    }
                    if (stringCurrentC == null)
                    {
                        if (!streamReaderC.EndOfStream)
                        {
                            prevC = currentC;
                            stringCurrentC = streamReaderC.ReadLine();
                            currentC = stringCurrentC.Split('|')[columNumSort - 1];
                            logger.Log(Level.INFO, $"Считываем строку {stringCurrentC}");
                        }
                        else
                        {
                            stringCurrentC = stringCurrentD;
                            stringCurrentD = null;
                            countC = countD;
                            currentC = currentD;
                            currentD = null;
                            flagExit++;
                        }
                    }
                    if (flagExit >= valueExit)
                    {
                        break;
                    }
                    flagExit = 0;
                    if (prevD != null)
                    {
                        if (prevD.CompareTo(currentD).Equals(1))
                        {
                            if (prevC == null)
                            {
                                prevC = currentC;
                            }
                            while (!prevC.CompareTo(currentC).Equals(1))
                            {
                                Write(stringCurrentC, "A", true);
                                if (!streamReaderC.EndOfStream)
                                {
                                    stringPrevC = stringCurrentC;
                                    prevC = currentC;
                                    stringCurrentC = streamReaderC.ReadLine();
                                    currentC = stringCurrentC.Split('|')[columNumSort - 1];
                                    logger.Log(Level.INFO, $"Считываем строку {stringCurrentC} с А");
                                }
                                else
                                {
                                    stringCurrentC = null;
                                    break;
                                }
                            }
                            prevC = null;
                            stringPrevC = null;
                            if (prevB == null)
                            {
                                prevB = currentB;
                            }
                            while (!prevB.CompareTo(currentB).Equals(1))
                            {
                                Write(stringCurrentB, "A", true);
                                if (!streamReaderB.EndOfStream)
                                {
                                    stringPrevB = stringCurrentB;
                                    prevB = currentB;
                                    stringCurrentB = streamReaderB.ReadLine();
                                    currentB = stringCurrentB.Split('|')[columNumSort - 1];
                                    logger.Log(Level.INFO, $"Считываем строку {stringCurrentB} с А");
                                }
                                else
                                {
                                    stringCurrentB = null;
                                    break;
                                }
                            }
                            prevB = null;
                            stringPrevB = null;
                            continue;
                        }
                    }
                    if (prevB != null)
                    {
                        if (prevB.CompareTo(currentB).Equals(1))
                        {
                            if (prevC == null)
                            {
                                prevC = currentC;
                            }
                            while (!prevC.CompareTo(currentC).Equals(1))
                            {
                                Write(stringCurrentC, "A", true);
                                if (!streamReaderC.EndOfStream)
                                {
                                    stringPrevC = stringCurrentC;
                                    prevC = currentC;
                                    stringCurrentC = streamReaderC.ReadLine();
                                    currentC = stringCurrentC.Split('|')[columNumSort - 1];
                                    logger.Log(Level.INFO, $"Считываем строку {stringCurrentC} с А");
                                }
                                else
                                {
                                    stringCurrentC = null;
                                    break;
                                }
                            }
                            prevC = null;
                            stringPrevC = null;
                            if (valueExit == 2)
                            {
                                if (prevD == null)
                                {
                                    prevD = currentD;
                                }
                                while (!prevD.CompareTo(currentD).Equals(1))
                                {
                                    Write(stringCurrentD, "A", true);
                                    if (!streamReaderD.EndOfStream)
                                    {
                                        stringPrevD = stringCurrentD;
                                        prevD = currentD;
                                        stringCurrentD = streamReaderD.ReadLine();
                                        currentD = stringCurrentD.Split('|')[columNumSort - 1];
                                        logger.Log(Level.INFO, $"Считываем строку {stringCurrentD} с А");
                                    }
                                    else
                                    {
                                        stringCurrentD = null;
                                        break;
                                    }
                                }
                                prevD = null;
                                stringPrevD = null;
                            }
                            continue;
                        }
                    }
                    if (prevC != null)
                    {
                        if (prevC.CompareTo(currentC).Equals(1))
                        {
                            if (prevB == null)
                            {
                                prevB = currentB;
                            }
                            while (!prevB.CompareTo(currentB).Equals(1))
                            {
                                Write(stringCurrentB, "A", true);
                                if (!streamReaderB.EndOfStream)
                                {
                                    stringPrevB = stringCurrentB;
                                    prevB = currentB;
                                    stringCurrentB = streamReaderB.ReadLine();
                                    currentB = stringCurrentB.Split('|')[columNumSort - 1];
                                    logger.Log(Level.INFO, $"Считываем строку {stringCurrentB} с А");
                                }
                                else
                                {
                                    stringCurrentB = null;
                                    break;
                                }
                            }
                            prevB = null;
                            stringPrevB = null;
                            if (valueExit == 2)
                            {
                                if (prevD == null)
                                {
                                    prevD = currentD;
                                }
                                while (!prevD.CompareTo(currentD).Equals(1))
                                {
                                    Write(stringCurrentD, "A", true);
                                    if (!streamReaderD.EndOfStream)
                                    {
                                        stringPrevD = stringCurrentD;
                                        prevD = currentD;
                                        stringCurrentD = streamReaderD.ReadLine();
                                        currentD = stringCurrentD.Split('|')[columNumSort - 1];
                                        logger.Log(Level.INFO, $"Считываем строку {stringCurrentD} с А");
                                    }
                                    else
                                    {
                                        stringCurrentD = null;
                                        break;
                                    }
                                }
                                prevD = null;
                                stringPrevD = null;
                            }
                            continue;
                        }
                    }
                    logger.Log(Level.INFO, $"Сравниваем {currentB} и {currentC}");
                    
                } while (true);
                while (true)
                {
                    if (stringCurrentD != null)
                    {
                        Write(stringCurrentD, "A", true);
                        stringCurrentD = null;
                    }
                    if (!streamReaderD.EndOfStream)
                    {
                        stringCurrentD = streamReaderD.ReadLine();
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    if (stringCurrentB != null)
                    {
                        Write(stringCurrentB, "A", true);
                        stringCurrentB = null;
                    }
                    if (!streamReaderB.EndOfStream)
                    {
                        stringCurrentB = streamReaderB.ReadLine();
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
                        Write(stringCurrentC, "A", true);
                        stringCurrentC = null;
                    }
                    if (!streamReaderC.EndOfStream)
                    {
                        stringCurrentC = streamReaderC.ReadLine();
                    }
                    else
                    {
                        break;
                    }
                }
                streamReaderB.Close();
                streamReaderC.Close();
                streamReaderD.Close();
            } while (true);
            logger.Log(Level.INFO, $"Отсортированно");
            Console.WriteLine("Готово, нажмите Enter чтобы продолжить");
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
    }
}
