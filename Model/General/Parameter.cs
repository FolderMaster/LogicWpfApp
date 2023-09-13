namespace Model.General
{
    public class Parameter
    {
        protected object? _value;

        private Func<object?, bool>? _valueValidateFunc;

        public string Name { get; private set; }

        public object? Value
        {
            get => _value;
            set
            {
                if (_valueValidateFunc == null && value?.GetType() == Type)
                {
                    _value = value;
                }
                else
                {
                    _valueValidateFunc?.Invoke(value);
                }
            }
        }

        public Type? Type { get; private set; }

        public Parameter(string name, object? defaultValue = null,
            Func<object?, bool>? valueValidateFunc = null)
        {
            Name = name;
            _valueValidateFunc = valueValidateFunc;
            Value = defaultValue;
            Type = defaultValue?.GetType();
        }
    }
}