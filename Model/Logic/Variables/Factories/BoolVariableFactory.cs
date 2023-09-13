using Model.General;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Variables.Factories
{
    public class BoolVariableFactory : IFactory<ILogicVariable>
    {
        private ObservableCollection<Parameter> _parameters = new ObservableCollection<Parameter>();

        public ObservableCollection<Parameter> Parameters => throw new NotImplementedException();

        public ILogicVariable Create()
        {
            throw new NotImplementedException();
        }
    }
}
