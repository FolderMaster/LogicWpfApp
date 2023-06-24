namespace Model.Variables
{
    public class BoolVariable : ILogicVariable
    {
        static readonly Type _type = typeof(bool);

        private string _name = null!;

        private object? _value = null;

        private bool? _bool = null;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public object? Value
        {
            get => _value;
            set
            {
                _value = value;
            }
        }

        public Type Type => _type;

        public bool? Bool => _bool;
    }
}