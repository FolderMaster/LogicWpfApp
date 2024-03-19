using Model.Calculating;
using Model.Logic.Expressions;

namespace ViewModel
{
    public class Session
    {
        public IExpression<bool> Expression { get; set; }

        public ICalculatingOptions<bool> CalculatingOptions { get; set; }
    }
}
