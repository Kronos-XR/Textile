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
        public static void GetdataFromApi()
        {
            string apiUrl = "http://www.diplomapi.somee.com/api/products";

            WebRequest request = WebRequest.Create(apiUrl);

            try
            {
                using(WebResponse responce = request.GetResponse())
                using(Stream dataStream = responce.GetResponseStream())
                using(StreamReader reader = new StreamReader(dataStream))
                {
                    string responseFromServer = reader.ReadToEnd();
                    Console.WriteLine(responseFromServer);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка");
                throw;
            }
        }
        

        /*
        private readonly HttpClient _httpClient;

        public  ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://www.diplomapi.somee.com/api/products");
        }

        public async Task<List<Product>> GetDataAsync()
        {
            var responce = await _httpClient.GetAsync("endpoint");
            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var data = JsonConverter.DeserializeObject<List<Product>>(content);
                return data;
            }
            return null;
        }
        */
    }
}
