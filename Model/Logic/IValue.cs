namespace Model.Logic
{
    public interface IValue<T> : ICloneable
    {
        public T GetValue();
    }
}