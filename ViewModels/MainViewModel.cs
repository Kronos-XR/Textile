using System.ComponentModel;
using System.Windows.Input;
using Textile.ViewModels.Interfaces;
using Textile.Views;

namespace Textile.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Constants

        public static readonly string Page1ViewModelAlias = "Page1VM";
        public static readonly string NotFoundPageViewModelAlias = "404VM";
        public static readonly string PageOrdersViewModelAlias = "PageOrdersVM";
        public static readonly string PageProductCategoriesViewModelAlias = "PageProductCategoriesVM";
        public static readonly string PageProductsViewModelAlias = "PageProductsVM";
        public static readonly string PageSuppliersViewModelAlias = "PageSuppliersVM";
        public static readonly string PageUsersViewModelAlias = "PageUsersVM";

        #endregion

        #region Fields

        private readonly IViewModelsResolver _resolver;

        private ICommand _goToPathCommand;
        private ICommand _goToPage1Command;
        private ICommand _goToPageOrdersCommand;
        private ICommand _goToPageProductCategoriesCommand;
        private ICommand _goToPageProductsCommand;
        private ICommand _goToPageSuppliersCommand;
        private ICommand _goToPageUsersCommand;

        private readonly INotifyPropertyChanged _p1ViewModel;
        private readonly INotifyPropertyChanged _pOrdersViewModel;
        private readonly INotifyPropertyChanged _pProductCategoriesViewModel;
        private readonly INotifyPropertyChanged _pProductsViewModel;
        private readonly INotifyPropertyChanged _pSuppliersViewModel;
        private readonly INotifyPropertyChanged _pUsersViewModel;
        
        #endregion


        #region Properties

        public ICommand GoToPathCommand
        {
            get { return _goToPathCommand; }
            set
            {
                _goToPathCommand = value;
                RaisePropertyChanged("GoToPathCommand");
            }
        }

        public ICommand GoToPage1Command
        {
            get
            {
                return _goToPage1Command;
            }
            set
            {
                _goToPage1Command = value;
                RaisePropertyChanged("GoToPage1Command");
            }
        }

        public ICommand GoToPageOrdersCommand
        {
            get { return _goToPageOrdersCommand; }
            set
            {
                _goToPageOrdersCommand = value;
                RaisePropertyChanged("GoToPageOrdersCommand");
            }
        }

        public ICommand GoToPageProductCategoriesCommand
        {
            get { return _goToPageProductCategoriesCommand; }
            set
            {
                _goToPageProductCategoriesCommand = value;
                RaisePropertyChanged("GoToPageProductCategoriesCommand");
            }
        }
        public ICommand GoToPageSuppliersCommand
        {
            get { return _goToPageSuppliersCommand; }
            set
            {
                _goToPageSuppliersCommand = value;
                RaisePropertyChanged("GoToPageSuppliersCommand");
            }
        }
        public ICommand GoToPageProductsCommand
        {
            get { return _goToPageProductsCommand; }
            set
            {
                _goToPageProductsCommand = value;
                RaisePropertyChanged("GoToPageProductsCommand");
            }
        }
        public ICommand GoToPageUsersCommand
        {
            get { return _goToPageUsersCommand; }
            set
            {
                _goToPageUsersCommand = value;
                RaisePropertyChanged("GoToPageUsersCommand");
            }
        }

        public INotifyPropertyChanged Page1ViewModel => _p1ViewModel;
        public INotifyPropertyChanged PageOrdersViewModel => _pOrdersViewModel;
        public INotifyPropertyChanged PageProductCategoriesViewModel => _pProductCategoriesViewModel;
        public INotifyPropertyChanged PageProductsViewModel => _pProductsViewModel;
        public INotifyPropertyChanged PageSuppliersViewModel => _pSuppliersViewModel;
        public INotifyPropertyChanged PageUsersViewModel => _pUsersViewModel;

        #endregion


        #region Constructors

        public MainViewModel(IViewModelsResolver resolver)
        {
            _resolver = resolver;

            _p1ViewModel = _resolver.GetViewModelInstance(Page1ViewModelAlias);
            _pOrdersViewModel = _resolver.GetViewModelInstance(PageOrdersViewModelAlias);
            _pProductCategoriesViewModel = _resolver.GetViewModelInstance(PageProductCategoriesViewModelAlias);
            _pProductsViewModel = _resolver.GetViewModelInstance(PageProductsViewModelAlias);
            _pSuppliersViewModel = _resolver.GetViewModelInstance(PageSuppliersViewModelAlias);
            _pUsersViewModel = _resolver.GetViewModelInstance(PageUsersViewModelAlias);

            InitializeCommands();
        }

        #endregion

        private void InitializeCommands()
        {

            GoToPathCommand = new RelayCommand<string>(GoToPathCommandExecute);

            GoToPage1Command = new RelayCommand<INotifyPropertyChanged>(GoToPage1CommandExecute);

            GoToPageOrdersCommand = new RelayCommand<INotifyPropertyChanged>(GoToPageOrdersCommandExecute);

            GoToPageProductCategoriesCommand = new RelayCommand<INotifyPropertyChanged>(GoToPageProductCategoriesCommandExecute);

            GoToPageProductsCommand = new RelayCommand<INotifyPropertyChanged>(GoToPageProductsCommandExecute);

            GoToPageSuppliersCommand = new RelayCommand<INotifyPropertyChanged>(GoToPageSuppliersCommandExecute);

            GoToPageUsersCommand = new RelayCommand<INotifyPropertyChanged>(GoToPageUsersCommandExecute);
        }

        private void GoToPathCommandExecute(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                return;
            }

            Navigation.Navigate(alias);
        }

        private void GoToPage1CommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigate(Navigation.Page1Alias, Page1ViewModel);
        }

        private void GoToPageOrdersCommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigate(Navigation.PageOrdersAlias, PageOrdersViewModel);
        }

        private void GoToPageProductCategoriesCommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigate(Navigation.PageProductCategoriesAlias, PageProductCategoriesViewModel);
        }
        private void GoToPageProductsCommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigate(Navigation.PageProductsAlias, PageProductsViewModel);
        }
        private void GoToPageSuppliersCommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigate(Navigation.PageSuppliersAlias, PageSuppliersViewModel);
        }
        private void GoToPageUsersCommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigate(Navigation.PageUsersAlias, PageUsersViewModel);
        }
    }
}
