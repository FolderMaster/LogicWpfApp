namespace ViewModel.Interfaces
{
    public interface IDialogService
    {
        public bool? ShowDialog(object? parameter = null);

        public bool IsShow { get; }
    }
}