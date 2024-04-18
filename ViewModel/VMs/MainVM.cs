using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Model.Calculating;
using Model.Logic.Expressions;
using Model.Logic.Variables;

using ViewModel.Interfaces;

namespace ViewModel.VMs
{
    public partial class MainVM : ObservableObject
    {
        private Stopwatch _stopwatch = new Stopwatch();

        private CalculatingManager<bool> _calculatingManager;

        [ObservableProperty]
        private Session session;

        private IBackgroundWorker _backgroundWorker;

        private IDialogService _informationDialog;

        [ObservableProperty]
        private string expressionText;

        [ObservableProperty]
        private IEnumerable<CalculatingResult<bool>>? calculatingResults = null;

        [ObservableProperty]
        private double calculatingProgressValue;

        [ObservableProperty]
        private CalculatingState calculatingProgressState;

        [ObservableProperty]
        private IEnumerable<INamedVariable<bool>> variables;

        public RelayCommand EditExpressionCommand { get; private set; }

        public RelayCommand SettingsCommand { get; private set; }

        public RelayCommand StartCommand { get; private set; }

        public RelayCommand StopCommand { get; private set; }

        public RelayCommand PauseCommand { get; private set; }

        public RelayCommand ResumeCommand { get; private set; }

        public MainVM(IDialogService errorDialog, IDialogService informationDialog,
            IDialogService editExpressionDialog, IDialogService calculatingOptionDialog,
            IBackgroundWorker backgroundWorker, Session session)
        {
            Session = session;
            _backgroundWorker = backgroundWorker;
            _informationDialog = informationDialog;
            _calculatingManager = new((m) => _backgroundWorker.Run(() => errorDialog.ShowDialog(m)));
            _calculatingManager.ProgressUpdated += CalculatingManager_ProgressUpdated;
            _calculatingManager.StateChanged += CalculatingManager_StateChanged;
            _calculatingManager.ResultCalculated += CalculatingManager_ResultCalculated;
            UpdateExpressionValues();
            SettingsCommand = new RelayCommand(() =>
            {
                if (calculatingOptionDialog.ShowDialog() == true)
                {
                    Session.CalculatingOptions = (ICalculatingOptions<bool>)calculatingOptionDialog.ResultValue;
                }
            });
            EditExpressionCommand = new RelayCommand(() =>
            {
                if (editExpressionDialog.ShowDialog() == true)
                {
                    Session.Expression = (IExpression<bool>)editExpressionDialog.ResultValue;
                    UpdateExpressionValues();
                }
            });
            StartCommand = new RelayCommand(() =>
            {
                _stopwatch.Start();
                _calculatingManager.Expression = Session.Expression;
                _calculatingManager.Variables = Session.Expression.GetVariables().ToList();
                _calculatingManager.Options = Session.CalculatingOptions;
                CalculatingResults = null;
                _calculatingManager.StartCalculate();
            }, () => CalculatingProgressState == CalculatingState.None);
            StopCommand = new RelayCommand(_calculatingManager.StopCalculate,
                () => CalculatingProgressState == CalculatingState.Run);
            PauseCommand = new RelayCommand(_calculatingManager.PauseCalculate,
                () => CalculatingProgressState == CalculatingState.Run);
            ResumeCommand = new RelayCommand(_calculatingManager.ResumeCalculate,
                () => CalculatingProgressState == CalculatingState.Pause);
        }

        private void UpdateExpressionValues()
        {
            if (Session.Expression != null)
            {
                Variables = Session.Expression.GetVariables();
                Session.CalculatingOptions = new BoolCalculatingOptions(Variables);
                ExpressionText = Session.Expression.ToString();
            }
            else
            {

            }
        }

        private void CalculatingManager_ResultCalculated(object? sender,
            CalculatingResultEventArgs<bool> e)
        {
            _backgroundWorker.Run(() => CalculatingResults = e.Result);
        }

        private void CalculatingManager_ProgressUpdated(object? sender,
            CalculatingProgressEventArgs e)
        {
            _backgroundWorker.Run(() => CalculatingProgressValue = e.ProgressValue);
        }

        private void CalculatingManager_StateChanged(object? sender, CalculatingStateEventArgs e)
        {
            _backgroundWorker.Run(() =>
            {
                CalculatingProgressState = e.State;
                StartCommand.NotifyCanExecuteChanged();
                StopCommand.NotifyCanExecuteChanged();
                PauseCommand.NotifyCanExecuteChanged();
                ResumeCommand.NotifyCanExecuteChanged();
            });
            if (e.State == CalculatingState.None)
            {
                _stopwatch.Stop();
                _backgroundWorker.Run(() =>
                {
                    CalculatingProgressValue = 0;
                    _informationDialog.ShowDialog($"Completed!\nTime:{_stopwatch.Elapsed}");
                });
            }
        }
    }
}