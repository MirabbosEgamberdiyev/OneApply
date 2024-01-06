using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAcceseLayer.DbContext;
using DataAcceseLayer.Entities;
using DataAcceseLayer.Interfaces;
using DataAcceseLayer.Repositories;
using DTOLayer.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace OneApply;

public static  class Startup
{
    private const string CORS_POLICY = "CorsPolicy";
    public static void AddDependencyInjection (this WebApplicationBuilder builder)
    {

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("OneApplyDbWithIdentity")));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        #region Add Identity
        builder.Services
            .AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        #endregion


        #region Config Identity for password
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.SignIn.RequireConfirmedEmail = false;
        });
        #endregion


        #region Add Authentication and JwtBearer
        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });
        #endregion

        #region Inject app Dependencies (Dependency Injection)
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

        builder.Services.AddTransient<ICertificateInterface, CertificateRepository>();
        builder.Services.AddTransient<ICertificateService, CertificateService>();

        builder.Services.AddTransient<ISkillInterface, SkillRepository>();
        builder.Services.AddTransient<ISkillService, SkillService>();

        builder.Services.AddTransient<IProjectInterface, ProjctRepository>();
        builder.Services.AddTransient<IProjectService, ProjectService>();

        builder.Services.AddTransient<IEducationInterface, EducationRepository>();
        builder.Services.AddTransient<IEducationService, EducationService>();

        builder.Services.AddTransient<IApplyInterface, ApplyRepository>();
        builder.Services.AddTransient<IApplyService, ApplyService>();

        builder.Services.AddTransient<IJobInterface, JobRepository>();
        builder.Services.AddTransient<IJobService, JobService>();

        builder.Services.AddTransient<ILanguageInterface, LanguageRepository>();
        builder.Services.AddTransient<ILanguageService, LanguageService>();

        builder.Services.AddTransient<IWorkExperienceInterface, WorkExperienceRepository>();
        builder.Services.AddTransient<IWorkExperienceService, WorkExperienceService>();

        builder.Services.AddTransient<ILinkInterface, LinkRepository>();
        builder.Services.AddTransient<ILinkService, LinkService>();

        builder.Services.AddTransient<IUserInterface, UserRepository>();
        builder.Services.AddTransient<IUserService, UserService>();
        #endregion

        #region Add AutoMapper
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AutoMapperProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);
        #endregion

        builder.Services.AddEndpointsApiExplorer();

        #region Swaggerni sozlash
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "OneApply", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
        #endregion

        #region CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(CORS_POLICY,
                builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
        });
        #endregion


    }
    public static void AddMiddleware(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors(CORS_POLICY);
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

    }
}
