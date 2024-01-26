using KlantenService_Steam_Framework.Models;
using Newtonsoft.Json;



namespace KlantenService_Steam_Framework.Services
{
    public class GameService
    {
        private readonly HttpClient _httpClient;

        public GameService(HttpClient httpClient)
        {
            _httpClient = new HttpClient();

            var ApiKey = "339ba9da43504cb3a25242b6ede8468c";
            var StartDate = "2003-09-01";
            var EndDate = "2019-09-30";
            var Platforms = "18,1,7";
            var PageSize = "10000";
            var pageNumber = "1";


            // Vorm de URL met de API-sleutel
            var requestUri = "https://api.rawg.io/api/games?key=" + ApiKey + "&dates=" + StartDate + "," + EndDate +"&platforms="+ Platforms + "&page_size=" + PageSize + "&page=" + pageNumber;

            // Stel de basis-URL in voor de HttpClient
            _httpClient.BaseAddress = new Uri(requestUri);
        }

        public async Task<List<Game>> GetGamesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress);

            if (response.IsSuccessStatusCode)
            {
                // De aanvraag is geslaagd, lees de response-inhoud
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);

                // Voer verdere verwerking uit met de ontvangen gegevens
                // Gebruik Newtonsoft.Json om de response-body om te zetten naar een lijst van games
                var gameResponse = JsonConvert.DeserializeObject<GameResponse>(responseBody);
                var games = gameResponse?.Results ?? new List<Game>();
                return games;
            }
            else
            {
                // De aanvraag is mislukt, verwerk de fout
                Console.WriteLine($"Fout: {response.StatusCode}");
                // Return een lege lijst in geval van fout, maar je kunt ook null of een foutafhandeling toevoegen
                return null;
            }
        }

    }

    public class GameResponse
    {
        public List<Game> Results { get; set; }
    }
}
