namespace Lab_1_5
{
    public class MinElementCounter : IMinElementCounter
    {
        private int[] _array;

        public MinElementCounter(int[] array)
        {
            _array = array;
        }

        public (int min, int count) CountMinElement()
        {
            int min = _array.Min();
            return (min, Count(min));
        }

        private int Count(int minValue)
        {
            int count = 0;
            for (int i = 0; i < _array.Length; ++i)
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
