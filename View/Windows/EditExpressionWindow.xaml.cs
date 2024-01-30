using View.Implementations.DialogServices;
using ViewModel.VMs;

namespace View.Windows
{
    /// <summary>
    /// Interaction logic for EditExpressionWindow.xaml
    /// </summary>
    public partial class EditExpressionWindow : DialogWindow
    {
        public EditExpressionWindow()
        {
            InitializeComponent();

            DataContext = new EditExpressionVM();
        }
    }
}
