using System.Windows;
using System.Windows.Controls;

using Model.Logic.Expressions;

namespace View.Controls
{
    public partial class ConstructorControl : UserControl
    {
        public IExpression<bool> Expression
        {
            get => (IExpression<bool>)GetValue(ExpressionProperty);
            set => SetValue(ExpressionProperty, value);
        }

        private static DependencyProperty ExpressionProperty =
            DependencyProperty.Register(nameof(Expression), typeof(IExpression<bool>),
                typeof(ConstructorControl), new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ConstructorControl()
        {
            InitializeComponent();
        }
    }
}
