namespace Model.Logic.Operators.SingleOperators
{
    public class NotOperator : BaseSingleOperator<bool>
    {
        public override int Priority => 0;

        public NotOperator() : base("!") { }

        protected override bool CalculateValue() => !Operand.GetValue();
    }
}