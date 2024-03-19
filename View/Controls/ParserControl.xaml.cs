using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using Model.Logic.Expressions;

using ViewModel.VMs;

namespace View.Controls
{
    public partial class ParserControl : UserControl
    {
        public IExpression<bool> Expression
        {
            get => (IExpression<bool>)GetValue(ExpressionProperty);
            set => SetValue(ExpressionProperty, value);
        }

        private static DependencyProperty ExpressionProperty =
            DependencyProperty.Register(nameof(Expression), typeof(IExpression<bool>),
                typeof(ParserControl), new FrameworkPropertyMetadata(null));

        public ParserControl()
        {
            InitializeComponent();

            DataContext = new ParserVM();
            var binding = new Binding(nameof(ParserVM.Expression))
            {
                Mode = BindingMode.TwoWay
            };
            SetBinding(ExpressionProperty, binding);

            BindingOperations.SetBinding(this, ExpressionProperty, new Binding());
        }
    }
}
