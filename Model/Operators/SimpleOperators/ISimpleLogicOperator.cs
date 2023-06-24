namespace Model.Operators.SimpleOperators
{
    public interface ISimpleLogicOperator : ILogicOperator
    {
        ILogicValue Operand { get; set; }
    }
}