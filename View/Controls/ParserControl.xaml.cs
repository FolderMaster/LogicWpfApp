using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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
                typeof(ParserControl), new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnExpressionChanged));

        public ParserControl()
        {
            InitializeComponent();

            var vm = new ParserVM();
            vm.PropertyChanged += Vm_PropertyChanged;
            DataContext = vm;
        }

        private void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ParserVM.Expression))
            {
                Expression = (sender as ParserVM).Expression;
            }
        }

        private static void OnExpressionChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (ParserControl)sender;
            var vm = (ParserVM)control.DataContext;
            vm.Expression = (IExpression<bool>)e.NewValue;
        }
    }
}
