using System.ComponentModel;
using System.Windows;

namespace View.Implementations.DialogServices
{
    public class DialogWindow : Window
    {
        public bool? ExtendedDialogResult { get; set; }

        public DialogWindow() : base() { }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            DialogResult = DialogResult ?? null;
        }
    }
}
