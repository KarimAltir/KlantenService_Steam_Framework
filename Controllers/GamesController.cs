using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KlantenService_Steam_Framework.Areas.Identity.Data;
using KlantenService_Steam_Framework.Models;
using KlantenService_Steam_Framework.Services;
using KlantenService_Steam_Framework.Data;

namespace KlantenService_Steam_Framework.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GameService _gameService;
        //private static Boolean hasbeenUpdated = false;


        public GamesController(ApplicationDbContext context)
        {
            _context = context;
            
            //maak een HttpClient aan
            var httpClient = new HttpClient();

            //maak een instantie van GameService aan en geef de HttpClient door
            _gameService = new GameService(httpClient);
        }

        //// GET: Games
        //public async Task<IActionResult> Index()
        //{
        //    //return View(await _context.Game.ToListAsync());


        //    // Roep de nieuwe UpdateGamesAsync-methode aan
        //    if (!hasbeenUpdated)
        //    {

        //        // Haal de games op vanuit de API
        //        var games = await _gameService.GetGamesAsync();
        //        List<Game> gamesList = games.ToList();

        //        _context.UpdateGames(games);
        //        hasbeenUpdated = true;
        //    }
        //    return View(_context.Games);
        //}

        // GET: Games
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Game.ToListAsync());

            var games = await _gameService.GetGamesAsync();
            return View(games);
        }



        //// GET: Games/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var game = await _context.Games
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (game == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(game);
        //}

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var game = await _context.Games
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var games = await _gameService.GetGamesAsync();
            Game game = games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/GetGames
        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            var games = await _gameService.GetGamesAsync();
            //var games = await _context.Game.ToListAsync();

            return Json(games);
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
