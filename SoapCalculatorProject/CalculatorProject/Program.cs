using CalculatorProject.Services.Abstract;
using CalculatorProject.Services.Concrete;
using CalculatorSoapServiceReferans;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<RequestModel>();
builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddSingleton(new CalculatorSoapClient(CalculatorSoapClient.EndpointConfiguration.CalculatorSoap12));


var app = builder.Build();


app.UseSoapEndpoint<ICalculatorService>("/Service.asmx", new SoapEncoderOptions());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
