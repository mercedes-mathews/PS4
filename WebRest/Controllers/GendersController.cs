﻿using System;
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
    public class GendersController : ControllerBase, iController<Gender>
    {
        private readonly WebRestOracleContext _context;

        public GendersController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/Genders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender>>> Get()
        {
            return await _context.Genders.ToListAsync();
        }

        // GET: api/Genders/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Gender>> Get(string id)
        {
            var _item = await _context.Genders.FindAsync(id);

            if (_item == null)
            {
                return NotFound();
            }

            return _item;
        }

        // PUT: api/Genders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Gender _item)
        {
            if (id != _item.GenderId)
            {
                return BadRequest();
            }
            _context.Genders.Update(_item);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(id))
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

        // POST: api/Genders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Gender>> Post(Gender _item)
        {
            _context.Genders.Add(_item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGender", new { id = _item.GenderId }, _item);
        }

        // DELETE: api/Genders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var _item = await _context.Genders.FindAsync(id);
            if (_item == null)
            {
                return NotFound();
            }

            _context.Genders.Remove(_item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenderExists(string id)
        {
            return _context.Genders.Any(e => e.GenderId == id);
        }
    }
}