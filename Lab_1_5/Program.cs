using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Lab_1_5
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if ("-generate".Equals(args[0]))
                {
                    ProceedGeneration(args);
                }
                else if ("-count".Equals(args[0]))
                {
                    ProceedCounting(args);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed! {ex.Message}");
            }
            Console.WriteLine("done");
            Console.ReadKey(true);
        }

        private static void ProceedGeneration(string[] args)
        {
            if (!int.TryParse(args[2], out int arrayLength) ||
                !int.TryParse(args[3], out int maxValue) ||
                !int.TryParse(args[4], out int processorsCount))
            {
                throw new ArgumentException("Argument is not an integer");
            }

            new FileGenerator(args[1], maxValue).Generate(arrayLength, processorsCount);
        }

        private static void ProceedCounting(string[] args)
        {
            if (!File.Exists(args[1]))
            {
                throw new ArgumentException("File doesn't exist");
            }

            var fileContent = new FileReader(File.OpenRead(args[1])).Read();
            var counter = new MinElementCounter(fileContent.array);

            int iterations = 10;
            Console.WriteLine($"Iterations: {iterations}");
            int[] results = new int[iterations];
            var watch = new Stopwatch();
            (int min, int count) result = (0, 0);
            for (int i = 0; i < iterations; ++i)
            {
                watch.Reset();
                watch.Start();
                result = counter.CountMinElement();
                watch.Stop();
                results[i] = watch.Elapsed.Milliseconds;
            }

            int notParallelMilliseconds = (int)Math.Floor(results.Average());
            Console.WriteLine($"\nNot parallel: min = {result.min}, count = {result.count}, avg time = {notParallelMilliseconds}\n");

            for (int i = 2; i < 6; ++i)
            {
                var parallelCounter = new MinElementCounterParallel(fileContent.array, i);
                for (int j = 0; j < iterations; ++j)
                {
                    watch.Reset();
                    watch.Start();
                    result = parallelCounter.CountMinElement();
                    watch.Stop();
                    results[j] = watch.Elapsed.Milliseconds;
                }
                int parallelMilliseconds = (int)Math.Floor(results.Average());
                double coefficient = (double)notParallelMilliseconds / parallelMilliseconds;
                Console.WriteLine($"Parallel ({i} cores): min = {result.min}, count = {result.count}, avg time =  {parallelMilliseconds} millisec");
                Console.WriteLine($"Коэффициент ускорения: {coefficient:0.00}\n");
            }
        }
    }
}
