using WebApi.DBOperations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB")); //new

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.Run(); //-new

//1. Get the IWebHost which will host this application.
//  var host = CreateHostBuilder(args).Build();

//2. Find the service layer within our scope.
using (var scope = app.Services.CreateScope()) //new
{
    //3. Get the instance of BoardGamesDBContext in our services layer
    var services = scope.ServiceProvider;
    //4. Call the DataGenerator to create sample data
    DataGenerator.Initialize(services);
}

//Continue to run the application
app.Run();
