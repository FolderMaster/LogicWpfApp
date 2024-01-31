using System.Windows;

using View.Implementations.DialogServices.MessageBoxes;
using View.Implementations.DialogServices.Windows;

using ViewModel;
using ViewModel.VMs;

namespace View.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow(ErrorMessageBoxDialogService errorDialog,
            EditExpressionWindowDialogService editExpressionDialog, Session session)
        {
            InitializeComponent();

            DataContext = new MainVM(errorDialog, editExpressionDialog, session);
        }
    }
}