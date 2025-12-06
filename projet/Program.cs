using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using projet.Data;
using projet.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
;

//ajout
var cnx = builder.Configuration.GetConnectionString("cnx");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(cnx));

builder.Services.AddScoped<IPatientRepository, PatientRepository>();  ////
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMedicamentRepository, MedicamentRepository>();
builder.Services.AddScoped<IligneMedRepository, ligneMedRepository>();
builder.Services.AddScoped<IOrdonnanceRepository, OrdonnanceRepository>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Entrez votre token JWT"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
   JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey

    (System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName);
});


//pour permette au front d'utilser les methodes
builder.Services.AddCors(
    policy => policy.AddPolicy("AllowAll", options => options.AllowAnyOrigin()
    .AllowAnyMethod().AllowAnyHeader()));


var app = builder.Build();
app.UseCors("AllowAll");
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



