using front.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace front
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5050") }); //url de backend

            builder.Services.AddScoped<PatientServices>(); ////ajouter 
            builder.Services.AddScoped<userServices>();

            await builder.Build().RunAsync();
        }
    }
}
