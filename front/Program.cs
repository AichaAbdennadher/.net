using front.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Blazored.LocalStorage;

namespace front
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddMudServices();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5050") }); //url de backend
            builder.Services.AddBlazoredLocalStorage(); // <- ajout ici

            builder.Services.AddScoped<PatientServices>(); ////ajouter 
            builder.Services.AddScoped<UserServices>();

            await builder.Build().RunAsync();
        }
    }
}
