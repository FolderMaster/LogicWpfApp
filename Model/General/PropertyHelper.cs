using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.General
{
    public class PropertyHelper : INotifyPropertyChanged, INotifyDataErrorInfo, IDataErrorInfo
    {
        private readonly Dictionary<string, ObservableCollection<string>> _errorDictionary = new();

        public string this[string columnName] => string.Join("\t",
            _errorDictionary.GetValueOrDefault(columnName));

        public bool HasErrors => _errorDictionary.Count > 0;

        public string Error => string.Join("\t", _errorDictionary.Values);

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnErrorsChanged([CallerMemberName] string? propertyName = null) =>
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public IEnumerable? GetErrors(string? propertyName) =>
            _errorDictionary.GetValueOrDefault(propertyName);
    }
}
