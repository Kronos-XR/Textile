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
        #region GetProduct
        public static async Task<Product> GetDataProductFromApiAsync(int productId)
        {
            string apiUrl = $"http://www.diplomapi.somee.com/api/products/{productId}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        Product product = JsonConvert.DeserializeObject<Product>(responseData);
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
        }

        public static async Task GetProductAsync(int productId = 1)
        {
            Product product = await GetDataProductFromApiAsync(productId);
        }
        #endregion

        #region Put
        public static async Task<Product> UpdateDataProductToApiAsync(int productId, Product updatedProduct)
        {
            string apiUrl = $"http://www.diplomapi.somee.com/api/products/{productId}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Сериализация объекта updatedProduct в JSON и отправка в теле запроса
                    string jsonBody = JsonConvert.SerializeObject(updatedProduct);
                    HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Выполнение PUT запроса
                    HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        Product product = JsonConvert.DeserializeObject<Product>(responseData);
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
        }

        public static async Task UpdateProductAsync(int productId, Product updatedProduct)
        {
            Product product = await UpdateDataProductToApiAsync(productId, updatedProduct);
        }
        #endregion
    }
}
