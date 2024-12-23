using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Ajoutez d'autres services nécessaires
builder.Services.AddControllersWithViews();

// Ajoutez la chaîne de connexion à la configuration
builder.Services.AddSingleton(new SqliteConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configurez le pipeline de traitement des requêtes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Configurez l'application pour servir index.html comme page par défaut
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();