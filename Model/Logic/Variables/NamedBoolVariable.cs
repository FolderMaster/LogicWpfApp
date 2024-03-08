using Model.Parsing.Tokenization;

namespace Model.Logic.Variables
{
    public class NamedBoolVariable : INamedVariable<bool>, ILexeme
    {
        protected bool _value;

        public string Name { get; set; }

        public string LexemePattern => @"\w+";

        public string LexemeType => "Variable";

        public bool GetValue() => _value;

        public void SetValue(bool value) => _value = value;

        public NamedBoolVariable() { }

        public NamedBoolVariable(string name) => Name = name;

        public NamedBoolVariable(string name, bool value) : this(name) => _value = value;

        public override string ToString() => Name.ToString();
    }
}