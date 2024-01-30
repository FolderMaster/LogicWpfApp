namespace Model.Logic.Operators.SingleOperators
{
    public abstract class BaseSingleOperator<T> : ISingleOperator<T>
    {
        private IValue<T> _operand;

        public IValue<T> Operand
        {
            get => _operand;
            set => _operand = value;
        }

        public abstract int Priority { get; }

        protected abstract T CalculateValue();

        public T GetValue()
        {
            if (Operand == null)
            {
                throw new InvalidOperationException($"Отсутствует операнд у {this}!");
            }
            return CalculateValue();
        }
    }
}
