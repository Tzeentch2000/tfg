using ContextDB;
using Microsoft.EntityFrameworkCore;
using Wrapper;
using RepositoryWrapperContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Serilog;
using Serilog.Events;


var builder = WebApplication.CreateBuilder(args);

//"User ID=postgres;Password=sanvalero12;Server=localhost;Port=5432;Database=Tfg7; IntegratedSecurity=true;Pooling=true;"

// Add services to the container.
// Database connection
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseNpgsql(connectionString));
// Dependency injection
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>(); 
// Ignore cycles error
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    // Titulo
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "Tfg Guillermo", Version = "v1"});

    //Boton authorize
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
        Description = "Jwt Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
}); 

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddCors(p=>p.AddPolicy("corspolicy",build =>{
    //build.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader();
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Configuración del logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("error/log.txt", restrictedToMinimumLevel: LogEventLevel.Error)
    .CreateLogger();

try
{
    app.Run();
}
catch (Exception ex)
{
    // Registrar el error en el log
    Log.Error(ex, "Unhandled Exception");
}
finally
{
    Log.CloseAndFlush();
}
