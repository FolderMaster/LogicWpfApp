using System.Windows;

using View.Implementations.DialogServices.MessageBoxes;
using View.Implementations.DialogServices.Windows;

using ViewModel;
using ViewModel.Interfaces;
using ViewModel.VMs;

namespace View.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow(ErrorMessageBoxDialogService errorDialog,
            InformationMessageBoxDialogService informationDialog,
            EditExpressionWindowDialogService editExpressionDialog,
            CalculatingOptionWindowDialogService calculatingOptionDialog,
            IBackgroundWorker backgroundWorker, Session session)
        {
            InitializeComponent();

            DataContext = new MainVM(errorDialog, informationDialog,
                editExpressionDialog, calculatingOptionDialog, backgroundWorker, session);
        }
    }
}