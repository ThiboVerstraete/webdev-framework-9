using Microsoft.EntityFrameworkCore;
using PeopleManager.Ui.Mvc.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//var connectionString = builder.Configuration.GetConnectionString(nameof(PeopleManagerDbContext));

builder.Services.AddDbContext<PeopleManagerDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(PeopleManagerDbContext));
    //options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    using var scope = app.Services.CreateScope();
    var database = scope.ServiceProvider.GetRequiredService<PeopleManagerDbContext>();
    if (database.Database.IsInMemory())
    {
        database.Seed();
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
