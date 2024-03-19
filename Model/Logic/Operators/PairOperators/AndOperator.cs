namespace Model.Logic.Operators.PairOperators
{
    public class AndOperator : BasePairOperator<bool>
    {
        public override int Priority => 1;

        public AndOperator() : base("&") { }

        protected override bool CalculateValue() =>
            LeftOperand.GetValue() && RightOperand.GetValue();

        public override object Clone() => new AndOperator();
    }
}