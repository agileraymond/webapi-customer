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
    public class AddressController : Controller
    {
        private readonly IBusinessController _businessController;

        public AddressController(IBusinessController businessController)
        {
            _businessController = businessController;
        }        
         
        [HttpGet("{id}", Name = "GetAddress")]
        public IActionResult Get(int id)
        {
            var address = _businessController.GetAddress(id);
            if (address == null)
            {
                return NotFound();
            }

            return new ObjectResult(address);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Address address)
        {
            var addressId = _businessController.AddAddress(address);
            return CreatedAtRoute("GetAddress", new { id = addressId }, address);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Address address)
        {
            _businessController.UpdateAddress(address);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _businessController.DeleteAddress(id);
            return new NoContentResult();
        }
    }
}
