using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Model.Logic.Expressions;

namespace View.Controls.Constructor
{
    /// <summary>
    /// Interaction logic for ConstructorPanelControl.xaml
    /// </summary>
    public partial class ConstructorCanvasControl : UserControl
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
                typeof(ConstructorCanvasControl), new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnExpressionChanged));

        public ConstructorCanvasControl()
        {
            InitializeComponent();
        }

        private static void OnExpressionChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (ConstructorCanvasControl)sender;
            var expression = control.Expression;
            control.mainLayout.Children.Clear();
            if(expression != null)
            {
                foreach (var value in expression)
                {
                    var item = new ConstructorItemControl(value.Value);
                    control.mainLayout.Children.Add(item);
                    Canvas.SetLeft(item, value.Index * 30);
                    Canvas.SetTop(item, value.Depth * 30);
                }
            }
        }

        private void mainLayout_Drop(object sender, DragEventArgs e)
        {
            var text = e.Data.GetData(DataFormats.Text) as string;
            if (text != null)
            {
                var item = new ConstructorItemControl(text);
                mainLayout.Children.Add(item);
                var dropPosition = e.GetPosition(mainLayout);
                Canvas.SetLeft(item, dropPosition.X);
                Canvas.SetTop(item, dropPosition.Y);
            }
        }
    }
}
