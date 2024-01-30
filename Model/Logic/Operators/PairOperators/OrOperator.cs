namespace Model.Logic.Operators.PairOperators
{
    public class OrOperator : BasePairOperator<bool>
    {
        public override int Priority => 1;

        protected override bool CalculateValue() =>
            LeftOperand.GetValue() || RightOperand.GetValue();

        public override string ToString() => "|";
    }
}