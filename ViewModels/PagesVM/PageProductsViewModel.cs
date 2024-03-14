using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Textile.Models;
using Textile.Services;

namespace Textile.ViewModels
{
    public class PageProductsViewModel : BaseViewModel
    {
        private int _productId;
        private Product _product;
        public PageProductsViewModel()
        {
            _product = new Product();
        }

        public int ProductId
        {
            get { return _productId; }
            set
            {
                if (_productId != value)
                {
                    _productId = value;
                    RaisePropertyChanged(nameof(ProductId));
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
            }
        }

        public async Task LoadDataTask()
        {
            ApiService apiService = new ApiService();
            Product = await apiService.GetProductAsync(ProductId);
        }

        private bool CanUpdateData(object parameter)
        {
            // Возвращаем true, если условие для выполнения обновления данных удовлетворено
            return true;
        }

        private async Task UpdateDataTask()
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
        private async Task CreateProductTask()
        {

            ApiService apiService = new ApiService();
            await apiService.CreateProductAsync(Product);
        }
        private async Task DeleteProductTask()
        {
            ApiService apiService = new ApiService();
            await apiService.DeleteProductAsync(ProductId);
        }

        public RelayCommand GetDataCommand => new RelayCommand(async (param) => await LoadDataTask());
        public RelayCommand UpdateDataCommand => new RelayCommand(async (param) => await UpdateDataTask(), CanUpdateData);
        public RelayCommand CreateDataCommand => new RelayCommand(async (param) => await CreateProductTask());
        public RelayCommand DeleteDataCommand => new RelayCommand(async (param) => await DeleteProductTask());
    }
}
