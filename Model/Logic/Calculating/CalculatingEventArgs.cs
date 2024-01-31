namespace Model.Logic.Calculating
{
    public class CalculatingEventArgs<T> : EventArgs
    {
        public CalculatingResult<T> ResultRow { get; set; }

        public double ProgressValue { get; set; }

        public CalculatingEventArgs(CalculatingResult<T> resultRow, double progressValue)
        {
            ResultRow = resultRow;
            ProgressValue = progressValue;
        }
    }
}
