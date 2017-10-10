using System;
using System.IO;

namespace Lab_1_5
{
    public class FileReader
    {
        private const char SEPARATOR = ' ';
        private Stream _stream;

        public FileReader(Stream stream)
        {
            _stream = stream;
        }

        public (int processorsCount, int[] array) Read()
        {
            if (!_stream.CanRead)
            {
                throw new ArgumentException("File is not available to read");
            }

            using (var reader = new StreamReader(_stream))
            {
                var info = ParseInfo(reader.ReadLine());
                var array = ParseArray(reader.ReadLine(), info.arrayLength);
                return (info.processorsCount, array);
            }
        }

        private (int arrayLength, int processorsCount) ParseInfo(string info)
        {
            var splittedInfo = SplitLine(info);
            if (splittedInfo.Length != 2 ||
                !int.TryParse(splittedInfo[0], out int arrayLength) ||
                !int.TryParse(splittedInfo[1], out int processorsCount))
            {
                throw new ArgumentException("File header has invalid format");
            }

            return (arrayLength, processorsCount);
        }

        private int[] ParseArray(string arrayStr, int arrayLength)
        {
            int[] array = new int[arrayLength];
            var splittedArray = SplitLine(arrayStr);
            if (splittedArray.Length != arrayLength)
            {
                throw new ArgumentException("Array have incorrect length");
            }

            for (int i = 0; i < arrayLength; ++i)
            {
                if (!int.TryParse(splittedArray[i], out array[i]))
                {
                    throw new ArgumentException("Invalid array element");
                }
            }

            return array;
        }

        private string[] SplitLine(string line) =>
            line.Split(new[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
    }
}
