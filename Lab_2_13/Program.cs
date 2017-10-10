using System;
using System.Diagnostics;
using System.Linq;

namespace Lab_2_13
{
    class Program
    {
        private const int LENGTH = 1000;

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
                int iterations = 10;
                Console.WriteLine($"Iterations: {iterations}");

                Console.WriteLine($"Iterations: {iterations}\n");
                var notParallelResult = Calculate(numbers, new PrefixSummator(), iterations);
                Console.WriteLine($"not parallel average: {notParallelResult.average} millisec");
                file.Write(notParallelResult.prefixes, "result.txt");

                var parallelResult = Calculate(numbers, new ParallelPrefixSummator(), iterations);
                Console.WriteLine($"parallel average: {parallelResult.average} millisec");
                file.Write(parallelResult.prefixes, "result_parallel.txt");
            }
            else if ("-test".Equals(args[0]))
            {
                var numbers = file.Read(args[1]);
                int iterations = 10;
                Console.WriteLine($"Iterations: {iterations}");

                var notParallelResult = Calculate(numbers, new PrefixSummator(), iterations);
                var parallelResult = Calculate(numbers, new ParallelPrefixSummator(), iterations);
                for (int i = 0; i < notParallelResult.prefixes.Length; ++i)
                {
                    if (notParallelResult.prefixes[i] != parallelResult.prefixes[i])
                    {
                        Console.WriteLine("not match");
                    }
                }
            }
            Console.WriteLine("done");
            Console.ReadKey(true);
        }

        private static (double[] prefixes, double average) Calculate(double[] numbers, IPrefixSummator summator, int iterations)
        {
            double[] result = null;

            Stopwatch watch = new Stopwatch();
            double[] milliseconds = new double[iterations];
            for (int i = 0; i < iterations; ++i)
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
