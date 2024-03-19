using Model.Logic.Expressions;
using Model.Logic.Variables;

namespace Model.Calculating
{
    public interface ICalculatingManager<T>
    {
        public IExpression<T>? Expression { get; set; }

        public IList<INamedVariable<T>>? Variables { get; set; }

        public ICalculatingOptions<T>? Options { get; set; }

        public CalculatingState State { get; }

        public event EventHandler<CalculatingStateEventArgs>? StateChanged;

        public event EventHandler<CalculatingProgressEventArgs>? ProgressUpdated;

        public event EventHandler<CalculatingResultEventArgs<T>>? ResultCalculated;

        public void StartCalculate();

        public void StopCalculate();

        public void PauseCalculate();

        public void ResumeCalculate();
    }
}
