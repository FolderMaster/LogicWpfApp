using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Model.Logic.Expressions;

namespace View.Controls
{
    public partial class ConstructorControl : UserControl
    {
        private static Brush _borderBrush = new SolidColorBrush(Color.FromArgb(255, 150, 150, 150));

        private static Brush _innerBrush = new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));

        public IExpression<bool> Expression
        {
            get => (IExpression<bool>)GetValue(ExpressionProperty);
            set => SetValue(ExpressionProperty, value);
        }

        private static DependencyProperty ExpressionProperty =
            DependencyProperty.Register(nameof(Expression), typeof(IExpression<bool>),
                typeof(ConstructorControl), new FrameworkPropertyMetadata(null, OnExpressionChanged));

        public ConstructorControl()
        {
            InitializeComponent();
        }

        private static void OnExpressionChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (ConstructorControl)sender;
            var expression = control.Expression;
            control.mainLayout.Children.Clear();
            foreach(var value in expression)
            {
                var text = new TextBlock()
                { 
                    Text = value.Value.ToString()
                };
                var border = new Border()
                {
                    Padding = new Thickness(5),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(5),
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Background = _innerBrush,
                    BorderBrush = _borderBrush,
                };
                border.Child = text;

                control.mainLayout.Children.Add(border);
                Canvas.SetLeft(border, value.Index * 30);
                Canvas.SetTop(border, value.Depth * 30);
            }
        }
    }
}
