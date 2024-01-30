namespace Model.Logic.Variables
{
    public interface INamedVariable<T> : IVariable<T>
    {
        public string Name { get; set; }
    }
}
