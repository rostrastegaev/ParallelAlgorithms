using System;
using System.Diagnostics;
using System.Linq;

namespace Lab_2_13
{
    class Program
    {
        private const int LENGTH = 10000;
        private const int ITERATIONS = 10;
        private const int THREADS_COUNT = 3;

        static void Main(string[] args)
        {
            var file = new FileOperations(LENGTH);

            if ("-generate".Equals(args[0]))
            {
                file.Generate(args[1]);
            }
            else if ("-count".Equals(args[0]))
            {
                var numbers = file.Read(args[1]);
                Console.WriteLine($"Iterations: {ITERATIONS}");
                Console.WriteLine($"Threads: {THREADS_COUNT}");

                var notParallelResult = Calculate(numbers, new PrefixSummator());
                Console.WriteLine($"not parallel average: {notParallelResult.average} millisec");
                file.Write(notParallelResult.prefixes, "result.txt");

                var parallelResult = Calculate(numbers, new ParallelPrefixSummator(THREADS_COUNT));
                Console.WriteLine($"parallel average: {parallelResult.average} millisec");
                file.Write(parallelResult.prefixes, "result_parallel.txt");

                double coefficient = notParallelResult.average / parallelResult.average;
                Console.WriteLine($"coefficient: {coefficient:0.00}");
            }
            Console.WriteLine("done");
            Console.ReadKey(true);
        }

        private static (double[] prefixes, double average) Calculate(double[] numbers, IPrefixSummator summator)
        {
            double[] result = null;

            Stopwatch watch = new Stopwatch();
            double[] milliseconds = new double[ITERATIONS];
            for (int i = 0; i < ITERATIONS; ++i)
            {
                watch.Reset();
                watch.Start();
                result = summator.Calculate(numbers);
                watch.Stop();
                milliseconds[i] = watch.Elapsed.TotalMilliseconds;
            }

            return (result, milliseconds.Average());
        }
    }
}
