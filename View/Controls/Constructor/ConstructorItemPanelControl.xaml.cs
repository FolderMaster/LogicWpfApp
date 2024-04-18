using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Model.Logic;

namespace View.Controls.Constructor
{
    /// <summary>
    /// Interaction logic for ConstructorItemPanelControl.xaml
    /// </summary>
    public partial class ConstructorItemPanelControl : UserControl
    {
        public ObservableCollection<object> Patterns { get; } = new();

        public ConstructorItemPanelControl()
        {
            InitializeComponent();

            CreatePatterns();
        }

        private void CreatePatterns()
        {
            var types = Assembly.GetAssembly(typeof(IValue<>)).GetTypes();
            foreach (var type in types)
            {
                if (type.GetInterfaces().Contains(typeof(IValue<bool>)) && !type.IsAbstract)
                {
                    Patterns.Add(Activator.CreateInstance(type));
                }
            }
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem != null)
            {
                var item = listBox.SelectedItem as IValue<bool>;
                var dataObject = new DataObject();
                dataObject.SetData(DataFormats.Text, item.ToString(), true);

                DragDrop.DoDragDrop(listBox, dataObject, DragDropEffects.Copy);
            }
        }
    }
}
