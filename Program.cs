using ecqmValidation.Library.Helpers;
using ecqmValidation.UI.Services;

namespace ecqmValidation.UI
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			ConfigureServices(builder.Services);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.MapBlazorHub();
			app.MapFallbackToPage("/_Host");

			app.Run();
		}
		public static void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			services.AddServerSideBlazor();

			services.AddHttpClient();
			services.AddSingleton<AppSettings>();
			services.AddScoped<ecqmValidationService>();

			services.AddTelerikBlazor();
			//services.AddAuthentication();
			//services.AddAuthenticationCore();
		}
	}
}