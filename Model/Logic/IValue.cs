namespace Model
{
    public interface IValue<T> : ICloneable
    {
        public T GetValue();
    }
}