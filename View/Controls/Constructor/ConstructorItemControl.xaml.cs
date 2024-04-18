using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace View.Controls.Constructor
{
    /// <summary>
    /// Interaction logic for ConstructorItemControl.xaml
    /// </summary>
    public partial class ConstructorItemControl : UserControl
    {
        public string Text { get; private set; }

        public ConstructorItemControl(object content)
        {
            Text = content?.ToString();

            InitializeComponent();
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Canvas.SetLeft(this, Canvas.GetLeft(this) + e.HorizontalChange);
            Canvas.SetTop(this, Canvas.GetTop(this) + e.VerticalChange);
        }
    }
}
