namespace Model.Logic.Operators.SingleOperators
{
    public interface ISingleOperator<T> : IOperator<T>
    {
        IValue<T> Operand { get; set; }
    }
}