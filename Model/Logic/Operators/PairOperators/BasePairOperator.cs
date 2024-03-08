using Model.Parsing.Tokenization;

namespace Model.Logic.Operators.PairOperators
{
    public abstract class BasePairOperator<T> : IPairOperator<T>, ILexeme
    {
        private IValue<T> _leftOperand;

        private IValue<T> _rightOperand;

        private string _lexemePattern;

        protected BasePairOperator(string lexemePattern) => _lexemePattern = lexemePattern;

        public string LexemePattern => _lexemePattern;

        public string LexemeType => "PairOperator";

        public IValue<T> LeftOperand
        {
            get => _leftOperand;
            set => _leftOperand = value;
        }

        public IValue<T> RightOperand
        {
            get => _rightOperand;
            set => _rightOperand = value;
        }

        public abstract int Priority { get; }

        protected abstract T CalculateValue();

        public T GetValue()
        {
            if (LeftOperand == null)
            {
                throw new InvalidOperationException($"Отсутствует левый операнд у '{this}'!");
            }
            if (RightOperand == null)
            {
                throw new InvalidOperationException($"Отсутствует правый операнд у '{this}'!");
            }
            return CalculateValue();
        }

        public override string ToString() => _lexemePattern;
    }
}
