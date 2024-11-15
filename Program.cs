using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ProjetoPI.Data;
using ProjetoPI.Interface;
using ProjetoPI.Repository;
using ProjetoPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjetoPI", Version = "v1" });
});

// Configura��o de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // URL do frontend em React
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Registra o contexto do banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"))); // String de conex�o

// Registra os servi�os e reposit�rios no cont�iner de DI
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IDoadorRepository, DoadorRepository>();
builder.Services.AddScoped<IOngRepository, OngRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjetoPI v1"));
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp"); // Aplica a pol�tica de CORS
app.UseAuthorization();

app.MapControllers();

app.Run();
