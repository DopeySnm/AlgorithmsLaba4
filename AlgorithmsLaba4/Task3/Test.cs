namespace AlgorithmsLaba4.Task3
{
    internal class Test
    {
        public string[] GetDataFile(string path)
        {
            string[] data = File.ReadAllText(path).Split(" ");
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i].Replace(",", "");
                data[i] = data[i].Replace(".", "");
            }
            return data;
        }
        public (double[], int[]) RunTest(IAlgorithms algorithm, int count, int countPoint, int sizeWordMin, int sizeWordMax)
        {
            string[] data = GetDataFile($"..\\..\\..\\..\\TestText.txt");
            //string[] data = GenData(count, sizeWordMin, sizeWordMax + 1);
            int[] dataResult = new int[countPoint];
            double[] resultTime = new double[countPoint];
            for (int i = 0; i < countPoint; i++)
            {
                int part = (i + 1) * (data.Length / countPoint);
                dataResult[i] = part;
                algorithm.SetData(GetPartData(data, part));
                resultTime[i] = TestTime.Run(algorithm);
            }
            return (resultTime, dataResult);
        }
        private string[] GetPartData(string[] data, int partCount)
        {
            string[] partData = new string[partCount];
            for (int i = 0; i < partCount; i++)
            {
                partData[i] = data[i];
            }
            return partData;
        }
        private string[] GenData(int num_words, int minLet, int maxLet)
        {
            string[] data = new string[num_words];
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            Random rand = new Random();
            for (int i = 0; i <= num_words - 1; i++)
            {
                // Сделайте слово.
                int num_letters = rand.Next(minLet, maxLet);
                string word = "";
                for (int j = 1; j <= num_letters; j++)
                {
                    // Выбор случайного числа от 0 до 25
                    // для выбора буквы из массива букв.
                    int letter_num = rand.Next(0, letters.Length - 1);

                    // Добавить письмо.
                    word += letters[letter_num];
                }

                // Добавьте слово в список.
                data[i] = word;
            }
            return data;
        }
    }
}
