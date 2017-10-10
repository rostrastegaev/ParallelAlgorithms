using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_2_13
{
    public class ParallelPrefixSummator : IPrefixSummator
    {
        public double[] Calculate(double[] numbers)
        {
            int threadsCount = 3;
            int lastIndex = numbers.Length / 2;
            double[] prefixes = new double[lastIndex + 1];

            Task[] tasks = new Task[threadsCount];
            int startI = 499;
            int startJ = 499;
            for (int k = 0; k < threadsCount; ++k)
            {
                int closureK = k;
                int i = startI - k;
                int j = startJ + k;
                tasks[k] = Task.Run(() =>
                {
                    for (int l = closureK; i >= 0 && j < numbers.Length; i -= threadsCount, j += threadsCount, l += threadsCount)
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
