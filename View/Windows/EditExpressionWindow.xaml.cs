using Model.Logic.Expressions;

using View.Implementations.DialogServices;

namespace View.Windows
{
    public partial class EditExpressionWindow : DialogWindow
    {
        public EditExpressionWindow(IExpression<bool> expression)
        {
            InitializeComponent();

            ResultValue = expression;
        }
    }
}
