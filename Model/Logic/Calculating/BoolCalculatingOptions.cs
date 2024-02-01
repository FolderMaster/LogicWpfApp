namespace Model.Logic.Calculating
{
    public class BoolCalculatingOptions : ICalculatingOptions<bool>
    {
        private int _iterationsCount;

        private int _variablesCount;

        public int IterationsCount => _iterationsCount;

        public BoolCalculatingOptions(int variablesCount)
        {
            _variablesCount = variablesCount;
            _iterationsCount = (int)Math.Pow(2, variablesCount);
        }

        public IEnumerable<IList<bool>> GenerateArgs()
        {
            for (var i = 0; i < _iterationsCount; ++i)
            {
                var value = i;
                var result = new List<bool>();
                for (var n = 0; n < _variablesCount; ++n)
                {
                    result.Add((value & 1) == 1);
                    value = value >> 1;
                }
                yield return result;
            }
        }
    }
}
