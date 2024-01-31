using CommunityToolkit.Mvvm.ComponentModel;

using Model.Logic.Expressions;
using Model.Logic.Variables;

namespace ViewModel.VMs
{
    public partial class EditExpressionVM : ObservableObject
    {
        private Session _session;

        private string _expressionText;

        public string ExpressionText
        {
            get => _expressionText;
            set => SetProperty(ref _expressionText, value);
        }

        public IExpression<bool> Expression
        {
            get => _session.Expression;
        }

        public IList<INamedVariable<bool>> Variables
        {
            get => _session.Variables;
        }

        public EditExpressionVM(Session session)
        {
            _session = session;
            _expressionText = session.Expression.ToString();
        }
    }
}
