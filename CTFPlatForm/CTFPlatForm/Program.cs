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
    /// Ӧ�ó���������
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Ӧ�ó�����ڵ�
        /// </summary>
        /// <param name="args">�����в���</param>
        public static void Main(string[] args)
        {
            // ���� WebApplicationBuilder ʵ��
            var builder = WebApplication.CreateBuilder(args);

            #region ����ע����������

            // ʹ�� Autofac ��Ϊ DI ����
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            // ���� Autofac ����
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule(new ServiceModule());
            });

            #endregion

            #region �����ļ�����

            // �ڿ���������ʹ��Ƕ��ʽ���ã�������������ֱ��ʹ�� appsettings.json
            var assembly = typeof(CTFPlatForm.Infrastructure.Tools.ConfigurationBuilderExtensions).Assembly;
            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration
                    .AddEmbeddedJsonFile(assembly, "Configuration.JWT.json")
                    .AddEmbeddedJsonFile(assembly, "Configuration.Database.json");
            }

            #endregion

            #region CORS ������Դ��������

            // ��� CORS ����
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

            #region MVC ����������

            // ��ӿ���������
            builder.Services.AddControllers();

            #endregion

            #region JWT ��֤����

            // �����Ȩ����
            builder.Services.AddAuthorization();

            // �����֤����
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

                // ����Ĭ�ϵ�claim����ӳ��
                options.MapInboundClaims = false;
            });

            #endregion

            #region ���ݿ�����

            // ���� SqlSugar ���ݿ�ͻ���
            builder.Services.AddScoped<ISqlSugarClient>(provider =>
            {
                var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
                return new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = connectionString,
                    DbType = DbType.MySql, // ����������ݿ������޸ģ��� MySql��Sqlite ��
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute // ʹ��ʵ��������ӳ���ֶ�
                });
            });

            #endregion

            #region API �ĵ�����

            // ��� API ̽����
            builder.Services.AddEndpointsApiExplorer();

            // ���� Swagger �ĵ�����
            builder.Services.AddSwaggerGen(s =>
            {
                // ���� Swagger �ĵ�ע��֧��
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "CTFPlatForm.Api.xml");
                s.IncludeXmlComments(xmlPath, true);
            });

            #endregion

            #region Ӧ�ùܵ�����

            // ���� WebApplication ʵ��
            var app = builder.Build();

            // ������������
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // ������������������Դ�����ɲ��ԣ�
                app.UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            }
            else
            {
                // ��������ʹ��ָ������
                app.UseCors("AllowVueApp");
            }

            // ���þ�̬�ļ��м����ȷ�����Է����ϴ����ļ�
            app.UseStaticFiles();

            // ������֤����Ȩ�м��
            app.UseAuthentication();
            app.UseAuthorization();

            // ӳ�������·��
            app.MapControllers();

            #endregion

            // ����Ӧ�ó���
            app.Run();
        }
    }
}