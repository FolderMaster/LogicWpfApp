namespace Model.Logic.Operators.PairOperators
{
    public class ImplicateOperator : BasePairOperator<bool>
    {
        public override int Priority => 1;

        public ImplicateOperator() : base("=>") { }

        protected override bool CalculateValue() =>
            !LeftOperand.GetValue() || RightOperand.GetValue();
    }
}