namespace CarRent.Infrastructure.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddSqlServer(configuration);

        services.AddOptions(configuration);

        services.AddScopedServices();

        services.AddSingeltonServices();

        services.AddJwtTokenAuth(configuration);

        services.AddCompression();

        services.InitializeDatabase();

        services.SeedDatabase();

        return services;
    }

    public static IServiceCollection AddOptions(this IServiceCollection services, ConfigurationManager configuration) 
    {
        var mailOptions = configuration.GetSection(nameof(MailSettings)).Get<MailSettings>();
        services.AddSingleton(mailOptions!);

        var connectionStringOptions = configuration.GetSection(nameof(ConnectionStringSettings)).Get<ConnectionStringSettings>();
        services.AddSingleton(connectionStringOptions!);

        var jwtOptions = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        services.AddSingleton(jwtOptions!);

        return services;
    }

    public static IServiceCollection AddSingeltonServices(this IServiceCollection services) 
    {
        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddSingleton<IEmailService, EmailService>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }

    public static IServiceCollection AddScopedServices(this IServiceCollection services) 
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ICarImageRepository, CarImageRepository>();

        return services;
    }

    public static IServiceCollection AddJwtTokenAuth(this IServiceCollection services, ConfigurationManager configuration) 
    {
        var jwtSetting = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()!;

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSetting.Issuer,
                    ValidAudience = jwtSetting.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSetting.Secret)),
                };
            }); 

        return services;
    }

    public static IServiceCollection AddSqlServer(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionStringOptions = configuration.GetSection(nameof(ConnectionStringSettings)).Get<ConnectionStringSettings>();

        services.AddDbContext<CarRentContext>(options =>
            options.UseSqlServer(connectionStringOptions!.SqlConnection));

        return services;
    }

    private static IServiceCollection AddCompression(this IServiceCollection services)
    {
        services.Configure<GzipCompressionProviderOptions>(options => 
        {
            options.Level = System.IO.Compression.CompressionLevel.Fastest;
        });

        services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
            options.EnableForHttps = true;
        });
        return services;
    }

    public static IServiceCollection InitializeDatabase(this IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();
        var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        using var context = sp.GetService(typeof(CarRentContext)) as CarRentContext;
        context!.Database.Migrate();

        return services;
    }

    public static IServiceCollection SeedDatabase(this IServiceCollection services) 
    {
        var sp = services.BuildServiceProvider();
        var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        using var context = sp.GetService(typeof(CarRentContext)) as CarRentContext;

        var seed = new Seed(context!);

        seed.Init();

        return services;
    }
}
