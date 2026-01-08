using dotnnet_api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Services
// =======================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJs", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3000",
                "http://alb-users-652528759.us-east-1.elb.amazonaws.com"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? Environment.GetEnvironmentVariable("DB_CONNECTION")
    )
);

var app = builder.Build();

// =======================
// Middleware
// =======================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ❌ NO HTTPS REDIRECTION IN ECS
// app.UseHttpsRedirection();

app.UseCors("AllowNextJs");

app.UseAuthorization();

// =======================
// EF Migrations (CRITICAL)
// =======================

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.MapControllers();

app.Run();



// using dotnnet_api.Data;
// using Microsoft.EntityFrameworkCore;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();


// //var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowNextJs",
//         policy =>
//         {
//             policy.WithOrigins("http://localhost:3000",
//              "http://alb-users-652528759.us-east-1.elb.amazonaws.com" // deployed frontend 
//             )
//                   .AllowAnyHeader()
//                   .AllowAnyMethod();
//         });
// });

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseNpgsql(
//         builder.Configuration.GetConnectionString("DefaultConnection") ??
//         Environment.GetEnvironmentVariable("DB_CONNECTION")
//     ));

// var app = builder.Build();


// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// // app.UseHttpsRedirection();

// if (!app.Environment.IsProduction())
// {
//     app.UseHttpsRedirection();
// }




// app.UseCors("AllowNextJs");


// app.UseAuthorization();

// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     db.Database.Migrate();
// }

// app.MapControllers();

// app.Run();
