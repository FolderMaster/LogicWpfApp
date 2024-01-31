using Model.Logic.Expressions;
using Model.Logic.Variables;

namespace Model.Logic.Calculating
{
    public class CalculatingManager<T>
    {
        public event EventHandler<CalculatingEventArgs<T>>? AddedResultRow;

        public async Task Calculate(IExpression<T> expression,
            IList<INamedVariable<T>> variables, int resultRowCount,
            Func<IEnumerable<IList<T>>> generator)
        {
            var result = new List<CalculatingResult<T>>();
            var count = 0;
            foreach (var values in generator())
            {
                if (values.Count != variables.Count)
                {
                    throw new ArgumentException();
                }
                var dictionary = new Dictionary<INamedVariable<T>, T>();
                for (int n = 0; n < variables.Count; ++n)
                {
                    dictionary.Add(variables[n], values[n]);
                    variables[n].SetValue(values[n]);
                }
                var value = expression.GetValue();
                ++count;
                await Task.Delay(100);
                AddedResultRow?.Invoke(this, new CalculatingEventArgs<T>
                    (new CalculatingResult<T>(value, dictionary), count / (double)resultRowCount));
            }
        }
    }
}
