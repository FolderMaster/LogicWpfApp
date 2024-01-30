namespace Model.Logic.Expressions
{
    public interface IExpression<T>: IValue<T>, IEnumerable<ExpressionValueWrapper<T>>
    {
        public int Count { get; }

        public IValue<T> this[int index] { get; set; }

        public void Add(IValue<T> value, int index = -1);

        public void Remove(int index);

        public void Clear();
    }
}
