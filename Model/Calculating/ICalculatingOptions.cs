namespace Model.Calculating
{
    public interface ICalculatingOptions<T>
    {
        public IEnumerable<IList<T>> GenerateArgs();

        public int IterationsCount { get; }
    }
}
