using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using NotificationService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHostedService<Consumer>();

var host = builder.Build();
host.Run();
