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
        private bool popB = false;
        private bool popC = false;
        private int countA = 0;
        private List<int> countOp;
        private Logger logger;

        private int countRound = 0;

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
            int dataWrite;
            NaturalMerge naturalMerge = new NaturalMerge();
            bool exit = naturalMerge.Distribution();
            if (exit)
            {
                countOp.Clear();
            }
            ClearFile("B");
            ClearFile("C");
            for (int i = 0; i < countOp.Count; i++)
            {
                do
                {
                    for (int j = 0; j < sizeTempData / 2; j++)
                    {
                        if (positionA == maxPositionA)
                        {
                            break;
                        }
                        dataWrite = Read("A", positionA);
                        Write(dataWrite, "B", true);
                        countA++;
                    }
                    if (maxPositionA == positionA)
                    {
                        break;
                    }
                    for (int j = 0; j < sizeTempData / 2; j++)
                    {
                        if (positionA == maxPositionA)
                        {
                            break;
                        }
                        dataWrite = Read("A", positionA);
                        Write(dataWrite, "C", true);
                        countA++;
                    }

                } while (maxPositionA != positionA);
                if (temp)
                {
                    CountOpSet();
                    temp = false;
                }
                ClearFile("A");
                int firstRead = Read("B", positionB);
                popB = true;
                int secondRead = 0;
                // && maxPositionA > 10900
                if (countRound == 9)
                {
                    Console.WriteLine();
                }
                countRound++;
                while ((maxPositionB != positionB) || (maxPositionC != positionC))
                {
                    if (popB == false)
                    {
                        if (maxPositionB != positionB)
                        {
                            firstRead = Read("B", positionB);
                            popB = true;
                        }
                    }
                    if (maxPositionC != positionC)
                    {
                        int countAllOp = countOp[i];
                        int countOpB = sizeTempData / 2;
                        int countOpC = sizeTempData / 2;
                        if (i == countOp.Count - 1 && countA > 2)
                        {
                            countOpC = countOp[i] - countOp[i - 1];
                        }
                        for (; countAllOp > 0;)
                        {
                            if (popC == false)
                            {
                                if (positionC != maxPositionC)
                                {
                                    secondRead = Read("C", positionC);
                                    popC = true;
                                }
                            }
                            if (firstRead.CompareTo(secondRead).Equals(-1))
                            {
                                Write(firstRead, "A", true);
                                popB = false;
                                countOpB--;
                                if (popB == false && countOpB != 0)
                                {
                                    if (maxPositionB != positionB)
                                    {
                                        firstRead = Read("B", positionB);
                                        popB = true;
                                    }
                                }
                            }
                            else
                            {
                                if (popC == true)
                                {
                                    Write(secondRead, "A", true);
                                    popC = false;
                                }
                                countOpC--;
                            }
                            countAllOp--;
                            if (countOpB == 0 || countOpC == 0)
                            {
                                break;
                            }
                        }
                        for (; countAllOp > 0;)
                        {
                            if (countOpB > 0)
                            {
                                Write(firstRead, "A", true);
                                popB = false;
                                countOpB--;
                            }
                            else if (countOpC > 0)
                            {
                                Write(secondRead, "A", true);
                                popC = false;
                                countOpC--;
                            }
                            countAllOp--;
                            if (countAllOp > 0)
                            {
                                if (countOpB > 0)
                                {
                                    if (maxPositionB != positionB)
                                    {
                                        firstRead = Read("B", positionB);
                                        popB = true;
                                    }
                                }
                                else if (countOpC > 0)
                                {
                                    if (maxPositionC != positionC)
                                    {
                                        secondRead = Read("C", positionC);
                                        popC = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Write(firstRead, "A", true);
                        popB = false;
                    }
                }
                sizeTempData *= 2;
                positionA = 0;
                positionB = 0;
                positionC = 0;
                ClearFile("B");
                ClearFile("C");
                countA = 0;
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
            using (FileStream sr = new FileStream(path, FileMode.Open))
            {
                byte[] buffer = new byte[1];
                string temp = "";
                sr.Seek(position, SeekOrigin.Current);
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
            //try
            //{
            //    using (FileStream sr = new FileStream(path, FileMode.Open))
            //    {
            //        byte[] buffer = new byte[1];
            //        sr.Seek(position, SeekOrigin.Current);
            //        do
            //        {
            //            var a = sr.Read(buffer, 0, 1);
            //            char converToChar = (char)buffer[0];
            //            if (converToChar == '\0')
            //            {
            //                throw new Exception();
            //            }
            //            data += converToChar;
            //        } while (buffer[0] != 32);
            //        result = data;
            //        logger.Log(Level.INFO, $"Элемент {data} считался из файла {namePath}");
            //        if (namePath.Equals("A"))
            //        {
            //            positionA = sr.Position;
            //            maxPositionA = sr.Length;
            //        }
            //        else if (namePath.Equals("B"))
            //        {
            //            positionB = sr.Position;
            //            maxPositionB = sr.Length;
            //        }
            //        else if (namePath.Equals("C"))
            //        {
            //            positionC = sr.Position;
            //            maxPositionC = sr.Length;
            //        }
            //        sr.Close();
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("The file could not be read:");
            //    Console.WriteLine(e.Message);
            //    throw new Exception("Неверный формат");
            //}
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
