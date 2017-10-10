namespace Lab_2_13
{
    public class PrefixSummator : IPrefixSummator
    {
        public double[] Calculate(double[] numbers)
        {
            int lastIndex = numbers.Length / 2;
            double[] prefixes = new double[lastIndex + 1];
            for (int i = 499, j = 499, k = 0; i >= 0 && j < numbers.Length; --i, ++j, ++k)
            {
                for (int l = i; l <= j; ++l)
                {
                    prefixes[k] += numbers[l];
                }
            }
            for (int i = 0; i < numbers.Length; ++i)
            {
                prefixes[lastIndex] += numbers[i];
            }

            return prefixes;
        }
    }
}
