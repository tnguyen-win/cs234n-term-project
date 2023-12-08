using BreweryEFClasses.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(
        builder => {
            builder
            .AllowAnyOrigin()
            .WithMethods("POST", "PUT", "DELETE", "GET", "OPTIONS")
            .AllowAnyHeader();
        });
});

builder.Services.AddDbContext<BitsContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
