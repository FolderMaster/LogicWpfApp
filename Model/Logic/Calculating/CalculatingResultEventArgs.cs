namespace Model.Logic.Calculating
{
    public class CalculatingResultEventArgs<T> : EventArgs
    {
        public CalculatingResult<T> ResultRow { get; private  set; }

        public double ProgressValue { get; private set; }

        public CalculatingResultEventArgs(CalculatingResult<T> resultRow, double progressValue)
        {
            ResultRow = resultRow;
            ProgressValue = progressValue;
        }
    }
}
