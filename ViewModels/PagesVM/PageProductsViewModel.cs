using System;
using System.Collections.Generic;
using System.Linq;
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
            Product = await ApiService.GetDataProductFromApiAsync(ProductId);
            
        }

        public ICommand UpdateDataCommand => new RelayCommand<object>(async (param) => await UpdateDataAsync(), CanUpdateData);

        private bool CanUpdateData(object parameter)
        {
            // Возвращаем true, если условие для выполнения обновления данных удовлетворено
            return true;
        }

        private async Task UpdateDataAsync()
        {
            // Вызываем метод для выполнения PUT-запроса с обновленными данными
            Product updatedProduct = await ApiService.UpdateDataProductToApiAsync(ProductId, Product);

            if (updatedProduct != null)
            {
                // Обновляем данные после успешного PUT-запроса
                Product = updatedProduct;
                // Может быть добавлена дополнительная логика после обновления данных
                Console.WriteLine("Данные успешно обновлены.");
            }
        }
    }
}
