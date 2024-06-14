using ClassLibrary1.Third_Parties;
using HandMade;
using WebAPI.Gateway.Configuration;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var dbConnection = builder.Configuration.GetConnectionString("HandMadeDB") ?? "";

builder.Services.AddDependency(dbConnection);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//Add cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
//Third-parties
builder.Services.Configure<CloudConfig>(builder.Configuration.GetSection(CloudConfig.ConfigName));
builder.Services.Configure<MomoConfig>(builder.Configuration.GetSection(MomoConfig.ConfigName));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
// app.UseSwaggerUI(c =>
// {
//     c.SwaggerEndpoint("/swagger/v1/swagger.json", "HandMade API");
//     c.RoutePrefix=string.Empty;
// });
app.UseSwaggerUI();
app.UseCors();
app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.Run();