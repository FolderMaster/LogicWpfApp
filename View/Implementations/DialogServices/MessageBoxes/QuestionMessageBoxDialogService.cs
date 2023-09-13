using System.Collections.Generic;
using System.Windows.Media;

using ViewModel.Interfaces;

namespace View.Implementations.DialogServices.MessageBoxes
{
    public class QuestionMessageBoxDialogService : BaseMessageBoxDialogService
    {
        protected override ImageSource Image =>
            (ImageSource)_resourceService.GetResource("QuestionIcon");

        protected override string Title => (string)_resourceService.GetResource("QuestionTitle");

        protected override Dictionary<bool, string> ButtonsContents => new()
        {
            [true] = (string)_resourceService.GetResource("YesHeader"),
            [false] = (string)_resourceService.GetResource("NoHeader")
        };

        public QuestionMessageBoxDialogService(IResourceService resourceService) :
            base(resourceService) { }
    }
}