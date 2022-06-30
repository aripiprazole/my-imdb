using Api;
using Microsoft.EntityFrameworkCore;
using MyImdb.Business.Repositories;
using MyImdb.Business.Services;
using MyImdb.Configuration;
using MyImdb.Entities;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(
	(hostingContext, logging) => {
		var configuration = new LoggerConfiguration();

		logging.AddSerilog(
			configuration.ReadFrom.Configuration(hostingContext.Configuration, "Serilog").CreateLogger()
		);
	}
);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ExceptionBuilder>();

builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<GenreService>();

builder.Services.AddScoped<MovieRepository>();
builder.Services.AddScoped<MovieService>();

builder.Services.AddScoped<ActorRepository>();
builder.Services.AddScoped<ActorService>();

builder.Services.AddScoped<MovieActorRepository>();
builder.Services.AddScoped<MovieActorService>();

builder.Services.AddScoped<ModelConverter>();

builder.Services.AddDbContext<AppDbContext>(
	options => {
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);
	}
);

builder.Services.AddControllersWithViews(
		options => {
			options.Filters.Add<HandleExceptionFilter>();
			options.Filters.Add<ValidateModelStateAttribute>();
		}
	)
	.AddNewtonsoftJson();

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

app.UseEndpoints(
	endpoints => {
		endpoints.MapControllers();
	}
);

app.Run();
