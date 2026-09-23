var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


// TODO: Add CORS you may add it to json settings
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowFrontend", policy =>
//     {
//         policy
//             .WithOrigins(
//                 "https://localhost:5173",       // React/Vite local
//                 "https://www.mywebsite.com"     // Production UI
//             )
//             .AllowAnyHeader()
//             .AllowAnyMethod();
//     });
// });

app.UseAuthorization();

app.MapControllers();

app.Run();
