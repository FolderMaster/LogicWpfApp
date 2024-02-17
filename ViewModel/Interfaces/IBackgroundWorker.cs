namespace ViewModel.Interfaces
{
    public interface IBackgroundWorker
    {
        public void Run(Action action);

        public void RunAsync(Action action);
    }
}
