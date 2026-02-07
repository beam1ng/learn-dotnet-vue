using interfaces;
using Microsoft.EntityFrameworkCore;
using other;
using services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddCors((options) =>
{
    options.AddPolicy("DevPolicy", p =>
    {
        p
            .AllowAnyOrigin()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(
    connectionString, 
    ServerVersion.AutoDetect(connectionString)
));

builder.Services.AddScoped<IWordsService,WordsService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("DevPolicy");
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
