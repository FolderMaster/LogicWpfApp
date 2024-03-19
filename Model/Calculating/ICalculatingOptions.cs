namespace Model.Calculating
{
    public interface ICalculatingOptions<T>: ICloneable
    {
        public IEnumerable<IList<T>> GenerateArgs();

        public int IterationsCount { get; }
    }
}
