namespace Model.Logic.Operators.SingleOperators
{
    public class NotOperator : BaseSingleOperator<bool>
    {
        public override int Priority => 0;

        protected override bool CalculateValue() => !Operand.GetValue();

        public override string ToString() => "!";
    }
}