using Microsoft.AspNetCore.ResponseCompression;
using PruebaDoble.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:5121",             // URL para desarrollo local
                "https://tu-app.herokuapp.com"       // URL de producción en Heroku
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// Registrar servicios adicionales
builder.Services.AddScoped<QRCodeService>();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Configuración del puerto dinámico para Heroku
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

// **Importante:** Usar CORS antes de MapControllers
app.UseCors("AllowBlazorApp");

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
