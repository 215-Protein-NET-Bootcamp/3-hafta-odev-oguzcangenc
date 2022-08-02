using FluentValidation.AspNetCore;
using JWTAuth.Business;
using JWTAuth.Core;
using JWTAuth.Data;
using JWTAuth.WebAPI;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
                .AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

builder.Host.UseSerilogExtension();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapperDependecyInjection(builder);
builder.Services.AddJwtConfigurationService(builder);
builder.Services.AddCustomSwagger();
builder.Services.AddDbContextDependecyInjection(builder);
builder.Services.AddDataLayerServiceRegistration();
builder.Services.AddCoreLayerServiceRegistration();
builder.Services.AddBusinessLayerServiceRegistration();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<AppEfDbContext>();
    dataContext.Database.Migrate();
}
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    //Hide Schemas
    opt.DefaultModelsExpandDepth(-1);
});

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<HeartbeatMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
