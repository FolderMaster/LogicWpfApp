using Model.Parsing.Tokenization;

namespace Model.Logic.Variables
{
    public class BoolLiteral : IVariable<bool>, ILexeme
    {
        public bool _value;

        public string LexemePattern => "0|1|true|false";

        public string LexemeType => "Literal";

        public BoolLiteral() { }

        public BoolLiteral(bool value) => _value = value;

        public bool GetValue() => _value;

        public override string ToString() => _value.ToString();

        public void SetValue(bool value) => _value = value;
    }
}