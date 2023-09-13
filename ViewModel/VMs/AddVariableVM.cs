using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Model.General;
using Model.Variables;

namespace ViewModel.VMs
{
    public partial class AddVariableVM : ObservableObject
    {
        private Dictionary<string, IFactory<ILogicVariable>> _variableFactoryDictionary;

        [ObservableProperty]
        private ObservableCollection<string> variableFactoryChoices = new();

        [ObservableProperty]
        private string? selectedVariableFactoryChoice = null;

        public ICommand CreateCommand { private set; get; }

        public ICommand CancelCommand { private set; get; }

        public AddVariableVM(ObservableCollection<IFactory<ILogicVariable>> variableFactories)
        {
            _variableFactoryDictionary = variableFactories.ToDictionary((vf) => vf.ToString());

            foreach(var key in _variableFactoryDictionary.Keys)
            {
                VariableFactoryChoices.Add(key);
            }
            SelectedVariableFactoryChoice = VariableFactoryChoices.FirstOrDefault();

            CreateCommand = new RelayCommand(() => { });
            CancelCommand = new RelayCommand(() => { });
        }
    }
}