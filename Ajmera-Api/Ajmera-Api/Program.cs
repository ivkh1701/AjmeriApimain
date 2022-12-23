using Ajmera_Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation.AspNetCore;
using Ajmera_Api.Filters;
using FluentValidation;
using Ajmera_Data.Repository;
using Ajmera_Services.Services;
using Ajmera_Api.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Custom code for add logger

// Add services to the container.
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

#endregion

#region Custom code for data connection with entity framework

//Add DB Context 
builder.Services.AddDbContext<AjmeraDbContext>(
  options => options.UseSqlServer(builder.Configuration.GetValue<string>("AjmeraDbConnection"),
//options => options.use
sso => sso.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)
  ));

#endregion

#region Custom code for register services and auto mapper

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion

#region Custom code for register filters and fluent validators

//register filters
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ValidationFilter>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ValidationFilter>();
});

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Custom Code for handle code with custom codes

app.UseMiddleware<ErrorMiddleWare>();

#endregion

#region Custom code make sure migration will be register

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AjmeraDbContext>();
    db.Database.Migrate();
}
#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
