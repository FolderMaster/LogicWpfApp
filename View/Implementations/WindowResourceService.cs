using System.Windows;

using ViewModel.Interfaces;

namespace View.Implementations
{
    public class WindowResourceService : IResourceService
    {
        public object GetResource(string resourceKey) =>
            Application.Current.Resources[resourceKey];
    }
}