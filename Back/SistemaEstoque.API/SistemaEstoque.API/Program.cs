using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;
using SistemaEstoque.API.Repository;
using SistemaEstoque.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SistemaEstoque.API.Context;
using Microsoft.OpenApi.Models;
using SistemaEstoque.API.Repository.Produto;
using SistemaEstoque.API.Repository.Produto.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connection, ServerVersion.AutoDetect(connection)));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy",
        build =>
        {
            build.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});
builder.Services.AddAutoMapper(typeof(UsuarioModel));

#region Repository
builder.Services.AddScoped<IDbMethods<ClienteModel>, ClienteRepository>();
builder.Services.AddScoped<IDbMethods<UsuarioModel>, UsuariosRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IDbMethods<TipoProdutoModel>, TpProdutoRepository>();
builder.Services.AddScoped<IDbMethods<PedidoModel>, PedidoRepository>();
builder.Services.AddScoped<IDbMethods<EmpresaModel>, EmpresaRepository>();
builder.Services.AddScoped<IDbMethods<LoginModel>, LoginRepository>();
builder.Services.AddScoped<IDbMethods<LicencaModel>, LicencaRepository>();
builder.Services.AddScoped<LogRepository>();
#endregion

#region Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<TpProdutoService>();
builder.Services.AddScoped<PedidoServices>();
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<LicencaService>();
#endregion

builder.Services.AddControllers();

#region JWTBearer
builder.Services.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", opt =>
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
}
);
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


var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
