using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Textile.Services.Interfaces;
using Textile.Views;
using Textile.Views.Pages;

namespace Textile.Services
{
    public class PagesResolver : IPageResolver
    {

        private readonly Dictionary<string, Func<Page>> _pagesResolvers = new Dictionary<string, Func<Page>>();

        public PagesResolver()
        {
            // TODO: Add Pages For navigation
            _pagesResolvers.Add(Navigation.Page1Alias, () => new Page1());
            _pagesResolvers.Add(Navigation.NotFoundPageAlias, () => new Page404());
        }

        public Page GetPageInstance(string alias)
        {
            if (_pagesResolvers.ContainsKey(alias))
            {
                return _pagesResolvers[alias]();
            }

            return _pagesResolvers[Navigation.NotFoundPageAlias]();
        }
    }
}
