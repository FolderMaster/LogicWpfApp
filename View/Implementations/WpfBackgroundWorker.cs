using System;
using System.Windows;

using ViewModel.Interfaces;

namespace View.Implementations
{
    public class WpfBackgroundWorker : IBackgroundWorker
    {
        public void Run(Action action) => Application.Current.Dispatcher.Invoke(action);
    }
}
