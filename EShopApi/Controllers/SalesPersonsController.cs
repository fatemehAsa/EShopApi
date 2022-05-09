using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EShopApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EShopApi.Models;

namespace EShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPersonsController : ControllerBase
    {
        private ISalesPersonsRepository _salesPersonsRepository;

        public SalesPersonsController(ISalesPersonsRepository salesPersonsRepository)
        {
            _salesPersonsRepository = salesPersonsRepository;
        }


        // GET: api/SalesPersons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesPerson>>> GetSalesPersons()
        {
            var result = new ObjectResult(_salesPersonsRepository.GetAllSalesPerson())
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            Request.HttpContext.Response.Headers.Add("X-Api", "Fatemeh");
            return result;
        }

        // GET: api/SalesPersons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesPerson>> GetSalesPerson(int id)
        {
            var salesPerson = await _salesPersonsRepository.Find(id);

            if (salesPerson == null)
            {
                return NotFound();
            }

            return salesPerson;
        }

        // PUT: api/SalesPersons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesPerson(int id, SalesPerson salesPerson)
        {
            if (id != salesPerson.SalesPersonId)
            {
                return BadRequest();
            }

            await _salesPersonsRepository.Update(salesPerson);

            return NoContent();
        }

        // POST: api/SalesPersons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalesPerson>> PostSalesPerson(SalesPerson salesPerson)
        {
            await _salesPersonsRepository.Add(salesPerson);

            return CreatedAtAction("GetSalesPerson", new { id = salesPerson.SalesPersonId }, salesPerson);
        }

        // DELETE: api/SalesPersons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesPerson(int id)
        {
            var salesPerson = await _salesPersonsRepository.Find(id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            await _salesPersonsRepository.Delete(id);

            return NoContent();
        }

        private async Task<bool> SalesPersonExists(int id)
        {
            return await _salesPersonsRepository.IsExistSalesPerson(id);
        }
    }
}
