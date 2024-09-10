using Aplication.Interfaces;
using Aplication.Services;
using CRMSystem.Data;
using Infraestructure.Commands;
using Infraestructure.Querys;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CrmContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddScoped<IClientServices, ClientService>();
builder.Services.AddScoped<IClientQuery, ClientQuery>();
builder.Services.AddScoped<IClientsCommand, ClientsCommand>();

builder.Services.AddScoped<ICampaignTypeService, CampaignTypeService>();
builder.Services.AddScoped<ICampaignTypeQuery, CampaignTypeQuery>();

builder.Services.AddScoped<IInteractionTypeService, InteractionTypeService>();
builder.Services.AddScoped<IInteractionTypeQuery, InteractionTypeQuery>();

builder.Services.AddScoped<ITaskStatusService, TaskStatusService>();
builder.Services.AddScoped<ITaskStatusQuery, TaskStatusQuery>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUsersQuery, UsersQuery>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectCommands, ProjectCommands>();
builder.Services.AddScoped<IProjectQuery, ProjectQuery>();

builder.Services.AddScoped<ITaskCommand, TaskCommand>();
builder.Services.AddScoped<ITaskQuery, TaskQuery>();




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
