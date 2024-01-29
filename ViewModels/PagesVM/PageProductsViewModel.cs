using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Textile.Models;
using Textile.Services;

namespace Textile.ViewModels
{
    public class PageProductsViewModel : BaseViewModel
    {
        private int _productId;
        private Product _product;

        public int ProductId
        {
            get { return _productId; }
            set
            {
                if (_productId != value)
                {
                    _productId = value;
                    RaisePropertyChanged(nameof(ProductId));
                    LoadDataAsync();
                }
            }
        }

        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                RaisePropertyChanged(nameof(Product));
                UpdateDataAsync();
            }
        }

        public async Task LoadDataAsync()
        {
            ApiService apiService = new ApiService();
            Product = await apiService.GetProductAsync(ProductId);
            
        }

        public ICommand UpdateDataCommand => new RelayCommand<object>(async (param) => await UpdateDataAsync(), CanUpdateData);

        private bool CanUpdateData(object parameter)
        {
            // Возвращаем true, если условие для выполнения обновления данных удовлетворено
            return true;
        }

        private async Task UpdateDataAsync() // поменять название
        {
            ApiService apiService = new ApiService();
            HttpResponseMessage updatedProduct = await apiService.UpdateProductAsync(Product);

            if (updatedProduct != null)
            {
                Console.WriteLine("Данные успешно изменены.");
            }
            else
            {
                Console.WriteLine("Ошибка не изменены.");
            }
        }
    }
}
