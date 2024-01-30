namespace Model.Logic.Operators.PairOperators
{
    public class EqualOperator : BasePairOperator<bool>
    {
        public override int Priority => 1;

        protected override bool CalculateValue() =>
            LeftOperand.GetValue() == RightOperand.GetValue();

        public override string ToString() => "=";
    }
}