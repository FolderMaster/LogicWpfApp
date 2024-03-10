namespace Model.Logic.Variables
{
    public class BoolLiteral : IVariable<bool>
    {
        public bool _value;

        public BoolLiteral() { }

        public BoolLiteral(bool value) => _value = value;

        public bool GetValue() => _value;

        public override string ToString() => _value.ToString();

        public void SetValue(bool value) => _value = value;
    }
}