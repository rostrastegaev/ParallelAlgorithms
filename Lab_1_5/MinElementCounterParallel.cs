using System;
using System.Threading.Tasks;

namespace Lab_1_5
{
    public class MinElementCounterParallel : IMinElementCounter
    {
        private int[] _array;
        private int _processorsCount;

        public MinElementCounterParallel(int[] array, int processorsCount)
        {
            _array = array;
            _processorsCount = processorsCount;
        }

        public (int min, int count) CountMinElement()
        {
            int blockLength = (int)Math.Ceiling((double)_array.Length / _processorsCount);
            int[] mins = new int[_processorsCount];

            Task[] tasks = new Task[_processorsCount];
            for (int i = 0; i < _processorsCount; ++i)
            {
                int j = i;
                var task = Task.Run(() =>
                {
                    int startIndex = j * blockLength;
                    int length = startIndex + blockLength <= _array.Length ? blockLength : _array.Length - startIndex;
                    int min = FindMin(startIndex, length);
                    mins[j] = min;
                });
                tasks[j] = (task);
            }
            Task.WaitAll(tasks);

            int minResult = mins.Min();
            int[] counts = new int[_processorsCount];
            for (int i = 0; i < _processorsCount; ++i)
            {
                int j = i;
                var task = Task.Run(() =>
                {
                    int startIndex = j * blockLength;
                    int length = startIndex + blockLength <= _array.Length ? blockLength : _array.Length - startIndex;
                    counts[j] = Count(startIndex, length, minResult);
                });
                tasks[j] = task;
            }
            Task.WaitAll(tasks);

            return (minResult, counts.Sum());
        }

        private int FindMin(int startIndex, int length)
        {
            int min = _array[startIndex];
            for (int i = startIndex; (i - startIndex) < length; ++i)
            {
                if (_array[i] < min)
                {
                    min = _array[i];
                }
            }
            return min;
        }

        private int Count(int startIndex, int length, int minValue)
        {
            int count = 0;
            for (int i = startIndex; (i - startIndex) < length; ++i)
            {
                if (_array[i] == minValue)
                {
                    ++count;
                }
            }
            return count;
        }
    }
}
