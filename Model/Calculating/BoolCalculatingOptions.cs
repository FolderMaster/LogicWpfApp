using Model.Logic.Variables;

namespace Model.Calculating
{
    public class BoolCalculatingOptions : ICalculatingOptions<bool>
    {
        private int _iterationsCount;

        public Dictionary<string, bool?> VariablesOptions { get; private set; }

        public int IterationsCount => _iterationsCount;

        public BoolCalculatingOptions(IEnumerable<INamedVariable<bool>> variables)
        {
            var variablesOptions = new Dictionary<string, bool?>();
            foreach (var variable in variables)
            {
                variablesOptions.Add(variable.Name, null);
            }
            VariablesOptions = variablesOptions;
        }

        public IEnumerable<IList<bool>> GenerateArgs()
        {
            _iterationsCount = (int)Math.Pow(2, VariablesOptions.Where((v) => v.Value == null).Count());
            for (var i = 0; i < _iterationsCount; ++i)
            {
                var number = i;
                var result = new List<bool>();
                foreach (var variableOption in VariablesOptions)
                {
                    var value = true;
                    if (variableOption.Value == null)
                    {
                        value = (number & 1) == 1;
                        number = number >> 1;
                    }
                    else
                    {
                        value = (bool)variableOption.Value;
                    }
                    result.Add(value);
                }
                yield return result;
            }
        }
    }
}
