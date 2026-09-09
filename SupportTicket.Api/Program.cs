using SupportTicket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SupportTicket.Infrastructure.EFRepository;
using SupportTicket.Infrastructure.AdoRepository;
using SupportTicket.Api.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TicketDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultString"))
);
builder.Services.AddScoped<IEFRepo, EFRepo>();
builder.Services.AddScoped<IAdoRepo, AdoRepo>();
builder.Services.AddScoped<ITicketServices, TicketServices>();
var app = builder.Build();

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
