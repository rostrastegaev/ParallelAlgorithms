using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_2_13
{
    public class ParallelPrefixSummator : IPrefixSummator
    {
        private int _threadsCount;

        public ParallelPrefixSummator(int threadsCount)
        {
            _threadsCount = threadsCount;
        }

        public double[] Calculate(double[] numbers)
        {
            int lastIndex = numbers.Length / 2;
            double[] prefixes = new double[lastIndex + 1];

            Task[] tasks = new Task[_threadsCount];
            int startI = lastIndex - 1;
            int startJ = startI;
            for (int k = 0; k < _threadsCount; ++k)
            {
                int closureK = k;
                int i = startI - k;
                int j = startJ + k;
                tasks[k] = Task.Run(() =>
                {
                    for (int l = closureK; i >= 0 && j < numbers.Length; i -= _threadsCount, j += _threadsCount, l += _threadsCount)
                    {
                        for (int n = i; n <= j; ++n)
                        {
                            prefixes[l] += numbers[n];
                        }
                    }
                });
            }
            Task.WaitAll(tasks);

            for (int i = 0; i < numbers.Length; ++i)
            {
                prefixes[lastIndex] += numbers[i];
            }

            return prefixes;
        }
    }
}
