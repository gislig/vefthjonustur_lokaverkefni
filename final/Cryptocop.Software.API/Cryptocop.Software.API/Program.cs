var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CrytoDbContext>(
    opt => opt.UseNpgsql(
        builder.Configuration.GetConnectionString(
            "CryptocopConnectionString"),
        b => 
            b.MigrationsAssembly("Cryptocop.Software.API")));

builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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