using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Peluches.BackOffice.Presentacion.Models;

namespace Peluches.BackOffice.Presentacion.Controllers
{
    public class ComprobantesController : Controller
    {
        private readonly C3_BD_PEDIDOSContext _context;

        public ComprobantesController(C3_BD_PEDIDOSContext context)
        {
            _context = context;
        }

        // GET: Comprobantes
        public async Task<IActionResult> Index()
        {
            var c3_BD_PEDIDOSContext = _context.Comprobantes.Include(c => c.IdPedidoNavigation);
            return View(await c3_BD_PEDIDOSContext.ToListAsync());
        }

        // GET: Comprobantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comprobante = await _context.Comprobantes
                .Include(c => c.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.IdComprobante == id);
            if (comprobante == null)
            {
                return NotFound();
            }

            return View(comprobante);
        }

        // GET: Comprobantes/Create
        public IActionResult Create()
        {
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido");
            return View();
        }

        // POST: Comprobantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdComprobante,IdPedido,TipoComprobante,FechaEmision,SubtotalComp,Descuento,Igv,ValorTotal")] Comprobante comprobante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comprobante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", comprobante.IdPedido);
            return View(comprobante);
        }

        // GET: Comprobantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comprobante = await _context.Comprobantes.FindAsync(id);
            if (comprobante == null)
            {
                return NotFound();
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", comprobante.IdPedido);
            return View(comprobante);
        }

        // POST: Comprobantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdComprobante,IdPedido,TipoComprobante,FechaEmision,SubtotalComp,Descuento,Igv,ValorTotal")] Comprobante comprobante)
        {
            if (id != comprobante.IdComprobante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comprobante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComprobanteExists(comprobante.IdComprobante))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", comprobante.IdPedido);
            return View(comprobante);
        }

        // GET: Comprobantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comprobante = await _context.Comprobantes
                .Include(c => c.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.IdComprobante == id);
            if (comprobante == null)
            {
                return NotFound();
            }

            return View(comprobante);
        }

        // POST: Comprobantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comprobante = await _context.Comprobantes.FindAsync(id);
            _context.Comprobantes.Remove(comprobante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComprobanteExists(int id)
        {
            return _context.Comprobantes.Any(e => e.IdComprobante == id);
        }
    }
}
