using Autofac;
using Autofac.Extensions.DependencyInjection;
using CTFPlatForm.Infrastructure.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SqlSugar;
using System.Text;

namespace CTFPlatForm
{
    /// <summary>
    /// 应用程序启动类
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 应用程序入口点
        /// </summary>
        /// <param name="args">命令行参数</param>
        public static void Main(string[] args)
        {
            // 创建 WebApplicationBuilder 实例
            var builder = WebApplication.CreateBuilder(args);

            #region 依赖注入容器配置

            // 使用 Autofac 作为 DI 容器
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            // 配置 Autofac 容器
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule(new ServiceModule());
            });

            #endregion

            #region 配置文件加载

            // 在开发环境中使用嵌入式配置，在生产环境中直接使用 appsettings.json
            var assembly = typeof(CTFPlatForm.Infrastructure.Tools.ConfigurationBuilderExtensions).Assembly;
            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration
                    .AddEmbeddedJsonFile(assembly, "Configuration.JWT.json")
                    .AddEmbeddedJsonFile(assembly, "Configuration.Database.json");
            }

            #endregion

            #region CORS 跨域资源共享配置

            // 添加 CORS 服务
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVueApp", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5001", "http://localhost:5001")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            #endregion

            #region MVC 控制器配置

            // 添加控制器服务
            builder.Services.AddControllers();

            #endregion

            #region JWT 认证配置

            // 添加授权服务
            builder.Services.AddAuthorization();

            // 添加认证服务
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = bool.Parse(builder.Configuration["JWTSettings:ValidateIssuerSigningKey"]),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:IssuerSigningKey"])),
                    ValidateIssuer = bool.Parse(builder.Configuration["JWTSettings:ValidateIssuer"]),
                    ValidIssuer = builder.Configuration["JWTSettings:ValidIssuer"],
                    ValidateAudience = bool.Parse(builder.Configuration["JWTSettings:ValidateAudience"]),
                    ValidAudience = builder.Configuration["JWTSettings:ValidAudience"],
                    ValidateLifetime = bool.Parse(builder.Configuration["JWTSettings:ValidateLifetime"]),
                    ClockSkew = TimeSpan.FromSeconds(int.Parse(builder.Configuration["JWTSettings:ClockSkew"])),
                    RequireExpirationTime = bool.Parse(builder.Configuration["JWTSettings:RequireExpirationTime"])
                };

                // 禁用默认的claim类型映射
                options.MapInboundClaims = false;
            });

            #endregion

            #region 数据库配置

            // 配置 SqlSugar 数据库客户端
            builder.Services.AddScoped<ISqlSugarClient>(provider =>
            {
                var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
                return new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = connectionString,
                    DbType = DbType.MySql, // 根据你的数据库类型修改，如 MySql、Sqlite 等
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute // 使用实体类属性映射字段
                });
            });

            #endregion

            #region API 文档配置

            // 添加 API 探索器
            builder.Services.AddEndpointsApiExplorer();

            // 配置 Swagger 文档生成
            builder.Services.AddSwaggerGen(s =>
            {
                // 开启 Swagger 文档注释支持
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "CTFPlatForm.Api.xml");
                s.IncludeXmlComments(xmlPath, true);
            });

            #endregion

            #region 应用管道配置

            // 构建 WebApplication 实例
            var app = builder.Build();

            // 开发环境配置
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // 开发环境允许所有来源（宽松策略）
                app.UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            }
            else
            {
                // 生产环境使用指定策略
                app.UseCors("AllowVueApp");
            }

            // 配置静态文件中间件，确保可以访问上传的文件
            app.UseStaticFiles();

            // 启用认证和授权中间件
            app.UseAuthentication();
            app.UseAuthorization();

            // 映射控制器路由
            app.MapControllers();

            #endregion

            // 启动应用程序
            app.Run();
        }
    }
}