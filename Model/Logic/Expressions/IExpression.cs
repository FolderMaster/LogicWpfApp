using Model.Logic.Variables;

namespace Model.Logic.Expressions
{
    public interface IExpression<T>: IValue<T>, IEnumerable<ExpressionValueWrapper<T>>, ICloneable
    {
        public int Count { get; }

        public IValue<T> this[int index] { get; set; }

        public void Add(IValue<T> value, int index = -1);

        public void Remove(int index);

        public IEnumerable<INamedVariable<T>> GetVariables();

        public void Clear();
    }
}
