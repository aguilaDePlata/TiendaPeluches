﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peluches.Web.API.Models;

namespace Peluches.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly C3BdPedidosContext _context;

        public MarcasController(C3BdPedidosContext context)
        {
            _context = context;
        }

        //GET: api/Marca
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas()
        {
            return await _context.Marcas.ToListAsync();
        }

        //GET: api/Marcas/5
        [HttpGet("{id}")]

        public async Task<ActionResult<Marca>> GetMarca(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null) 
            {
                return NotFound();
            }

            return marca;
        }

        //PUT: api/Marcas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarca(int id, Marca marca)
        {
            if (id != marca.IdMarca) 
            {
                return BadRequest();
            }

            _context.Entry(marca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!MarcasExists(id))
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

        //POST: api/Marcas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Marca>> PostMarca(Marca marca)
        {
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarca", new { id = marca.IdMarca }, marca);
        }

        // DELETE: api/Marcas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarca(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }

            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarcasExists(int id)
        {
            return _context.Marcas.Any(e => e.IdMarca == id);
        }
    }
}
