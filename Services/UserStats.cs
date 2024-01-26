using KlantenService_Steam_Framework.Areas.Identity.Data;
using KlantenService_Steam_Framework.Data;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace KlantenService_Steam_Framework.Services
{
    public class UserStats
    {
        readonly RequestDelegate _next; // Verwijsing naar de volgdende middleware in de afhalndeling van de request
        readonly IMyUser _myUser;
        struct Statistics 
        {
            public KlantenServiceUser User { get; set; }
            public DateTime Connected { get; set; }
            public int NumberOfRequests { get; set; }
            public DateTime LastConnected { get; set; }
        }

        static Dictionary<string, Statistics> DictStatistics;

        //public static int GetNumberOfRequests { get { return DicStatistics } }

        private Statistics stats;

        public UserStats(RequestDelegate next) 
        {
            DictStatistics = new Dictionary<string, Statistics>();
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IMyUser myUser)
        {
            try
            {
                Statistics stat = DictStatistics[myUser.User().UserName];
                stat.LastConnected = DateTime.Now;
                stat.NumberOfRequests++;
            }
            catch
            {
                Statistics stat = new Statistics();
                stat.User = myUser.User();
                stat.Connected = DateTime.Now;
                stat.NumberOfRequests = 1;
                stat.LastConnected = DateTime.Now;
                DictStatistics[myUser.User().UserName] = stats;
            }
            

            await _next(httpContext);
        }

        public static int GetCountofRequest(string name)
        {
            return DictStatistics[name].NumberOfRequests;
        }
    }
}
