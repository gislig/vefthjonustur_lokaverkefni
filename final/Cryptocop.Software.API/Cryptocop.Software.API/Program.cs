using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


// Register Services
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<ITokenRepository, TokenRepository>();
builder.Services.AddTransient<IUserSessionRepository, UserSessionRepository>();
builder.Services.AddTransient<IUserSessionService, UserSessionService>();
builder.Services.AddTransient<IAddressService, AddressService>();
builder.Services.AddTransient<IAddressRepository, AddressRepository>();

builder.Services.AddDistributedMemoryCache();
string sessionTimeout = builder.Configuration["Session:SessionTimeout"];
builder.Services.AddSession(o => 
    o.IdleTimeout = TimeSpan.FromMinutes(Convert.ToInt32(sessionTimeout))
); // Set Session Timeout


// TODO: MIDDLEWARE, MIGHT BE A GOOD IDEA TO MOVE THIS TO A SEPARATE FILE
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        //ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
    };
    o.Events = new JwtBearerEvents()
    {
        OnTokenValidated = async context =>
        {
            var claim = context.Principal.Claims.FirstOrDefault(c => c.Type == "tokenId").Value;
            var email = context.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var name = context.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var fullName = context.Principal.Claims.FirstOrDefault(c => c.Type == "fullName").Value;
            // get new claim and convert the claim into an array of data
            
            // get the token service
            var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
            int.TryParse(claim, out var tokenId);
            Console.WriteLine("Found token : " + tokenId);
            Console.WriteLine("Found email : " + email);
            Console.WriteLine("Found name : " + name);
            Console.WriteLine("Found fullName : " + fullName);
            
            // TODO: Finish middleware for token validation
            //var tokenService = context.HttpContext.RequestServices.GetService<IJwtTokenService>();
            //var tokenBlacklisted = myTokenService.IsBlacklisted(tokenId);
            /*
            Console.WriteLine();
            if(tokenService.IsTokenBlacklisted(tokenId))
            {
                // set response to unauthorized
                context.Response.StatusCode = 401;
                            
                // Respond
                await context.Response.WriteAsync("Invalid token"); 
            }
            */
        }
    };
});

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAccountService, AccountService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<CrytoDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("CryptocopConnectionString"), b => b.MigrationsAssembly("Cryptocop.Software.API")));

builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cryptocop",
        Version = "v1",
        Description = "A simple API for the Cryptocop",
        Contact = new OpenApiContact
        {
            Name = "Gísli Guðmundsson",
            Email = "gisligudm18@ru.is"
        }
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    // TODO: Find out why the file creation is not working.
    string basePath = AppContext.BaseDirectory;
    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(basePath, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();
app.UseSession();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();