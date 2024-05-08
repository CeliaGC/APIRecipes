using API.IServices;
using API.Middlewares;
using API.Services;
using Data;
using Logic.Ilogic;
using Logic.ILogic;
using Logic.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Security.IServices;
using Security.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter authorization",
        Name = "EndpointAuthorize",
        Type = SecuritySchemeType.Http,
        BearerFormat = "Bearer",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});



builder.Services.AddScoped<IRecipeItemLogic, RecipeItemLogic>();
builder.Services.AddScoped<IRecipeItemService, RecipeItemService>();

builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddScoped<IUserSecurityLogic, UserSecurityLogic>();
builder.Services.AddScoped<IUserSecurityService, UserSecurityService>();

builder.Services.AddScoped<ICategoryItemLogic, CategoryItemLogic>();
builder.Services.AddScoped<ICategoryItemService, CategoryItemService>();

builder.Services.AddScoped<IAlergenItemLogic, AlergenItemLogic>();
builder.Services.AddScoped<IAlergenItemService, AlergenItemService>();

builder.Services.AddScoped<IngredientItemLogic, IngredientItemLogic>();
builder.Services.AddScoped<IIngredientItemService, IngredientItemService>();

builder.Services.AddScoped<IOrderItemLogic, OrderItemLogic>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();


builder.Services.AddDbContext<ServiceContext>(
        options => options.UseSqlServer("name=ConnectionStrings:ServiceContext"));


var app = builder.Build();
app.UseCors("AllowAll");
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) => {
    var serviceScope = app.Services.CreateScope();
    var userSecurityService = serviceScope.ServiceProvider.GetRequiredService<IUserSecurityService>();
    var requestAuthorizationMiddleware = new RequestAuthorizationMiddleware(userSecurityService);
    requestAuthorizationMiddleware.ValidateRequestAutorizathion(context);
    await next();
});

app.UseHttpsRedirection();



app.MapControllers();

app.Run();
