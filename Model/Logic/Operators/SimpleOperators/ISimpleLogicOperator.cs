namespace Model.Logic.Operators.SimpleOperators
{
    public interface ISimpleLogicOperator : ILogicOperator
    {
        ILogicValue Operand { get; set; }
    }
}