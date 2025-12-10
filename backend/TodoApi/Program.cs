using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Data;
using TodoApi.Models;


var builder = WebApplication.CreateBuilder(args);

// JWT config (appsettings or secrets)
builder.Configuration["Jwt:Key"] = "Y7Df!9sLh28ZKp9A3vNqB5Tt!XwG@R4u";
builder.Configuration["Jwt:Issuer"] = "TodoApi";
builder.Configuration["Jwt:Audience"] = "TodoApiUsers";


//Acepta llamados de Angular
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
	policy =>
	{
		policy.WithOrigins("http://localhost:4200")
			  .AllowAnyHeader()
			  .AllowAnyMethod()
			  .AllowCredentials();
	});
});


// Services
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("todoDb"));
builder.Services.AddScoped<AuthService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT auth
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Seed demo user & todos
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

	// Usuario demo
	if (!db.Users.Any())
	{
		db.Users.Add(new User { Email = "test@demo.com", PasswordHash = "Password123" });
	}

	// TODOs demo
	if (!db.Todos.Any())
	{
		db.Todos.AddRange(
			new TodoItem { Title = "Back .NET", IsCompleted = false },
			new TodoItem { Title = "Frond Angular", IsCompleted = false },
			new TodoItem { Title = "Crear To-Do List", IsCompleted = true }
		);
	}

	db.SaveChanges();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
