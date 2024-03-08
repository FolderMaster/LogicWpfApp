using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using View.Implementations.DialogServices;

using ViewModel;
using Model.Calculating;

namespace View.Windows
{
    /// <summary>
    /// Interaction logic for CalculatingOptionWindow.xaml
    /// </summary>
    public partial class CalculatingOptionWindow : DialogWindow
    {
        public BoolCalculatingOptions CalculatingOptions { get; set; }

        public CalculatingOptionWindow(Session session)
        {
            InitializeComponent();
            CalculatingOptions = session.CalculatingOptions;
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
            binding.Path = new PropertyPath($"{nameof(CalculatingOptions)}." +
                $"{nameof(BoolCalculatingOptions.VariablesOptions)}[{name}]");
            checkBox.SetBinding(CheckBox.IsCheckedProperty, binding);
            return checkBox;
        }

        private void FillVariables()
        {
            foreach (var variable in CalculatingOptions.VariablesOptions)
            {
                variablesPanel.Children.Add(CreateVariableElement(variable.Key));
            }
        }
    }
}
