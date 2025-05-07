using Microsoft.EntityFrameworkCore;
using SEProject.Data;
using SEProject.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔥 1️⃣ Configurare baza de date
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔥 2️⃣ Adăugare Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// 🔥 3️⃣ Configurare Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.Run();
