using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RoteiroFacil.API.Context;
using RoteiroFacil.API.DTOs.Mapper;
using RoteiroFacil.API.Models;
using RoteiroFacil.API.Repository;
using RoteiroFacil.API.Repository.Geral;
using RoteiroFacil.API.Repository.Interfaces;
using RoteiroFacil.API.Services.Clientes;
using RoteiroFacil.API.Services.Geral;
using RoteiroFacil.API.Services.Produto;
using RoteiroFacil.API.Services.Usuario;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<RoteiroFacilContext>(opt => opt.UseMySql(connection, ServerVersion.AutoDetect(connection)));
builder.Services.AddAutoMapper(typeof(MapperProfile));

#region Services

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<UsuarioServices>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ProdutoService>();

#region Geral
builder.Services.AddScoped<LogService>();
builder.Services.AddSingleton<LogModel>();
#endregion

#endregion

#region Repository
builder.Services.AddScoped<IRepositoryCRUD<UsuarioModel>, UsuarioRepository>();
builder.Services.AddScoped<IRepositoryCRUD<ClienteModel>, ClienteRespository>();
builder.Services.AddScoped<IRepositoryCRUD<ProdutoModel>, ProdutoRepository>();
builder.Services.AddScoped<LogRepository>();
#endregion

#region JWTSwagger
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {


        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,
        },
        new List<string>()
        }

    });
});
#endregion

#region JWTAPI
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["jwt:issuer"],
        ValidAudience = builder.Configuration["jwt:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:secretkey"]))
    };

    // Personalizar o processamento do token
    opt.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Use diretamente o cabeçalho sem prefixo "Bearer"
            context.Token = context.Request.Headers["Authorization"];
            return Task.CompletedTask;
        }
    };
});
#endregion



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
