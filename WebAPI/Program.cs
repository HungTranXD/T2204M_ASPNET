using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1 - Them CORS:
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            //policy.WithOrigins("http://24h.com.vn"); cho phep dia chi IP goi API
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        }
    );
});

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


// Add connection to databbase.
var connectionString = builder.Configuration.GetConnectionString("T2204M_ASPNET_API");
builder.Services.AddDbContext<WebAPI.Entities.T2204mAspnetApiContext>(
    options => options.UseSqlServer(connectionString)
);

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

// 1 - Them CORS:
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
