using Microsoft.EntityFrameworkCore;
using SEProject.DataAccess.EF;
using SEProject.DataAccess.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// 🔥 1️⃣ Configurare baza de date
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddSession();

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

app.UseSession();
app.UseAuthorization();

app.MapRazorPages();
app.Run();
