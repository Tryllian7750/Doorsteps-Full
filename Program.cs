
  
using Microsoft.EntityFrameworkCore;
using ExperimentApi.Models;
using ExperimentApi.Controllers;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// builder.Services.AddControllers(options =>
//   {
//     var jsonInputFormatter = options.InputFormatters
//         .OfType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>()
//         .Single();
//     jsonInputFormatter.SupportedMediaTypes.Add("application/csp-report");
//   }).AddNewtonsoftJson(options =>
//     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//builder.Services.AddDbContext<ExperimentContext>(opt =>
    //opt.UseInMemoryDatabase("DataSource=Experiments.db"));

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ExperimentContext>(opt =>
    opt.UseSqlite("DataSource=Experiments.db"));



var app = builder.Build();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapGet("/Environment", () =>
{
    return new EnvironmentInfo();
});




// void SeedData(IHost app)
// {
//     var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

//     using (var scope = scopedFactory.CreateScope())
//     {
//         var service = scope.ServiceProvider.GetService<DataSeeder>();
//         service.Seed();
//     }
// }


//SeedData(app);

app.UseSwagger(x => x.SerializeAsV2 = true);
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Seeding.Seed();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
