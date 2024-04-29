using BusinessLayer;
using DataAccesLayer;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services of interfaces
builder.Services.AddScoped<ICourseService, CourseServices>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

// Resolve CORS issue whith alow frontend with port 4200 to have acces to the backend
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Apply CORS policy
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

// create custom middleware 
app.Use(async (context, next) =>
{
    context.Request.Headers.TryGetValue("user-agent", out StringValues headerValue);
    if(headerValue.ToString().Contains("Edg"))
        await next();
    else
    {
        context.Response.StatusCode = 412;
        await context.Response.WriteAsync("Please use chrome " + " " + headerValue);
    }
}

);




app.Run();
