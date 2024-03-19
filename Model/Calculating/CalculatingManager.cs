using Model.Logic.Expressions;
using Model.Logic.Variables;

namespace Model.Calculating
{
    public class CalculatingManager<T>: ICalculatingManager<T>
    {
        private static int _updateFrequency = 1;

        private Action<string> _errorAction;

        private AutoResetEvent _pauseEvent = new(true);

        private CalculatingState _state = CalculatingState.None;

        public IExpression<T>? Expression { get; set; }

        public IList<INamedVariable<T>>? Variables { get; set; }

        public ICalculatingOptions<T>? Options { get; set; }

        public CalculatingState State
        {
            get => _state;
            private set
            {
                if (_state != value)
                {
                    _state = value;
                    StateChanged?.Invoke(this, new CalculatingStateEventArgs(value));
                }
            }
        }

        public event EventHandler<CalculatingStateEventArgs>? StateChanged;

        public event EventHandler<CalculatingProgressEventArgs>? ProgressUpdated;

        public event EventHandler<CalculatingResultEventArgs<T>>? ResultCalculated;

        public CalculatingManager(Action<string> errorAction) => _errorAction = errorAction;

        public void StartCalculate()
        {
            State = CalculatingState.Run;
            var thread = new Thread(Calculate)
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void Calculate()
        {
            var result = new List<CalculatingResult<T>>();
            var count = 0;
            try
            {
                Parallel.ForEach(Options.GenerateArgs(), (values, loopState) =>
                {
                    if (State == CalculatingState.None)
                    {
                        loopState.Stop();
                    }
                    if (State == CalculatingState.Pause)
                    {
                        _pauseEvent.WaitOne();
                    }
                    if (values.Count != Variables.Count)
                    {
                        throw new ArgumentException();
                    }
                    var dictionary = new Dictionary<string, T>();
                    lock (this)
                    {
                        for (int n = 0; n < Variables.Count; ++n)
                        {
                            dictionary.Add(Variables[n].Name, values[n]);
                            Variables[n].SetValue(values[n]);
                        }
                        var value = Expression.GetValue();
                        result.Add(new CalculatingResult<T>(value, dictionary));
                    }
                    ++count;
                    if (count % _updateFrequency == 0 || count == Options.IterationsCount)
                    {
                        ProgressUpdated?.Invoke(this, new CalculatingProgressEventArgs
                            (count / (double)Options.IterationsCount));
                    }
                });
            }
            catch (Exception ex)
            {
                State = CalculatingState.Error;
                _errorAction.Invoke(ex.Message);
            }
            ResultCalculated?.Invoke(this, new CalculatingResultEventArgs<T>(result));
            State = CalculatingState.None;
        }

        public void StopCalculate()
        {
            State = CalculatingState.None;
        }

        public void PauseCalculate()
        {
            State = CalculatingState.Pause;
            _pauseEvent.Reset();
        }

        public void ResumeCalculate()
        {
            _pauseEvent.Set();
            State = CalculatingState.Run;
        }
    }
}
