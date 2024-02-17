namespace Model.Logic.Calculating
{
    public class CalculatingProgressEventArgs : EventArgs
    {
        public double ProgressValue { get; private set; }

        public CalculatingProgressEventArgs(double progressValue)
        {
            ProgressValue = progressValue;
        }
    }
}
