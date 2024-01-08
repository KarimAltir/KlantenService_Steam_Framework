using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KlantenService_Steam_Framework.Areas.Identity.Data;
using KlantenService_Steam_Framework.Models;
using KlantenService_Steam_Framework.Data;

namespace KlantenService_Steam_Framework.Controllers
{
    public class ParametersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParametersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parameters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parameters.ToListAsync());
        }

        // GET: Parameters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameter = await _context.Parameters
                .FirstOrDefaultAsync(m => m.Name == id);
            if (parameter == null)
            {
                return NotFound();
            }

            return View(parameter);
        }

        // GET: Parameters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parameters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Value,Description,UserId,LastChanged,Obsolete,Destination")] Parameter parameter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parameter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parameter);
        }

        // GET: Parameters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameter = await _context.Parameters.FindAsync(id);
            if (parameter == null)
            {
                return NotFound();
            }
            return View(parameter);
        }

        // POST: Parameters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Value,Description,UserId,LastChanged,Obsolete,Destination")] Parameter parameter)
        {
            if (id != parameter.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    parameter.UserId = _context.Users.First(u => u.Id == User.Identity.Name).Id;
                    parameter.LastChanged = DateTime.Now;
                    _context.Update(parameter);
                    await _context.SaveChangesAsync();

                    Globals.Parameters[parameter.Name] = parameter;
                    if (parameter.Destination == "Mail")
                    {
                        Globals.ConfigureMail();
                    }
                    await _context.SaveChangesAsync();
                }
                

                catch (DbUpdateConcurrencyException)
                {
                    if (!ParameterExists(parameter.Name))
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
            return View(parameter);
        }

        // GET: Parameters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameter = await _context.Parameters
                .FirstOrDefaultAsync(m => m.Name == id);
            if (parameter == null)
            {
                return NotFound();
            }

            return View(parameter);
        }

        // POST: Parameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var parameter = await _context.Parameters.FindAsync(id);
            if (parameter != null)
            {
                _context.Parameters.Remove(parameter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParameterExists(string id)
        {
            return _context.Parameters.Any(e => e.Name == id);
        }
    }
}
