using BusinessLayer;
using DataAccesLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services of interfaces
builder.Services.AddScoped<ICourseService, CourseServices>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();





//authentification config 

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();

// Resolve CORS issue whith alow frontend with port 4200 to have acces to the backend
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});


app.UseRouting();



app.UseAuthorization();

app.MapControllers();

// create custom middleware 
//app.Use(async (context, next) =>
//{
//    context.Request.Headers.TryGetValue("user-agent", out StringValues headerValue);
//    if(headerValue.ToString().Contains("Edg"))
//        await next();
//    else
//    {
//        context.Response.StatusCode = 412;
//        await context.Response.WriteAsync("Please use chrome " + " " + headerValue);
//    }
//}

//);




app.Run();
