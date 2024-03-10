namespace Model.Logic.Operators.PairOperators
{
    public class EqualOperator : BasePairOperator<bool>
    {
        public override int Priority => 1;

        public EqualOperator() : base("=") { }

        protected override bool CalculateValue() =>
            LeftOperand.GetValue() == RightOperand.GetValue();
    }
}