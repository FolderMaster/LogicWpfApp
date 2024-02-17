using View.Windows;

using ViewModel;

namespace View.Implementations.DialogServices.Windows
{
    public class CalculatingOptionWindowDialogService : BaseWindowDialogService
    {
        private Session _session;

        public CalculatingOptionWindowDialogService(Session session)
        {
            _session = session;
        }

        protected override DialogWindow CreateWindow(object? parameter) =>
            new CalculatingOptionWindow(_session);
    }
}
