using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KlantenService_Steam_Framework.Areas.Identity.Data;
using KlantenService_Steam_Framework.Models;

namespace KlantenService_Steam_Framework.Controllers
{
    public class ProblemTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProblemTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProblemTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProblemType.ToListAsync());
        }

        // GET: ProblemTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemType = await _context.ProblemType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problemType == null)
            {
                return NotFound();
            }

            return View(problemType);
        }

        // GET: ProblemTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProblemTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName")] ProblemType problemType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(problemType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(problemType);
        }

        // GET: ProblemTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemType = await _context.ProblemType.FindAsync(id);
            if (problemType == null)
            {
                return NotFound();
            }
            return View(problemType);
        }

        // POST: ProblemTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName")] ProblemType problemType)
        {
            if (id != problemType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problemType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemTypeExists(problemType.Id))
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
            return View(problemType);
        }

        // GET: ProblemTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemType = await _context.ProblemType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problemType == null)
            {
                return NotFound();
            }

            return View(problemType);
        }

        // POST: ProblemTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problemType = await _context.ProblemType.FindAsync(id);
            if (problemType != null)
            {
                _context.ProblemType.Remove(problemType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemTypeExists(int id)
        {
            return _context.ProblemType.Any(e => e.Id == id);
        }
    }
}
