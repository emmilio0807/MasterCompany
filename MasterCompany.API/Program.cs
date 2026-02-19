using MasterCompany.Application.Interfaces; /// For IEmployeeService
using MasterCompany.Application.Services; /// For EmployeeService
using MasterCompany.Infrastructure.Data; /// For EmployeeData

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200")
                       .AllowAnyHeader()
                       .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();   


/// Dependency Injection
builder.Services.AddScoped<IEmployeeData, EmployeeData>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build(); /// Build the application

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAngular"); /// Enable CORS for Angular app
app.MapControllers(); /// Map controller routes

app.Run();