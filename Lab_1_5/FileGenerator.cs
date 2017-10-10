using System;
using System.IO;

namespace Lab_1_5
{
    public class FileGenerator
    {
        private string _fileName;
        private int _maxValue;

        public FileGenerator(string fileName, int maxValue)
        {
            _fileName = fileName;
            _maxValue = maxValue;
        }

        public void Generate(int arrayLength, int processorsCount)
        {
            var rand = new Random();
            int[] array = new int[arrayLength];

            for (int i = 0; i < arrayLength; ++i)
            {
                array[i] = rand.Next(_maxValue);
            }

            using (var stream = File.Create(_fileName))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine($"{arrayLength} {processorsCount}");
                writer.WriteLine(string.Join(" ", array));
            }
        }
    }
}
