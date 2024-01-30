using Model.Logic.Operators;

namespace Model.Logic.Expressions
{
    public class ExpressionValueWrapper<T> : IValue<T>
    {
        public IValue<T> Value { get; set; }

        public ExpressionValueWrapper<T>? Parent { get; set; }

        public int Index { get; set; }

        public int Priority
        {
            get
            {
                if (Value is IOperator<T> op)
                {
                    return op.Priority;
                }
                else
                {
                    return -1;
                }
            }
        }

        public int Depth
        {
            get => Parent != null ? Parent.Depth + 1 : 0;
        }

        public ExpressionValueWrapper(IValue<T> value) => Value = value;

        public T GetValue() => Value.GetValue();
    }
}
