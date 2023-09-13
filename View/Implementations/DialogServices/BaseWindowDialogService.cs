using System;
using System.Windows;

using ViewModel.Interfaces;

namespace View.Implementations.DialogServices
{
    public abstract class BaseWindowDialogService : IDialogService
    {
        private Window? _window;

        public bool IsShow => _window != null;

        public bool? ShowDialog(object? parameter = null)
        {
            if(IsShow)
            {
                throw new NotImplementedException();
            }
            else
            {
                _window = CreateConfiguredWindow(parameter);
                return _window.ShowDialog();
            }
        }

        protected abstract Window CreateWindow(object? parameter);

        private Window CreateConfiguredWindow(object? parameter)
        {
            var window = CreateWindow(parameter);
            window.Closed += Window_Closed;
            return window;
        }

        private void Window_Closed(object? sender, EventArgs e) => _window = null;
    }
}