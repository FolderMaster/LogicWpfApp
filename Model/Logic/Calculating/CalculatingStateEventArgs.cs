namespace Model.Logic.Calculating
{
    public class CalculatingStateEventArgs : EventArgs
    {
        public CalculatingState State { get; private set; }

        public CalculatingStateEventArgs(CalculatingState state) => State = state;
    }
}
