namespace Model.Logic.Operators.PairOperators
{
    public class XorOperator : BasePairOperator<bool>
    {
        public override int Priority => 1;

        public XorOperator() : base(@"\^") { }

        protected override bool CalculateValue() =>
            LeftOperand.GetValue() ^ RightOperand.GetValue();
    }
}