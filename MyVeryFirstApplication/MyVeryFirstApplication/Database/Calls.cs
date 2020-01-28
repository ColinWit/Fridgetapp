using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyVeryFirstApplication.Database
{
    class Calls
    {
        HttpClient _client;
        public Calls()
        {
            _client = new HttpClient();
        }
        public async Task<ObservableCollection<Product>> RefreshDataAsync()
        {
            try
            {
                ObservableCollection<Product> products = null;
                var uri = new Uri("https://fridget.chprojecten.nl/api/411/products");
                HttpClient myClient = myClient = new HttpClient();

                var response = await myClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(content);
                    Console.WriteLine("");
                }
                return products;
            }
            catch(Exception e)
            {
                return new ObservableCollection<Product>();
            }
        }

        public async Task<bool> Deleteitem(int id)
        {
            var uri = "https://fridget.chprojecten.nl/api/remove/product/" + id;
            var response = await _client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
    
}
