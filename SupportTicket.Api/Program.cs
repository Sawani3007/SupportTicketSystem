using Microsoft.EntityFrameworkCore;
using SupportTicket.Api.Middleware;
using SupportTicket.Api.Services;
using SupportTicket.Infrastructure.AdoRepository;
using SupportTicket.Infrastructure.Data;
using SupportTicket.Infrastructure.EFRepository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TicketDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultString"))
);
builder.Services.AddScoped<IEFRepo, EFRepo>();
builder.Services.AddScoped<IAdoRepo, AdoRepo>();
builder.Services.AddScoped<ITicketServices, TicketServices>();
var app = builder.Build();
app.UseCors("FrontendPolicy");
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
