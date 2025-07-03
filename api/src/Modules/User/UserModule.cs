namespace User;
public static class UserModule
{
    public static IServiceCollection AddUserModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        //services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        //services.AddScoped<IUserRepository, UserRepository>();
        //services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<UserDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        //MapsterConfig.RegisterMappings();

        services.AddIdentity<AppUser, IdentityRole<Guid>>()
        .AddEntityFrameworkStores<UserDbContext>()
        .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });

        services.AddAuthorization();

        return services;
    }
    public static IApplicationBuilder UseUserModule(this IApplicationBuilder app)
    {
        app.UseMigration<UserDbContext>();
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}
