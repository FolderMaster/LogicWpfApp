using System.Collections.ObjectModel;

namespace Model.General
{
    public interface IFactory<T>
    {
        public ObservableCollection<Parameter> Parameters { get; }

        public T Create();
    }
}