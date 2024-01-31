using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Model.Logic.Calculating;

using ViewModel.Interfaces;

namespace ViewModel.VMs
{
    public partial class MainVM : ObservableObject
    {
        private Session _session;

        private string _expressionText;

        [ObservableProperty]
        private ObservableCollection<CalculatingResult<bool>> calculatingResults = new();

        [ObservableProperty]
        private double calculatingProgressValue;

        public string ExpressionText
        {
            get => _expressionText;
            set => SetProperty(ref _expressionText, value);
        }

        public ICommand EditExpressionCommand { get; private set; }

        public ICommand SettingsCommand { get; private set; }

        public ICommand CalculateCommand { get; private set; }

        public MainVM(IDialogService errorDialog, IDialogService editExpressionDialog, Session session)
        {
            _session = session;
            _expressionText = _session.Expression.ToString();

            EditExpressionCommand = new RelayCommand(() =>
            {
                var dialogResult = editExpressionDialog.ShowDialog();
            });
            CalculateCommand = new RelayCommand(async () =>
            {
                CalculatingResults.Clear();
                var manager = new CalculatingManager<bool>();
                manager.AddedResultRow += CalculatingManager_AddedResultRow;
                await manager.Calculate(_session.Expression, _session.Variables,
                    (int)Math.Pow(2, _session.Variables.Count), Generate);
                manager.AddedResultRow -= CalculatingManager_AddedResultRow;
                CalculatingProgressValue = 0;
            });
        }

        private IEnumerable<IList<bool>> Generate()
        {
            var count = _session.Variables.Count;
            for (var i = 0; i < Math.Pow(2, count); ++i)
            {
                var value = i;
                var result = new List<bool>();
                for (var n = 0; n < count; ++n)
                {
                    result.Add((value & 1) == 1);
                    value = value >> 1;
                }
                yield return result;
            }
        }

        private void CalculatingManager_AddedResultRow(object? sender, CalculatingEventArgs<bool> args)
        {
            CalculatingResults.Add(args.ResultRow);
            CalculatingProgressValue = args.ProgressValue;
        }
    }
}