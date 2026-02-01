using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CrudEF.Data;
using CrudEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using SQLitePCL;

namespace CrudEF.Controllers
{
    public class ContactosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET Contactos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contactos = await _context.Contactos.ToListAsync();
            return View(contactos);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                // Si es valido se guarda en la base de datos
                _context.Add(contacto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos.FindAsync(id);

            if (contacto is null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Contacto contacto)
        {
            if (id != contacto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoExist(contacto.Id))
                    {
                        return NotFound();
                    } else
                    {
                        throw;
                    }
                    
                }
            }

            return View(contacto);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos.FirstOrDefaultAsync(m => m.Id == id);

            if (contacto is null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpGet]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos.FirstOrDefaultAsync(m => m.Id == id);

            if (contacto is null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrar(int id)
        {

            var contacto = await _context.Contactos.FirstOrDefaultAsync(m => m.Id == id);

            if (contacto is null)
            {
                return NotFound();
            }

            _context.Remove(contacto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactoExist(int id)
        {
            return _context.Contactos.Any(e => e.Id == id);
        }
    }
}