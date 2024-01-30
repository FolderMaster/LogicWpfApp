using Model.Logic.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Logic.Expressions
{
    public static class ExpressionCalculating<T>
    {
        public static async Task<IList<ExpressionCalculatingResult<T>>> Calculate(IExpression<T> expression,
            IList<INamedVariable<T>> variables, Func<IEnumerable<IList<T>>> generator)
        {
            var result = new List<ExpressionCalculatingResult<T>>();
            foreach(var values in generator())
            {
                if (values.Count != variables.Count)
                {
                    throw new ArgumentException();
                }
                var dictionary = new Dictionary<INamedVariable<T>, T>();
                for(int n = 0; n < variables.Count; ++n)
                {
                    dictionary.Add(variables[n], values[n]);
                    variables[n].SetValue(values[n]);
                }
                var value = expression.GetValue();
                result.Add(new ExpressionCalculatingResult<T>(value, dictionary));
            }
            return result;
        }
    }
}
