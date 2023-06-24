namespace Model.Operators.SimpleOperators
{
    public class NotOperator : ISimpleLogicOperator
    {
        private static readonly string _operationName = "Not";

        private static readonly string _operationChar = "!";

        private static readonly int _operationPriority = 1;

        public ILogicValue Operand { get; set; } = null!;

        public bool? Bool => !Operand.Bool;

        public int OperationPriority => _operationPriority;

        public string OperationName => _operationName;

        public string OperationChar => _operationChar;

        public override string ToString() => $"{OperationChar}{Operand}";
    }
}