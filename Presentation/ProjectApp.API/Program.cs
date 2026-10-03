using ProjectApp.Application.Extensions;
using ProjectApp.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



//IServiceCollection sayesinde . alta indiip devam ettim!
builder.Services.AddPersistanceServices(builder.Configuration)
                .AddAplicationServices();







builder.Services.AddControllers();



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
