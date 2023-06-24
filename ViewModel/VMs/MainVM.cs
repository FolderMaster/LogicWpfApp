using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Model;
using Model.Variables;
using ViewModel.Interfaces;

namespace ViewModel.VMs
{
    public partial class MainVM : ObservableObject
    {
        private IDialogShowable addVariableDialog;

        private IDialogShowable editExpressionDialog;

        [ObservableProperty]
        private ObservableCollection<ILogicVariable> variables = new();

        [ObservableProperty]
        private LogicExpression expression = new();

        public ICommand AddVariableCommand { get; private set; }

        public ICommand EditExpressionCommand { get; private set; }

        public MainVM()
        {
            AddVariableCommand = new RelayCommand(() =>
            Variables.Add(new BoolVariable()));
            EditExpressionCommand = new RelayCommand(() => { });
        }
    }
}