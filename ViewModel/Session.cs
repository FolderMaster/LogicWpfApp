using Model.Logic.Expressions;
using Model.Logic.Variables;

namespace ViewModel
{
    public class Session
    {
        public IExpression<bool> Expression { get; set; }

        public IList<INamedVariable<bool>> Variables { get; set; }
    }
}
