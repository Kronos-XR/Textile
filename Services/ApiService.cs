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
        private readonly HttpClient _httpClient;
        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri("http://www.diplomapi.somee.com");
        }

        private async Task<List<Product>> GetProductsInApiAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(responseBody);

            return products;
        }

        private async Task<Product> GetProductByIdInApiAsync(int productId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/products/{productId}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            Product product = JsonConvert.DeserializeObject<Product>(responseBody);

            return product;
        }
        private async Task<int> PostProductInApiAsync(Product product)
        {
            string productJson = JsonConvert.SerializeObject(product);
            StringContent content = new StringContent(productJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("/api/products", content);
            response.EnsureSuccessStatusCode();

            string createdProductJson = await response.Content.ReadAsStringAsync();
            Product createdProduct = JsonConvert.DeserializeObject<Product>(createdProductJson);

            return createdProduct.Id;
        }

        private async Task<HttpResponseMessage> PutProductInApiAsync(Product product)
        {
            string productJson = JsonConvert.SerializeObject(product);
            StringContent content = new StringContent(productJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"/api/products/{product.Id}", content);
            return response.EnsureSuccessStatusCode();
        }

        private async Task<HttpResponseMessage> DeleteProductInApiAsync(int productId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/products/{productId}");
            return response.EnsureSuccessStatusCode();
        }

        public async Task GetProductAsync(int productId)
        {
            Product product = await GetProductByIdInApiAsync(productId);
        }
        public async Task CreateProductAsync(Product product)
        {
            await PostProductInApiAsync(product);
        }
        public async Task UpdateProductAsync(Product product)
        {
            await PutProductInApiAsync(product);
        }
        public async Task DeleteProductAsync(int productId)
        {
            await DeleteProductInApiAsync(productId);
        }
    }
}
