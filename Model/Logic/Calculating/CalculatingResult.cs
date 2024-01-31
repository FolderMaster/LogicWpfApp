using Model.Logic.Variables;
using System.Collections.Immutable;

namespace Model.Logic.Calculating
{
    public class CalculatingResult<T>
    {
        public T Result { get; private set; }

        public ImmutableDictionary<INamedVariable<T>, T> Parameters { get; private set; }

        public CalculatingResult(T result, IDictionary<INamedVariable<T>, T> parameters)
        {
            Result = result;
            Parameters = parameters.ToImmutableDictionary();
        }
    }
}
