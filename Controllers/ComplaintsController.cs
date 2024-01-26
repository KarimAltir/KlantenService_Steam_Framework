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
using KlantenService_Steam_Framework.Services;
using Microsoft.AspNetCore.Mvc.Localization;

namespace KlantenService_Steam_Framework.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly KlantenServiceUser _myUser;
        private readonly IHtmlLocalizer<ComplaintsController> _localizer;


        public ComplaintsController(ApplicationDbContext context, IMyUser myUser, IHtmlLocalizer<ComplaintsController> localizer)
        {
            _context = context;
            _myUser = myUser.User();
            _localizer = localizer;

        }

        // GET: Complaints
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Complaints.Include(c => c.Game).Include(c => c.ProblemType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Complaints/Details/5
        public async Task<IActionResult> Details(int? id)
        {   
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.Game)
                .Include(c => c.ProblemType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // GET: Complaints/Create
        public IActionResult Create()
        {
            // Haal de lijst met games op uit de database
            var games = _context.Games.Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = g.Id.ToString()
            }).ToList();

            // Voeg een "Selecteer" optie toe aan de lijst
            games.Insert(0, new SelectListItem { Text = "", Value = "" });

            // Geef de lijst met games door aan de ViewBag
            ViewBag.Games = games;

            // Haal de lijst met probleemtypes op uit de database
            var problemTypes = _context.ProblemTypes.Select(pt => new SelectListItem
            {
                Text = pt.TypeName,
                Value = pt.Id.ToString()
            }).ToList();

            // Voeg een "Selecteer" optie toe aan de lijst
            problemTypes.Insert(0, new SelectListItem { Text = "", Value = "" });

            // Geef de lijst met probleemtypes door aan de ViewBag
            ViewBag.ProblemTypes = problemTypes;

            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id");
            ViewData["ProblemTypeId"] = new SelectList(_context.ProblemTypes, "Id", "Id");

            return View();
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,GameId,ProblemTypeId,Email,Status")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Haal de lijst met games op uit de database
            var games = _context.Games.Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = g.Id.ToString()
            }).ToList();

            // Voeg een "Selecteer" optie toe aan de lijst
            games.Insert(0, new SelectListItem { Text = "", Value = "" });

            // Geef de lijst met games door aan de ViewBag
            ViewBag.Games = games;

            // Haal de lijst met probleemtypes op uit de database
            var problemTypes = _context.ProblemTypes.Select(pt => new SelectListItem
            {
                Text = pt.TypeName,
                Value = pt.Id.ToString()
            }).ToList();

            // Voeg een "Selecteer" optie toe aan de lijst
            problemTypes.Insert(0, new SelectListItem { Text = "", Value = "" });

            // Geef de lijst met probleemtypes door aan de ViewBag
            ViewBag.ProblemTypes = problemTypes;

            // Vertaal de ModelState-fouten
            var modelStateErrors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => _localizer[e.ErrorMessage].Value);

            // Voeg vertaalde modelstate fouten toe aan ViewData zodat deze kunnen worden weergegeven op de weergavepagina
            ViewData["ModelStateErrors"] = modelStateErrors;

            // Als er iets misgaat, render dan gewoon het formulier opnieuw met de bestaande gegevens
            return View(complaint);
        }


        // GET: Complaints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", complaint.GameId);
            ViewData["ProblemTypeId"] = new SelectList(_context.ProblemTypes, "Id", "Id", complaint.ProblemTypeId);
            //ViewData["ProblemTypeName"] = new SelectList(_context.ProblemTypes, "TypeName", "TypeName", complaint.ProblemTypeName);
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,GameId,ProblemTypeId/*,ProblemTypeName*/,Email,Status")] Complaint complaint)
        {
            if (id != complaint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.Id))
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
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", complaint.GameId);
            ViewData["ProblemTypeId"] = new SelectList(_context.ProblemTypes, "Id", "Id", complaint.ProblemTypeId);
            //ViewData["ProblemTypeName"] = new SelectList(_context.ProblemTypes, "TypeName", "TypeName", complaint.ProblemTypeName);
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.Game)
                .Include(c => c.ProblemType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaints.Any(e => e.Id == id);
        }
    }
}
