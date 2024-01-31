using View.Implementations.DialogServices;
using ViewModel;
using ViewModel.VMs;

namespace View.Windows
{
    public partial class EditExpressionWindow : DialogWindow
    {
        public EditExpressionWindow(Session session)
        {
            InitializeComponent();

            DataContext = new EditExpressionVM(session);
        }
    }
}
