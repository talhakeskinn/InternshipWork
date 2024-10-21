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
        Type = SecuritySchemeType.Http, //Type: G�venlik �emas� t�r�. Burada SecuritySchemeType.Http kullan�larak HTTP tabanl�                                bir kimlik do�rulama �emas� belirtir.

        Scheme = "basic",               // Kimlik do�rulama �emas� t�r�. basic olarak ayarlanm��, yani temel kimlik do�rulama                                  (username ve password) kullan�l�r.
        In = ParameterLocation.Header,  // G�venlik tan�m�n�n konumu. ParameterLocation.Header ile, Authorization ba�l���                                      i�inde g�nderilmesi gerekti�ini belirtir.
        Name = "Authorization",         //Bu, Swagger UI'nin kimlik do�rulama i�in kullanaca�� ba�l�k ad�

        Description = "Basic Authentication header using the Bearer scheme." //Kimlik do�rulama y�ntemi hakk�nda a��klama.
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
        /* new OpenApiSecurityScheme: Tan�mlad���n�z g�venlik �emas�n� referans al�r.
          Reference: G�venlik �emas�n�n Swagger JSON'daki tan�m�n� belirtir.
          new string[] {}: Bu, belirli bir kapsam veya izin gereksinimi belirtmez, yani temel kimlik do�rulama �emas� t�m u�                 noktalar i�in ge�erlidir. */
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
    // Bu k�s�m, Swagger UI'ye hangi Swagger JSON dosyas�n� kullanarak API belgelerinizi y�klemesi gerekti�ini belirtir. Burada iki parametre kullan�l�r
}

app.UseHttpsRedirection();

// Add authentication and authorization middleware
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
