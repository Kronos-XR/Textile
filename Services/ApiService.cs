using Textile.Models;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Textile.Services
{
    //TODO: сделать Асинхронным
    internal class ApiService
    {
        public static Product GetDataProductFromApi(int productId)
        {
            string apiUrl = $"http://www.diplomapi.somee.com/api/products/{productId}";

            using (HttpClient client = new HttpClient())
            {
            try
            {
                    // Выполняем GET-запрос
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result; // Блокируем выполнение до завершения асинхронной операции

                    // Проверяем успешность запроса
                    if (response.IsSuccessStatusCode)
                {
                        // Читаем содержимое ответа
                        string responseData = response.Content.ReadAsStringAsync().Result; // Блокируем выполнение до завершения асинхронной операции

                        // Десериализуем JSON в объект Product
                        Product product = JsonConvert.DeserializeObject<Product>(responseData);

                        // Возвращаем полученный продукт
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

                // В случае ошибки или неудачного запроса возвращаем null
                return null;
            }
        }
        
        public static void GetProduct(int productId = 1)
        {
            Product product = GetDataProductFromApi(productId);

            if (product != null)
            {
                // Теперь у вас есть объект Product, и вы можете использовать его как вам нужно
                Console.WriteLine($"Product ID: {product.Id}, Name: {product.ProductName}, Price: {product.Price}");
            }
            return null;
        }
    }
}
