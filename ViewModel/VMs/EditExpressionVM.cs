using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

using Model.Logic.Expressions;
using Model.Logic.Variables;
using Model.Logic.Operators.PairOperators;
using Model.Logic.Operators.SingleOperators;

namespace ViewModel.VMs
{
    public partial class EditExpressionVM : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<INamedVariable<bool>> variables = new() { new NamedBoolVariable("A"), new NamedBoolVariable("B") };

        [ObservableProperty]
        private IExpression<bool> expression = new Expression<bool>();

        public EditExpressionVM()
        {
            Expression.Add(Variables[0]);
            Expression.Add(new OrOperator());
            Expression.Add(Variables[1]);
            Expression.Add(new AndOperator());
            Expression.Add(Variables[0]);
            Expression.Add(new ImplicateOperator());
            Expression.Add(new NotOperator());
            Expression.Add(Variables[1]);
            Expression.Add(new XorOperator());
            Expression.Add(Variables[0]);
            Expression.Add(new EqualOperator());
            Expression.Add(Variables[1]);
        }
    }
}
