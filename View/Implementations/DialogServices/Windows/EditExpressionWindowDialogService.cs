using View.Windows;

namespace View.Implementations.DialogServices.Windows
{
    public class EditExpressionWindowDialogService : BaseWindowDialogService
    {
        protected override DialogWindow CreateWindow(object? parameter) =>
            new EditExpressionWindow();
    }
}
