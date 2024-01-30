namespace Model.Logic.Operators
{
    public interface IOperator<T> : IValue<T>
    {
        public int Priority { get; }
    }
}