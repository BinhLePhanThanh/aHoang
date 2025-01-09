using aHoang.Context;
using aHoang.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins("http://192.168.1.8:5500") // Cho phép mọi origin
            .AllowAnyMethod() // Cho phép mọi phương thức
            .AllowAnyHeader() // Cho phép mọi tiêu đề
            .AllowCredentials(); // Cho phép cookie
    });
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<EmailService>();
builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql((builder.Configuration.GetConnectionString("DefaultConnection"))));
builder.Services.AddScoped<ItemService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // Thực hiện migration
}
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
