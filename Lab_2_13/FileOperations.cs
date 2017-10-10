using System;
using System.IO;

namespace Lab_2_13
{
    public class FileOperations
    {
        private int _count;

        public FileOperations(int count)
        {
            _count = count;
        }

        public void Generate(string file)
        {
            var random = new Random();

            using (var output = File.Create(file))
            using (var writer = new StreamWriter(output))
            {
                for (int i = 1; i < _count; ++i)
                {
                    writer.Write(random.NextDouble());
                    writer.Write(' ');
                }
                writer.Write(random.NextDouble());
            }
        }

        public double[] Read(string file)
        {
            string content;
            using (var input = File.OpenRead(file))
            using (var reader = new StreamReader(input))
            {
                content = reader.ReadToEnd();
            }

            var splitted = content.Split(' ');
            double[] numbers = new double[_count];
            for (int i = 0; i < splitted.Length; ++i)
            {
                numbers[i] = double.Parse(splitted[i]);
            }
            return numbers;
        }

        public void Write(double[] numbers, string file)
        {
            var length = numbers.Length * 2;
            using (var input = File.Create(file))
            using (var writer = new StreamWriter(input))
            {
                for (int i = 499, j = 499, k = 0; i >= 0 && j < length; --i, ++j, ++k)
                {
                    writer.WriteLine($"S[{i + 1}, {j + 1}] = {numbers[k]}");
                }
                writer.WriteLine($"S[1, 1000] = {numbers[numbers.Length - 1]}");
            }
        }
    }
}
