namespace ViewModel.Interfaces
{
    public interface IBackgroundWorker
    {
        public void Run(Action action);
    }
}
