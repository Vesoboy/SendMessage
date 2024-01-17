using Microsoft.Extensions.Configuration;
using SendMessage.Models;
using SendMessage.Services;
using SendMessage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// ����������� ������������ SMTP
builder.Services.Configure<SmtpConfig>(builder.Configuration.GetSection("SmtpConfig"));

builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

// ����������� EmailService � ���������� ������������
builder.Services.AddScoped<EmailService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();