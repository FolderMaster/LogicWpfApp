using System;

using ViewModel.Interfaces;

namespace View.Implementations.DialogServices
{
    public abstract class BaseWindowDialogService : IDialogService
    {
        private DialogWindow? _window;

        private object? _resultValue;

        public bool IsShow => _window != null;

        public object? ResultValue => _resultValue;

        public bool? ShowDialog(object? parameter = null)
        {
            if(IsShow)
            {
                throw new NotImplementedException();
            }
            else
            {
                _window = CreateWindow(parameter);
                _window.ShowDialog();
                var dialogResult = _window.ExtendedDialogResult;
                _resultValue = _window.ResultValue;
                _window = null;
                return dialogResult;
            }
        }

        protected abstract DialogWindow CreateWindow(object? parameter);
    }
}