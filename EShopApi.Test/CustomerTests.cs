using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EShopApi.Test
{
    [TestClass]
    public class CustomerTests

    {
        HttpClient _client;

        public CustomerTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [TestMethod]
        public void GetAllTest()
        {
            var request=new HttpRequestMessage(new HttpMethod("Get"),"/Api/Customer" );
            var response = _client.SendAsync(request).Result.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK,response);
        }

        [TestMethod]
        [DataRow(3)]
        public void GetOneCustomer(int id)
        {
            var request=new HttpRequestMessage(new HttpMethod("Get"), $"/Api/Customer/{id}");
            var response = _client.SendAsync(request).Result;
            Assert.AreEqual(HttpStatusCode.OK,response.StatusCode);
        }

        [TestMethod]
        public void CustomerPostTest()
        {
            var request=new HttpRequestMessage(new HttpMethod("Post"),$"/Api/Customer" );
            var response = _client.SendAsync(request).Result;
            Assert.AreEqual(HttpStatusCode.UnsupportedMediaType,response.StatusCode);
        }
    }
}
