using DiplomBackend.Models;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Textile.Services
{
    internal class ApiService
    {
        public static async void GetDataProductsFromApi()
        {
            int productId = 1;
            //string apiUrl = "http://www.diplomapi.somee.com/api/products";
            string apiUrl = $"http://www.diplomapi.somee.com/api/products/{productId}";


            using (HttpClient client = new HttpClient())
            {

                try
                {
                    // Выполняем GET-запрос
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Проверяем успешность запроса
                    if (response.IsSuccessStatusCode)
                    {
                        // Читаем содержимое ответа
                        string responseData = await response.Content.ReadAsStringAsync();

                        // Обрабатываем данные
                        Console.WriteLine(responseData);
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
            }           
        }
    }
}
