using KlantenService_Steam_Framework.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KlantenService_Steam_Framework.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using NETCore.MailKit.Infrastructure.Internal;
using static KlantenService_Steam_Framework.Services.MailService;
using KlantenService_Steam_Framework;
using Microsoft.OpenApi.Models;
using KlantenService_Steam_Framework.Services;
using Microsoft.Extensions.DependencyInjection;
using GroupSpace23.Services;
using Microsoft.AspNetCore.Hosting;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<KlantenServiceUser>((options) => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();

        // De volgende configuratie van de MailKit wordt toegevoegd als demonstratie, maar gebruiken we niet.
        // Deze is "overschreven" door het gebruik van de database-parameters in Globals, en ge�nitialiseerd in de data Initializer
        builder.Services.Configure<MailKitOptions>(options =>
        {
            options.Server = builder.Configuration["ExternalProviders:MailKit:SMTP:Address"];
            options.Port = Convert.ToInt32(builder.Configuration["ExternalProviders:MailKit:SMTP:Port"]);
            options.Account = builder.Configuration["ExternalProviders:MailKit:SMTP:Account"];
            options.Password = builder.Configuration["ExternalProviders:MailKit:SMTP:Password"];
            options.SenderEmail = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
            options.SenderName = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderName"];
            options.Security = true;  // true zet ssl or tls aan
        });

        // Add services for globalization/localization
        builder.Services.AddLocalization(options => options.ResourcesPath = "Translations");
        builder.Services.AddMvc()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();


        builder.Services.AddControllersWithViews();

        //Add services for RESTFull API
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
                            {   
                                c.SwaggerDoc("v1", 
                                new OpenApiInfo { Title = "KlantenService_Steam_Framework", Version = "v1" });
                            });

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });

        // Onze dependency aankondigen als service
        builder.Services.AddTransient<IMyUser, MyUser>();

        var app = builder.Build();

        Globals.App = app;          // Zorgt dat dit altijd een instantie van de huidige app bijhoudt

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        else
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "KlantenService_Steam_Framework v1"); });
        }


        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<KlantenServiceUser>>();
            await ApplicationDbContext.DataInitializer(context, userManager);
        }

        var supportedCultures = new[] { "en-US", "nl", "ar", "de" };
        var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);
        app.UseRequestLocalization(localizationOptions);


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.UseMiddleware<UserStats>();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.Run();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<GameService>();
    }

    public void Configure(IApplicationBuilder app)
    {
        // Voeg de GameListMiddleware toe aan de pipeline
        app.UseMiddleware<GameListMiddleware>();
    }

}