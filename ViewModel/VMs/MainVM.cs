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
        private CalculatingManager<bool> _calculatingManager = new();

        private Session _session;

        private IBackgroundWorker _backgroundWorker;

        private string _expressionText;

        [ObservableProperty]
        private ObservableCollection<CalculatingResult<bool>> calculatingResults = new();

        [ObservableProperty]
        private double calculatingProgressValue;

        [ObservableProperty]
        private CalculatingState calculatingProgressState;

        public string ExpressionText
        {
            get => _expressionText;
            set => SetProperty(ref _expressionText, value);
        }

        public RelayCommand EditExpressionCommand { get; private set; }

        public RelayCommand SettingsCommand { get; private set; }

        public RelayCommand StartCommand { get; private set; }

        public RelayCommand StopCommand { get; private set; }

        public RelayCommand PauseCommand { get; private set; }

        public RelayCommand ResumeCommand { get; private set; }

        public MainVM(IDialogService errorDialog, IDialogService editExpressionDialog,
            IBackgroundWorker backgroundWorker, Session session)
        {
            _session = session;
            _backgroundWorker = backgroundWorker;
            _expressionText = _session.Expression.ToString();
            _calculatingManager.Expression = session.Expression;
            _calculatingManager.Variables = session.Variables;
            _calculatingManager.Options = new BoolCalculatingOptions(session.Variables.Count);
            EditExpressionCommand = new RelayCommand(() =>
            {
                var dialogResult = editExpressionDialog.ShowDialog();
            });
            StartCommand = new RelayCommand(() =>
            {
                CalculatingResults.Clear();
                _calculatingManager.AddedResult += CalculatingManager_AddedResult;
                _calculatingManager.StateChanged += CalculatingManager_StateChanged;
                _calculatingManager.StartCalculate();
            }, () => CalculatingProgressState == CalculatingState.None);
            StopCommand = new RelayCommand(_calculatingManager.StopCalculate,
                () => CalculatingProgressState == CalculatingState.Run);
            PauseCommand = new RelayCommand(_calculatingManager.PauseCalculate,
                () => CalculatingProgressState == CalculatingState.Run);
            ResumeCommand = new RelayCommand(_calculatingManager.ResumeCalculate,
                () => CalculatingProgressState == CalculatingState.Pause);
        }

        private void CalculatingManager_StateChanged(object? sender, CalculatingStateEventArgs e)
        {
            _backgroundWorker.Run(() => {
                CalculatingProgressState = e.State;
                StartCommand.NotifyCanExecuteChanged();
                StopCommand.NotifyCanExecuteChanged();
                PauseCommand.NotifyCanExecuteChanged();
                ResumeCommand.NotifyCanExecuteChanged();
            });
            if (e.State == CalculatingState.None)
            {
                _calculatingManager.AddedResult -= CalculatingManager_AddedResult;
                _calculatingManager.StateChanged -= CalculatingManager_StateChanged;
                _backgroundWorker.Run(() => CalculatingProgressValue = 0);
            }
        }

        private void CalculatingManager_AddedResult(object? sender, CalculatingResultEventArgs<bool> e)
        {
            _backgroundWorker.Run(() =>
            {
                CalculatingResults.Add(e.ResultRow);
                CalculatingProgressValue = e.ProgressValue;
            });
        }
    }
}