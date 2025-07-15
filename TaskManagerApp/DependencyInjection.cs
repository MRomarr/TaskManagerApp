using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace TaskManagerApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwaggerGen(options=>
                {
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Enter 'Bearer' followed by space and your JWT token."
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            Array.Empty<string>()
                        }
                    });
                });

            services.AddDbContext(configuration);
            services.AddIdentity();
            services.AddJWT(configuration);
            services.AddRepositores();
            services.AddServices();
            return services;
        }
        private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection String Not Found");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }

        private static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser,IdentityRole> ( options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        private static IServiceCollection AddJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTHelper>(configuration.GetSection(JWTHelper.Section));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    var jwtSettings = configuration.GetSection(JWTHelper.Section).Get<JWTHelper>();
                    if (jwtSettings == null)
                    {
                        throw new InvalidOperationException("JWT settings not found in configuration.");
                    }
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                        ClockSkew = TimeSpan.Zero // Optional: Set clock skew to zero for immediate expiration
                    };
                });
            return services;
        }

        private static IServiceCollection AddRepositores(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IListService, ListService>();
            return services;
        }
    }
}
