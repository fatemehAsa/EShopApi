using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EShopApi.Interfaces;
using EShopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            var result = new ObjectResult(_customerRepository.GetAllCustomer())
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            Request.HttpContext.Response.Headers.Add("X-Count", _customerRepository.CustomerCount().ToString());
            Request.HttpContext.Response.Headers.Add("X-Name", "Iman Madaeny");

            return result;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (await CheckExistCustomer(id))
            {

                var customer = await _customerRepository.Find(id);
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }
        }

        private async Task<bool> CheckExistCustomer(int id)
        {
            return  await _customerRepository.IsExistCustomer(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _customerRepository.Add(customer);

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            var person = await _customerRepository.Find(id);
            person.Email = "Sara@Yahoo.com";
           await _customerRepository.Update(customer);
            return Ok(person);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {

            await _customerRepository.Remove(id);
            return Ok();
        }
    }
}
