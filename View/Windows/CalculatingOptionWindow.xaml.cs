using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using View.Implementations.DialogServices;

using Model.Calculating;

namespace View.Windows
{
    /// <summary>
    /// Interaction logic for CalculatingOptionWindow.xaml
    /// </summary>
    public partial class CalculatingOptionWindow : DialogWindow
    {
        public CalculatingOptionWindow(ICalculatingOptions<bool> calculatingOptions)
        {
            InitializeComponent();

            ResultValue = calculatingOptions;
            FillVariables();
        }

        private UIElement CreateVariableElement(string name)
        {
            var checkBox = new CheckBox()
            {
                Content = name,
                IsThreeState = true
            };
            Binding binding = new Binding();
            binding.ElementName = "CalculatingOptionsWindow";
            binding.Path = new PropertyPath($"{nameof(ResultValue)}." +
                $"{nameof(BoolCalculatingOptions.VariablesOptions)}[{name}]");
            checkBox.SetBinding(CheckBox.IsCheckedProperty, binding);
            return checkBox;
        }

        private void FillVariables()
        {
            foreach (var variable in ((BoolCalculatingOptions)ResultValue).VariablesOptions)
            {
                variablesPanel.Children.Add(CreateVariableElement(variable.Key));
            }
        }
    }
}
