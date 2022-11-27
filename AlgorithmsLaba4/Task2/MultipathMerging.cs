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
            logger = new Logger("Прямая сортировка");
            IMessageHandler fileHandler = new FileHandler("NaturalMergeLog");
            logger.addMessageHandler(fileHandler);
            logger.SetLevel(Level.INFO);
        }
        private bool DistributionFiles()
        {
            StreamReader streamReader = new StreamReader($"..\\..\\..\\..\\TestMerge\\A.txt");
            bool exit = true;
            string current = streamReader.ReadLine();
            string next = streamReader.ReadLine();
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
                string currentB = null;
                string currentC = null;
                string currentD = null;
                int valueExit = 2;
                if (streamReaderD.EndOfStream)
                {
                    valueExit = 1;
                }
                int flagExit = 0;
                string c = null;
                string b = null;
                string d = null;
                do
                {
                    if (valueExit == 2)
                    {
                        if (currentD == null)
                        {
                            if (!streamReaderD.EndOfStream)
                            {

                                currentD = streamReaderD.ReadLine();
                                d = currentD.Split('|')[columNumSort - 1];
                            }
                            else
                            {
                                d = null;
                                countD = -1;
                                flagExit++;
                            }
                        }
                    }
                    if (currentB == null)
                    {
                        if (!streamReaderB.EndOfStream)
                        {
                            currentB = streamReaderB.ReadLine();
                            b = currentB.Split('|')[columNumSort - 1];
                        }
                        else
                        {
                            currentB = currentD;
                            currentD = null;
                            countB = countD;
                            b = d;
                            d = null;
                            flagExit++;
                        }
                    }
                    if (currentC == null)
                    {
                        if (!streamReaderC.EndOfStream)
                        {
                            currentC = streamReaderC.ReadLine();
                            c = currentC.Split('|')[columNumSort - 1];
                        }
                        else
                        {
                            currentC = currentD;
                            currentD = null;
                            countC = countD;
                            c = d;
                            d = null;
                            flagExit++;
                        }
                    }
                    if (flagExit >= valueExit)
                    {
                        break;
                    }
                    flagExit = 0;
                    if (b.CompareTo(c).Equals(-1))
                    {
                        if (d != null)
                        {
                            if (b.CompareTo(d).Equals(-1))
                            {
                                if (currentB != null)
                                {
                                    Write(currentB, "A", true);
                                    currentB = null;
                                    countB--;
                                }
                            }
                            else if (b.CompareTo(d).Equals(0))
                            {
                                if (currentB != null)
                                {
                                    Write(currentB, "A", true);
                                    currentB = null;
                                    countB--;
                                }
                                if (currentD != null)
                                {
                                    Write(currentD, "A", true);
                                    currentD = null;
                                    countD--;
                                }
                            }
                            else
                            {
                                if (currentD != null)
                                {
                                    Write(currentD, "A", true);
                                    currentD = null;
                                    countD--;
                                }
                            }
                        }
                        else
                        {
                            if (currentB != null)
                            {
                                Write(currentB, "A", true);
                                currentB = null;
                                countB--;
                            }
                        }
                    }
                    else if (b.CompareTo(c).Equals(0))
                    {
                        if (d != null)
                        {
                            if (b.CompareTo(d).Equals(0))
                            {
                                if (currentD != null)
                                {
                                    Write(currentD, "A", true);
                                    currentD = null;
                                    countD--;
                                }
                            }
                        }
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
                        if (d != null)
                        {
                            if (c.CompareTo(d).Equals(-1))
                            {
                                if (currentC != null)
                                {
                                    Write(currentC, "A", true);
                                    currentC = null;
                                    countC--;
                                }
                            }
                            else if (c.CompareTo(d).Equals(0))
                            {
                                if (currentC != null)
                                {
                                    Write(currentC, "A", true);
                                    currentC = null;
                                    countC--;
                                }
                                if (currentD != null)
                                {
                                    Write(currentD, "A", true);
                                    currentD = null;
                                    countD--;
                                }
                            }
                            else
                            {
                                if (currentD != null)
                                {
                                    Write(currentD, "A", true);
                                    currentD = null;
                                    countD--;
                                }
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
                    }
                } while (true);
                while (true)
                {
                    if (currentD != null)
                    {
                        Write(currentD, "A", true);
                        currentD = null;
                    }
                    if (!streamReaderD.EndOfStream)
                    {
                        currentD = streamReaderD.ReadLine();
                    }
                    else
                    {
                        break;
                    }
                }
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
