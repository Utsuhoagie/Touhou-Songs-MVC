using Microsoft.EntityFrameworkCore;
using Serilog;
using Touhou_Songs_MVC.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Touhou_Songs_MVC_Context>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("Touhou_Songs_MVC_Context") ?? throw new InvalidOperationException("Connection string 'Touhou_Songs_MVC_Context' not found.")));

try
{
	// Add services to the container.
	builder.Host.UseSerilog(
	(context, services, configuration) =>
		configuration
			.ReadFrom.Configuration(context.Configuration)
			.ReadFrom.Services(services)
			.Enrich.FromLogContext()
			.WriteTo.Console(
				outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u5}] {Message:lj}{NewLine}{Exception}"
			)
	);

	builder.Services.AddControllersWithViews();

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (!app.Environment.IsDevelopment())
	{
		app.UseExceptionHandler("/Home/Error");
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
	}

	app.UseHttpsRedirection();
	app.UseStaticFiles();

	app.UseRouting();

	app.UseAuthorization();

	app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

	app.Run();
}

#region==== Catch ====
catch (Exception ex)
{
	if (ex is HostAbortedException)
	{
		return;
	}
	Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
	Log.CloseAndFlush();
}
#endregion