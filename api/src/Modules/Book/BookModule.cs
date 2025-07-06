namespace Book;
public static class BookModule
{
    public static IServiceCollection AddBookModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        //services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        //services.AddScoped<ICatalogRepository, CatalogRepository>();
        //services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<BookDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        services.Scan(s => s
        .FromAssemblies(Assembly.GetExecutingAssembly())
        .AddClasses(classes => classes.AssignableTo<ICapSubscribe>())
        .AsSelfWithInterfaces()
        .WithTransientLifetime());

        //MapsterConfig.RegisterMappings();

        return services;
    }
    public static IApplicationBuilder UseBookModule(this IApplicationBuilder app)
    {
        app.UseMigration<BookDbContext>();
        return app;
    }
}
