namespace Lab_1_5
{
    public static class Extensions
    {
        public static int Min(this int[] array)
        {
            int min = array[0];
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] < min)
                {
                    min = array[i];
                }
            }
            return min;
        }

        public static int Sum(this int[] array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; ++i)
            {
                sum += array[i];
            }
            return sum;
        }
    }
}
