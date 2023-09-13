using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace View.Windows
{
    public partial class MessageBoxWindow : Window
    {
        public MessageBoxWindow(ImageSource image, string title, string description,
            Dictionary<bool, string> buttonsContents)
        {
            Icon = image;
            Title = title;

            InitializeComponent();

            textBox.Text = description;
            CreateButtons(buttonsContents);
        }

        private void CreateButtons(Dictionary<bool, string> buttonsContents)
        {
            foreach (var content in buttonsContents)
            {
                var button = new Button();
                button.Content = content.Value;
                button.Click += (object sender, RoutedEventArgs e) =>
                {
                    DialogResult = content.Key;
                };
                buttonWrap.Children.Add(button);
            }
        }
    }
}