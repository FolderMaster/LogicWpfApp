using System.Collections.Generic;
using System.Windows.Media;

using ViewModel.Interfaces;

namespace View.Implementations.DialogServices.MessageBoxes
{
    public class ErrorMessageBoxDialogService : BaseMessageBoxDialogService
    {
        protected override ImageSource Image =>
            (ImageSource)_resourceService.GetResource("ErrorIcon");

        protected override string Title => (string)_resourceService.GetResource("ErrorTitle");

        protected override Dictionary<bool, string> ButtonsContents => new()
            { [true] = (string)_resourceService.GetResource("OKHeader") };

        public ErrorMessageBoxDialogService(IResourceService resourceService) :
            base(resourceService) { }
    }
}