namespace Model.Operators.PairOperators
{
    public interface IPairLogicOperator : ILogicOperator
    {
        ILogicValue LeftOperand { get; set; }

        ILogicValue RightOperand { get; set; }
    }
}