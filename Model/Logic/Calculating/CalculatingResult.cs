namespace Model.Logic.Calculating
{
    public class CalculatingResult<T>
    {
        public T Result { get; private set; }

        public Dictionary<string, T> Parameters { get; private set; }

        public CalculatingResult(T result, Dictionary<string, T> parameters)
        {
            Result = result;
            Parameters = parameters;
        }
    }
}
