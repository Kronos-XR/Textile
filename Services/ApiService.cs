using Textile.Models;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace Textile.Services
{
    //TODO: Разделить адреса api от функционала, если бекенд переедет то нужно будет чинить каждую ссылку 
    internal class ApiService
    {
        #region HttpClient Configuration

        private static ApiService _instance;
        private readonly HttpClient _httpClient;
        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri("http://www.diplomapi.somee.com");
        }
        public static ApiService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApiService();
                }
                return _instance;
            }
        }
        #endregion

        #region GetProductsInApiAsync
        private async Task<List<Product>> GetProductsInApiAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/api/products");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<Product> products = JsonConvert.DeserializeObject<List<Product>>(responseBody);

                    return products;
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            return null;
        }
        #endregion

        #region GetProductByIdInApiAsync
        private async Task<Product> GetProductByIdInApiAsync(int productId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/products/{productId}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Product product = JsonConvert.DeserializeObject<Product>(responseBody);

                    return product;
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            return null;
        }
        #endregion

        #region PostProductInApiAsync
        private async Task<int> PostProductInApiAsync(Product product)
        {
            product.Id = 0;
            try
            {
                string productJson = JsonConvert.SerializeObject(product);
                StringContent content = new StringContent(productJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("/api/products", content);
                response.EnsureSuccessStatusCode();

                string createdProductJson = await response.Content.ReadAsStringAsync();
                Product createdProduct = JsonConvert.DeserializeObject<Product>(createdProductJson);

                return createdProduct.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            return 0;
        }
        #endregion

        #region PutProductInApiAsync
        private async Task<HttpResponseMessage> PutProductInApiAsync(Product product)
        {
            try
            {
                string productJson = JsonConvert.SerializeObject(product);
                StringContent content = new StringContent(productJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"/api/products/{product.Id}", content);
                return response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        #endregion

        #region DeleteProductInApiAsync
        private async Task<HttpResponseMessage> DeleteProductInApiAsync(int productId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/products/{productId}");
                return response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        #endregion 

        #region Tasks for page
        public async Task<Product> GetProductAsync(int productId)
        {
            return await GetProductByIdInApiAsync(productId);
        }
        public async Task CreateProductAsync(Product product)
        {
            await PostProductInApiAsync(product);
        }
        public async Task<HttpResponseMessage> UpdateProductAsync(Product product)
        {
            return await PutProductInApiAsync(product);
        }
        public async Task DeleteProductAsync(int productId)
        {
            await DeleteProductInApiAsync(productId);
        }
        #endregion
    }
}
