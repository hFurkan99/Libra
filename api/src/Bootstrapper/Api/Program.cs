// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

var userAssembly = typeof(UserModule).Assembly;
var catalogAssembly = typeof(CatalogModule).Assembly;

builder.Services
    .AddCarterWithAssemblies(userAssembly, 
    catalogAssembly);

builder.Services
    .AddMediatRWithAssemblies(userAssembly,
    catalogAssembly);

builder.Services
    .AddCapWithRabbitMq(builder.Configuration);

builder.Services
    .AddUserModule(builder.Configuration)
    .AddCatalogModule(builder.Configuration);

builder.Services.AddControllers();

builder.Services
    .AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendDev", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontendDev");

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => { });

app.UseUserModule()
    .UseCatalogModule();

app.MapControllers();
app.MapCarter();

app.Run();
