namespace Model.Logic.Operators
{
    public interface ILogicOperator : ILogicValue
    {
        string OperationName { get; }

        string OperationChar { get; }

        int OperationPriority { get; }
    }
}