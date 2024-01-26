using KlantenService_Steam_Framework.Data;
using KlantenService_Steam_Framework.Services;
using Microsoft.EntityFrameworkCore;

namespace GroupSpace23.Services
{
    public class GameListMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GameService _gameService;
        private readonly ApplicationDbContext _context;

        public GameListMiddleware(RequestDelegate next, GameService gameService, ApplicationDbContext context)
        {
            _next = next;
            _gameService = gameService;
            _context = context;
        }
        
        public async Task Invoke(HttpContext context)
        {
            // Haal de lijst van spellen op vanuit de externe API
            var games = await _gameService.GetGamesAsync();

            // Sla de lijst van spellen op in de database
            await _context.UpdateGamesAsync(games);

            // Ga door naar het volgende middelware-component in de pipeline
            await _next(context);
        }
    }
}
