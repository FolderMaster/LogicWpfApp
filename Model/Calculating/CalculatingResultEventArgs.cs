namespace Model.Calculating
{
    public class CalculatingResultEventArgs<T> : EventArgs
    {
        public IEnumerable<CalculatingResult<T>> Result { get; private set; }

        public CalculatingResultEventArgs(IEnumerable<CalculatingResult<T>> result)
        {
            Result = result;
        }
    }
}
