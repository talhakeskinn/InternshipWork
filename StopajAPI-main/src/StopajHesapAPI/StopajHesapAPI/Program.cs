using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using StopajHesapAPI.Middlewares;
using StopajHesapAPI.Models;
using StopajHesapAPI.Services.Abstraction;
using StopajHesapAPI.Services.Concerete; // Ensure this namespace is included

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IStopajHesapService, StopajHesapService>();
// Register IUserService implementation
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ResponseStopaj>();


// Configure authentication with BasicAuthenticationHandler
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stopaj Hesaplama API", Version = "v1" });

    // Add Basic Authentication to Swagger
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http, //Type: Güvenlik þemasý türü. Burada SecuritySchemeType.Http kullanýlarak HTTP tabanlý                                bir kimlik doðrulama þemasý belirtir.

        Scheme = "basic",               // Kimlik doðrulama þemasý türü. basic olarak ayarlanmýþ, yani temel kimlik doðrulama                                  (username ve password) kullanýlýr.
        In = ParameterLocation.Header,  // Güvenlik tanýmýnýn konumu. ParameterLocation.Header ile, Authorization baþlýðý                                      içinde gönderilmesi gerektiðini belirtir.
        Name = "Authorization",         //Bu, Swagger UI'nin kimlik doðrulama için kullanacaðý baþlýk adý

        Description = "Basic Authentication header using the Bearer scheme." //Kimlik doðrulama yöntemi hakkýnda açýklama.
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new string[] {}
        }
        /* new OpenApiSecurityScheme: Tanýmladýðýnýz güvenlik þemasýný referans alýr.
          Reference: Güvenlik þemasýnýn Swagger JSON'daki tanýmýný belirtir.
          new string[] {}: Bu, belirli bir kapsam veya izin gereksinimi belirtmez, yani temel kimlik doðrulama þemasý tüm uç                 noktalar için geçerlidir. */
    });
});


var app = builder.Build();

if (app.Environment.IsProduction())
    app.UseHsts();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stopaj Hesaplama API Open API"));
    // Bu kýsým, Swagger UI'ye hangi Swagger JSON dosyasýný kullanarak API belgelerinizi yüklemesi gerektiðini belirtir. Burada iki parametre kullanýlýr
}

app.UseHttpsRedirection();

// Add authentication and authorization middleware
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
