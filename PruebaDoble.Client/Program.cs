using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PruebaDoble.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuración dinámica de la URL base
var baseAddress = builder.HostEnvironment.IsDevelopment() 
    ? "http://localhost:5000/" // URL local para desarrollo
    : builder.HostEnvironment.BaseAddress; // URL base en producción

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

await builder.Build().RunAsync();
