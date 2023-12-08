using KlantenService_Steam_Framework.Models;



namespace KlantenService_Steam_Framework.Services
{
    public class GameService
    {
        private readonly HttpClient _httpClient;

        public GameService(HttpClient httpClient)
        {
            _httpClient = new HttpClient();

            // Je API-sleutel
            var keyAPI = "339ba9da43504cb3a25242b6ede8468c";

            // Vorm de URL met de API-sleutel
            var requestUri = $"https://api.rawg.io/api/games?key={keyAPI}&dates=2019-09-01,2019-09-30&platforms=18,1,7";

            // Stel de basis-URL in voor de HttpClient
            _httpClient.BaseAddress = new Uri(requestUri);
        }

        public async Task GetGamesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress);

            if (response.IsSuccessStatusCode)
            {
                // De aanvraag is geslaagd, lees de response-inhoud
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                // Voer verdere verwerking uit met de ontvangen gegevens
            }
            else
            {
                // De aanvraag is mislukt, verwerk de fout
                Console.WriteLine($"Fout: {response.StatusCode}");
            }
        }
    }
}
