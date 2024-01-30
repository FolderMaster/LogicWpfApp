using System;
using ViewModel.Interfaces;

namespace View.Implementations.DialogServices
{
    public abstract class BaseWindowDialogService : IDialogService
    {
        private DialogWindow? _window;

        public bool IsShow => _window != null;

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
                _window = null;
                return dialogResult;
            }
        }

        protected abstract DialogWindow CreateWindow(object? parameter);
    }
}