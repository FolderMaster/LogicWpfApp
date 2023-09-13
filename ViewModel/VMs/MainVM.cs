using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Model;
using Model.General;
using Model.Variables;
using Model.Variables.Factories;

using ViewModel.Interfaces;

namespace ViewModel.VMs
{
    public partial class MainVM : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ILogicVariable> variables = new();

        [ObservableProperty]
        private LogicExpression expression = new();

        public ICommand AddVariableCommand { get; private set; }

        public ICommand EditExpressionCommand { get; private set; }

        public MainVM(IDialogService addVariableDialog, IDialogService errorDialog)
        {
            AddVariableCommand = new RelayCommand(() =>
            {
                var dialogResult = addVariableDialog.ShowDialog(new ObservableCollection<IFactory<ILogicVariable>>()
                {
                    new BoolVariableFactory()
                });
            });
            EditExpressionCommand = new RelayCommand(() =>
            {
                var dialogResult = errorDialog.ShowDialog(
                "Keys:\n1\n2\n3\n4\n5\n6\n7\n8\n9\n10\n11\n12\n13\n14\n15");
            });
        }
    }
}