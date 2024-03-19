using View.Windows;

using ViewModel;

using Model.Logic.Expressions;

namespace View.Implementations.DialogServices.Windows
{
    public class EditExpressionWindowDialogService : BaseWindowDialogService
    {
        private Session _session;

        public EditExpressionWindowDialogService(Session session)
        {
            _session = session;
        }

        protected override DialogWindow CreateWindow(object? parameter) =>
            new EditExpressionWindow((IExpression<bool>)_session.Expression.Clone());
    }
}
