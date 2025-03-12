using Microsoft.EntityFrameworkCore;
using PruebaPeriferiaIT.Application.Interfaces.Repository;
using PruebaPeriferiaIT.Application.Interfaces.Service;
using PruebaPeriferiaIT.Application.Queries;
using PruebaPeriferiaIT.Application.Services;
using PruebaPeriferiaIT.Core.Entitites;
using PruebaPeriferiaIT.Infrastructure.Persistence;
using PruebaPeriferiaIT.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("Data Source=company.db"));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SeedData(AppDbContext context)
{
    if (!context.Departments.Any())
    {
        context.Departments.AddRange(new List<Department>
        {
            new() { Id = 1, Name = "Tecnología" },
            new() { Id = 2, Name = "Recursos Humanos" },
            new() { Id = 3, Name = "Finanzas" }
        });

        context.SaveChanges();
    }
}