namespace Model.Logic.Variables
{
    public interface IVariable<T>: IValue<T>
    {
        public void SetValue(T value);
    }
}