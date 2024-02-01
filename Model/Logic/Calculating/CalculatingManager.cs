using Model.Logic.Expressions;
using Model.Logic.Variables;

namespace Model.Logic.Calculating
{
    public class CalculatingManager<T>
    {
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

        public event EventHandler<CalculatingResultEventArgs<T>>? AddedResult;

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
            foreach (var values in Options.GenerateArgs())
            {
                if (State == CalculatingState.None)
                {
                    break;
                }
                if(State == CalculatingState.Pause)
                {
                    _pauseEvent.WaitOne();
                }
                if (values.Count != Variables.Count)
                {
                    throw new ArgumentException();
                }
                var dictionary = new Dictionary<INamedVariable<T>, T>();
                for (int n = 0; n < Variables.Count; ++n)
                {
                    dictionary.Add(Variables[n], values[n]);
                    Variables[n].SetValue(values[n]);
                }
                var value = Expression.GetValue();
                ++count;
                AddedResult?.Invoke(this, new CalculatingResultEventArgs<T>
                    (new CalculatingResult<T>(value, dictionary), count / (double)Options.IterationsCount));
            }
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
