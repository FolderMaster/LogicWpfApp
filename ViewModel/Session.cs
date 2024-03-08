using Model.Calculating;
using Model.Logic.Expressions;
using Model.Logic.Variables;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class Session
    {
        public IExpression<bool> Expression { get; set; }

        public ObservableCollection<INamedVariable<bool>> Variables { get; set; }

        public BoolCalculatingOptions CalculatingOptions { get; set; }
    }
}
