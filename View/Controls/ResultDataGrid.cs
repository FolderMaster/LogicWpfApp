using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;

using Model.Logic.Variables;
using Model.Calculating;

namespace View.Controls
{
    public class ResultDataGrid : DataGrid
    {
        private readonly int _defaultCount = 0;

        public IEnumerable<INamedVariable<bool>> Variables
        {
            get => (IEnumerable<INamedVariable<bool>>)GetValue(VariablesProperty);
            set => SetValue(VariablesProperty, value);
        }

        public static DependencyProperty VariablesProperty = DependencyProperty.Register
            (nameof(Variables), typeof(IEnumerable<INamedVariable<bool>>),
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
                IEnumerable<INamedVariable<bool>>;
            if (oldVariables != null)
            {
                for (var i = 0; i < oldVariables.Count(); ++i)
                {
                    dataGrid.Columns.RemoveAt(dataGrid._defaultCount);
                }
            }
            var newVariables = e.NewValue as
                IEnumerable<INamedVariable<bool>>;
            if (newVariables != null)
            {
                foreach (var variable in newVariables)
                {
                    dataGrid.CreateNewColumn(variable);
                }
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
