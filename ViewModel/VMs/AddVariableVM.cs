using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.VMs
{
    public partial class AddVariableVM : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string> variableChoices = new();

        [ObservableProperty]
        private string selectedVariableChoice = "";




    }
}
