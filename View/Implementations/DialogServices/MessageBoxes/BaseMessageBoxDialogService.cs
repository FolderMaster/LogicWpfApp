using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

using View.Windows;

using ViewModel.Interfaces;

namespace View.Implementations.DialogServices.MessageBoxes
{
    public abstract class BaseMessageBoxDialogService : BaseWindowDialogService
    {
        protected IResourceService _resourceService;

        protected abstract ImageSource Image { get; }

        protected abstract string Title { get; }

        protected abstract Dictionary<bool, string> ButtonsContents { get; }

        public BaseMessageBoxDialogService(IResourceService resourceService) : base() =>
            _resourceService = resourceService;

        protected override Window CreateWindow(object? parameter) =>
            new MessageBoxWindow(Image, Title, parameter?.ToString() ?? "", ButtonsContents);
    }
}