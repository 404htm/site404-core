var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var isDevelopment = environment == Environments.Development;

var builder = WebApplication.CreateBuilder(args);

if (isDevelopment)
{
    builder.Services.AddCors(p => p.AddPolicy("dev", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSwaggerGen();





var app = builder.Build();

if (isDevelopment)
{
    app.UseCors("dev");
}


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Urls.Add("http://*:8080");
app.Urls.Add("http://*:80");
app.Run();
