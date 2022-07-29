using JWTAuth.Business;
using JWTAuth.Core;
using JWTAuth.Data;
using JWTAuth.WebAPI;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddAutoMapperDependecyInjection(builder);
builder.Services.AddJwtConfigurationService(builder);
builder.Services.AddCustomSwagger();
builder.Services.AddDbContextDependecyInjection(builder);
builder.Services.AddDataLayerServiceRegistration();
builder.Services.AddCoreLayerServiceRegistration();
builder.Services.AddBusinessLayerServiceRegistration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        //Hide Schemas
        opt.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<HeartbeatMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
