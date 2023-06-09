﻿namespace Model.Operators.PairOperators
{
    public class EqualOperator : IPairLogicOperator
    {
        private static readonly string _operationName = "Equal";

        private static readonly string _operationChar = "=";

        private static readonly int _operationPriority = 1;

        public ILogicValue LeftOperand { get; set; } = null!;

        public ILogicValue RightOperand { get; set; } = null!;

        public bool? Bool => LeftOperand.Bool == RightOperand.Bool;

        public int OperationPriority => _operationPriority;

        public string OperationName => _operationName;

        public string OperationChar => _operationChar;

        public override string ToString() => $"{LeftOperand} {OperationChar} {RightOperand}";
    }
}