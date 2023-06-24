namespace Model.Variables
{
    public interface ILogicVariable : ILogicValue
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public Type Type { get; }
    }
}