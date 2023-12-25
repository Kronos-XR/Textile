using System.ComponentModel;

namespace Textile.ViewModels.Interfaces
{
    public interface IViewModelsResolver
    {
        INotifyPropertyChanged GetViewModelInstance(string alias);
    }
}
