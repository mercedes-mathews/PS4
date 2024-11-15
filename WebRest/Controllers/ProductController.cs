using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;
using WebRest.Interfaces;
namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase, iController<Product>
    {
        private readonly WebRestOracleContext _context;

        public ProductsController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var _item = await _context.Products.FindAsync(id);

            if (_item == null)
            {
                return NotFound();
            }

            return _item;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Product _item)
        {
            if (id != _item.ProductId)
            {
                return BadRequest();
            }
            _context.Products.Update(_item);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product _item)
        {
            _context.Products.Add(_item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = _item.ProductId }, _item);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var _item = await _context.Products.FindAsync(id);
            if (_item == null)
            {
                return NotFound();
            }

            _context.Products.Remove(_item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}