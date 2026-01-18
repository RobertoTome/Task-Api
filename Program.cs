using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TaskApi;
using TaskApi.Hubs;
using TaskApi.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

builder.Services.AddScoped<ITaskService, TaskService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    //{
    //    policy.AllowAnyOrigin()
    //          .AllowAnyMethod()
    //          .AllowAnyHeader();
    //});
    {
        policy.SetIsOriginAllowed(_ => true)  // ← Cambia esto
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();             // ← Agrega esto
    });
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.MapHub<TaskHub>("/taskHub");

app.Run();

//iniciar con           ngrok http 5211