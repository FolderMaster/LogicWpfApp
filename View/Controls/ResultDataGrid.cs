using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.Specialized;
using Model.Logic.Variables;
using Model.Calculating;

namespace View.Controls
{
    public class ResultDataGrid : DataGrid
    {
        private readonly int _defaultCount = 0;

        public ObservableCollection<INamedVariable<bool>> Variables
        {
            get => (ObservableCollection<INamedVariable<bool>>)GetValue(VariablesProperty);
            set => SetValue(VariablesProperty, value);
        }

        public static DependencyProperty VariablesProperty = DependencyProperty.Register
            (nameof(Variables), typeof(ObservableCollection<INamedVariable<bool>>),
            typeof(ResultDataGrid), new PropertyMetadata(OnVariablesChanged));

        public ResultDataGrid() : base()
        {
            AutoGenerateColumns = false;
            _defaultCount = CreateDefaultColumns();
        }

        private static void OnVariablesChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = sender as ResultDataGrid;
            var oldVariables = e.OldValue as
                ObservableCollection<INamedVariable<bool>>;
            if (oldVariables != null)
            {
                for (var i = 0; i < oldVariables.Count; ++i)
                {
                    dataGrid.Columns.RemoveAt(i + dataGrid._defaultCount);
                }
                oldVariables.CollectionChanged -=
                    dataGrid.Variables_CollectionChanged;
            }
            var newVariables = e.NewValue as
                ObservableCollection<INamedVariable<bool>>;
            if (newVariables != null)
            {
                foreach (var variable in newVariables)
                {
                    dataGrid.CreateNewColumn(variable);
                }
                newVariables.CollectionChanged +=
                    dataGrid.Variables_CollectionChanged;
            }
        }

        private DataGridColumn CreateColumn(string header, string bindingPath)
        {
            var column = new DataGridCheckBoxColumn();
            column.Header = header;
            column.Binding = new Binding(bindingPath)
            {
                Mode = BindingMode.OneWay
            };
            return column;
        }

        private int CreateDefaultColumns()
        {
            var name = nameof(CalculatingResult<bool>.Result);
            var column = CreateColumn(name, name);
            Columns.Add(column);
            return Columns.Count;
        }

        private void CreateNewColumn(INamedVariable<bool> variable,
            int index = -1)
        {
            if (index == -1)
            {
                index = Columns.Count;
            }
            var name = variable.Name;
            var column = CreateColumn(name,
                $"{nameof(CalculatingResult<bool>.Parameters)}[{name}]");
            Columns.Insert(index, column);
        }

        private void Variables_CollectionChanged(object? sender,
            NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    CreateNewColumn(e.NewItems[0] as INamedVariable<bool>,
                        e.NewStartingIndex + _defaultCount);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Columns.RemoveAt(e.OldStartingIndex + _defaultCount);
                    break;
            }
        }
    }
}
