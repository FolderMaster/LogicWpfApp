using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace View.Implementations.DialogServices
{
    public class DialogWindow : Window
    {
        public bool? ExtendedDialogResult { get; set; }

        public object ResultValue
        {
            get => GetValue(ResultValueProperty);
            set => SetValue(ResultValueProperty, value);
        }

        public static readonly DependencyProperty ResultValueProperty =
            DependencyProperty.Register(nameof(ResultValue), typeof(object), typeof(DialogWindow),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty DialogResultInstance = 
            DependencyProperty.RegisterAttached(nameof(DialogResultInstance), typeof(bool?),
                typeof(DialogWindow), new PropertyMetadata(null, OnDialogResultInstanceChanged));

        public DialogWindow() : base() { }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            DialogResult = DialogResult ?? null;
        }

        public static bool? GetDialogResultInstance(UIElement target) =>
            (bool?)target.GetValue(DialogResultInstance);

        public static void SetDialogResultInstance(UIElement target, bool? value) =>
            target.SetValue(DialogResultInstance, value);

        private static void OnDialogResultInstanceChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is Button button)
            {
                var ownerWindow = (DialogWindow)GetWindow(button);
                var dialogResultInstance = GetDialogResultInstance(button);
                button.Click += (sender, args) =>
                {
                    ownerWindow.ExtendedDialogResult = dialogResultInstance;
                    ownerWindow.DialogResult = dialogResultInstance;
                };
            }
        }
    }
}
