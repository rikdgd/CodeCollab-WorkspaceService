using CodeCollab___WorkspaceService.Utils;

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


builder.Services.AddSingleton(provider =>
{
    string hostname = "localhost";
    string appName = "WorkspaceService";
    string exchangeName = "CodeCollab";
    string queueName = "workspace-queue";

    return new Messenger(hostname, appName, exchangeName, queueName, new BasicMessageHandler());
});


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
