using Microsoft.EntityFrameworkCore;
using MyImdb.Business.Repositories;
using MyImdb.Business.Services;
using MyImdb.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<GenreService>();

builder.Services.AddScoped<MovieRepository>();
builder.Services.AddScoped<MovieService>();

builder.Services.AddScoped<ActorRepository>();
builder.Services.AddScoped<ActorService>();

builder.Services.AddScoped<MovieActorRepository>();
builder.Services.AddScoped<MovieActorService>();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
	app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => {
	endpoints.MapControllers();
});

app.Run();
