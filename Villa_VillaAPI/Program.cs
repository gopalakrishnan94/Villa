using Microsoft.EntityFrameworkCore;
using Villa_VillaAPI;
using Villa_VillaAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// EFCore DBContext
builder.Services.AddDbContext<ApplicationDBContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers(options => {
    // options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();

