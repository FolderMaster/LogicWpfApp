using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.Logic.Expressions;

using ViewModel.Interfaces;

namespace ViewModel.VMs
{
    public partial class MainVM : ObservableObject
    {
        [ObservableProperty]
        private Expression<bool> expression = new();

        public ICommand EditExpressionCommand { get; private set; }

        public ICommand SettingsCommand { get; private set; }

        public ICommand CalculateCommand { get; private set; }

        public MainVM(IDialogService errorDialog, IDialogService editExpressionDialog)
        {
            EditExpressionCommand = new RelayCommand(() =>
            {
                var dialogResult = errorDialog.ShowDialog();
                dialogResult = editExpressionDialog.ShowDialog();
            });
        }
    }
}