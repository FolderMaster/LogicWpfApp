using Model.Logic.Operators.PairOperators;
using Model.Logic.Operators.SingleOperators;

namespace Model.Logic.Expressions
{
    public static class ExpressionHelper<T>
    {
        public static ExpressionValueWrapper<T>? GetRightValue
            (ExpressionValueWrapper<T> value)
        {
            return (value.Value) switch
            {
                ISingleOperator<T> singleOperator =>
                    singleOperator.Operand as ExpressionValueWrapper<T>,
                IPairOperator<T> pairOperator =>
                    pairOperator.RightOperand as ExpressionValueWrapper<T>,
                _ => null
            };
        }

        public static void SetRightValue(ExpressionValueWrapper<T> parent,
            ExpressionValueWrapper<T> value)
        {
            switch (parent.Value)
            {
                case ISingleOperator<T> singleOperator:
                    singleOperator.Operand = value;
                    SetParent(value, parent);
                    break;
                case IPairOperator<T> pairOperator:
                    pairOperator.RightOperand = value;
                    SetParent(value, parent);
                    break;
            };
        }

        public static ExpressionValueWrapper<T>? GetLeftValue
            (ExpressionValueWrapper<T> value)
        {
            return (value.Value) switch
            {
                IPairOperator<T> pairOperator =>
                    pairOperator.LeftOperand as ExpressionValueWrapper<T>,
                _ => null
            };
        }

        public static void SetLeftValue(ExpressionValueWrapper<T> parent,
            ExpressionValueWrapper<T> value)
        {
            switch (parent.Value)
            {
                case IPairOperator<T> pairOperator:
                    pairOperator.LeftOperand = value;
                    SetParent(value, parent);
                    break;
            };
        }

        private static void SetParent(ExpressionValueWrapper<T>? value,
            ExpressionValueWrapper<T> parent)
        {
            if(value != null)
            {
                value.Parent = parent;
            }
        }
    }
}
