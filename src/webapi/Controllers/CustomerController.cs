using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Business;
using Data.Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IBusinessController _businessController;

        public CustomerController(IBusinessController businessController)
        {
            _businessController = businessController;
        }        

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = _businessController.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }

            return new ObjectResult(customer);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            _businessController.AddCustomer(customer);
            return CreatedAtRoute("GetCustomer", new { id = 1 }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer customer)
        {
            _businessController.UpdateCustomer(customer);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _businessController.DeleteCustomer(id);
            return new NoContentResult();
        }
    }
}
