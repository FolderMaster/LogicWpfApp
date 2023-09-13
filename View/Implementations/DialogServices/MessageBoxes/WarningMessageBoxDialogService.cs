using System.Collections.Generic;
using System.Windows.Media;

using ViewModel.Interfaces;

namespace View.Implementations.DialogServices.MessageBoxes
{
    public class WarningMessageBoxDialogService : BaseMessageBoxDialogService
    {
        protected override ImageSource Image =>
            (ImageSource)_resourceService.GetResource("WarningIcon");

        protected override string Title => (string)_resourceService.GetResource("WarningTitle");

        protected override Dictionary<bool, string> ButtonsContents => new()
        {
            [true] = (string)_resourceService.GetResource("OKHeader"),
            [false] = (string)_resourceService.GetResource("CancelHeader")
        };

        public WarningMessageBoxDialogService(IResourceService resourceService) :
            base(resourceService) { }
    }
}