using CodeCollab___WorkspaceService.Utils;
using CarrotMQ;
using CodeCollab___WorkspaceService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


string hostname = "amqp://guest:guest@rabbitmq:5672/";
string appName = "WorkspaceService";
string exchangeName = "CodeCollab";
string queueName = "workspace-queue";
WorkspaceService workspaceService = new WorkspaceService();
BasicMessageHandler messageHandler = new BasicMessageHandler(workspaceService);
Messenger messenger = new Messenger(hostname, appName, exchangeName, queueName, messageHandler);

builder.Services.AddSingleton<Messenger>(messenger);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
