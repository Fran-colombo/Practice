using Application.Interfaces;
using Application.Services;
using Infraestructure;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Services;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UnauthorizeIfUser", policy =>
    {
        policy.RequireAssertion(context =>
        {
            return !(context.User.Identity?.IsAuthenticated == true && context.User.IsInRole("User"));
        });
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("PracticeApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Acá pegar el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "PracticeApiBearerAuth" }
                }, new List<string>() }
    });
});

builder.Services.AddCors();

builder.Services.AddDbContext<PracticeContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DBConectionString")));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]!))
        };
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
