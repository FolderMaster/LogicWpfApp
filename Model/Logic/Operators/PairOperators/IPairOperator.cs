namespace Model.Logic.Operators.PairOperators
{
    public interface IPairOperator<T> : IOperator<T>
    {
        IValue<T> LeftOperand { get; set; }

        IValue<T> RightOperand { get; set; }
    }
}