namespace Model.Operators
{
    public interface ILogicOperator : ILogicValue
    {
        string OperationName { get; }

        string OperationChar { get; }

        int OperationPriority { get; }
    }
}