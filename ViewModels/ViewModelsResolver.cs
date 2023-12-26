using System;
using System.Collections.Generic;
using System.ComponentModel;
using Textile.ViewModels.Interfaces;

namespace Textile.ViewModels
{
    public class ViewModelsResolver : IViewModelsResolver
    {

        private readonly Dictionary<string, Func<INotifyPropertyChanged>> _vmResolvers = new Dictionary<string, Func<INotifyPropertyChanged>>();

        public ViewModelsResolver()
        {
            // TODO: Add Pages for navigation
            _vmResolvers.Add(MainViewModel.Page1ViewModelAlias, () => new Page1ViewModel());
            _vmResolvers.Add(MainViewModel.NotFoundPageViewModelAlias, () => new Page404ViewModel());
            _vmResolvers.Add(MainViewModel.PageOrdersViewModelAlias, () => new PageOrdersViewModel());
            _vmResolvers.Add(MainViewModel.PageProductCategoriesViewModelAlias, () => new PageProductCategoriesViewModel());
            _vmResolvers.Add(MainViewModel.PageProductsViewModelAlias, () => new PageProductsViewModel());
            _vmResolvers.Add(MainViewModel.PageSuppliersViewModelAlias, () => new PageSuppliersViewModel());
            _vmResolvers.Add(MainViewModel.PageUsersViewModelAlias, () => new PageUsersViewModel());
        }

        public INotifyPropertyChanged GetViewModelInstance(string alias)
        {
            if (_vmResolvers.ContainsKey(alias))
            {
                return _vmResolvers[alias]();
            }

            return _vmResolvers[MainViewModel.NotFoundPageViewModelAlias]();
        }
    }
}
