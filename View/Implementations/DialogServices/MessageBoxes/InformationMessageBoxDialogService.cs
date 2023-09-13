using System.Collections.Generic;
using System.Windows.Media;

using ViewModel.Interfaces;

namespace View.Implementations.DialogServices.MessageBoxes
{
    public class InformationMessageBoxDialogService : BaseMessageBoxDialogService
    {
        protected override ImageSource Image =>
            (ImageSource)_resourceService.GetResource("InformationIcon");

        protected override string Title =>
            (string)_resourceService.GetResource("InformationTitle");

        protected override Dictionary<bool, string> ButtonsContents => new()
            { [true] = (string)_resourceService.GetResource("OKHeader") };

        public InformationMessageBoxDialogService(IResourceService resourceService) :
            base(resourceService) { }
    }
}