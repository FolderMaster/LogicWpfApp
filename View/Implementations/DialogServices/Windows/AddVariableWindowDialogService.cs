using System.Windows;

using View.Windows;

namespace View.Implementations.DialogServices.Windows
{
    public class AddVariableWindowDialogService : BaseWindowDialogService
    {
        protected override Window CreateWindow(object? parameter) => new AddVariableWindow();
    }
}