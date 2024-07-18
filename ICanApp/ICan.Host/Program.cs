using ICan.Core.EFDbContext;
using ICan.Core.Enum;
using ICan.Core.StartExtesion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(connectionString);
});
// Add services to the container.

builder.Services.AddAuthorization();
builder.Services.AddAuthenticationJWTConfig(builder.Configuration["JWT:SecretKey"], builder.Configuration["JWT:Issuer"], builder.Configuration["JWT:Audience"]);
builder.Services.AddControllers()
    .AddNewtonsoftJson(option =>
        //忽略循环引用
        option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    );
builder.Services.AddAutoMapper(typeof(AutoMapConfig));
builder.Services.AddCros();
builder.Host.AutoFacRegister();
#if DEBUG
string ips = builder.Configuration.GetValue(typeof(string), "Urls") as string;
if (ips != null)
{
    builder.WebHost.UseUrls(ips);
}
#endif

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer schema.
                    Enter 'Bearer' [space] amd then your token in the text input below
                    Example: 'Bearer' 123456asdfgh",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme="0auth2",
                Name=JwtBearerDefaults.AuthenticationScheme,
                In= ParameterLocation.Header
            },
            new List<string> { }
        }
    });
    foreach (var item in typeof(ApiVersionInfo).GetEnumNames().ToList())
    {
        option.SwaggerDoc(item, new OpenApiInfo { Title = "后台管理系统", Version = item });
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        foreach (var n in typeof(ApiVersionInfo).GetEnumNames().ToList())
        {
            c.SwaggerEndpoint($"/swagger/{n}/swagger.json", $"后台API管理系统-Version: {n}");
        }
    });
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
