using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebClient.Models
{
    public class CustomerRepository
    {
        private string apiUrl = "http://localhost:38469/Api/Customer";
        private HttpClient _client;

        public CustomerRepository()
        {
            _client = new HttpClient();
        }
        public List<Customer> GetAllCustomer()
        {
            var result = _client.GetStringAsync(apiUrl).Result;
            List<Customer> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Customer>>(result);
            return list;
        }

        public Customer GetCustomerById(int customerId)
        {
            var result = _client.GetStringAsync(apiUrl + "/" + customerId).Result;
            var customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(result);
            return customer;
        }


        public void AddCustomer(Customer customer)
        {
            var jsonCustomer = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");
            var res = _client.PostAsync(apiUrl, content).Result;
        }
        public void UpdateCustomer(Customer customer)
        {
            var jsonCustomer = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");
            var res = _client.PutAsync(apiUrl, content).Result;

        }

        public void DeleteCustomer(int customerId)
        {
            var res = _client.DeleteAsync(apiUrl + "/" + customerId).Result;
        }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
