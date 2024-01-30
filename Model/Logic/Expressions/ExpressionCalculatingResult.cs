using Model.Logic.Variables;
using System.Collections.Immutable;

namespace Model.Logic.Expressions
{
    public class ExpressionCalculatingResult<T>
    {
        public T Result { get; private set; }

        public ImmutableDictionary<INamedVariable<T>, T> Parameters { get; private set; }

        public ExpressionCalculatingResult(T result, IDictionary<INamedVariable<T>, T> parameters)
        {
            Result = result;
            Parameters = parameters.ToImmutableDictionary();
        }
    }
}
