using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly SmarterCityDBContext _context;

        public CustomersController(SmarterCityDBContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]        
        public async Task<JsonResult> GetCustomers()
        {
            return (new JsonResult(await _context.Customers.ToListAsync()));            
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]        
        public async Task<JsonResult> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {                
                return new JsonResult("Not Found");
            }
            
            return new JsonResult(customer);
        }

        // PUT: api/Customers
        [HttpPut]
        public async Task<JsonResult> PutCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {                
                if (!CustomerExists(customer.Id))
                {                    
                    return new JsonResult("Not Found");
                }
                else
                {
                    throw;
                }
            }
            
            return new JsonResult("Update Successfully");
        }

        // POST: api/Customers        
        [HttpPost]        
        public async Task<JsonResult> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new JsonResult("Add Customer Successfully");
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]        
        public async Task<JsonResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {                
                return new JsonResult("Not Found");
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            
            return new JsonResult("Delete Successfully");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
