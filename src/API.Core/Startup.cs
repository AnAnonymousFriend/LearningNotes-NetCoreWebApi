using System.IO;
using System.Reflection;
using API.Core.AOP;
using API.Core.Common.Helper;
using API.Core.Common.MemoryCache;
using API.Core.Common.Redis;
using API.Core.Extensions;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API.Core
{
    public class Startup
    {
        public string ApiName { get; set; } = "API.Core";
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

      

        // 服务注入
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(new Appsettings(Env.ContentRootPath));
            services.AddScoped<ICaching, MemoryCaching>();
            // Redis
            services.AddScoped<IRedisCacheManager, RedisCacheManager>();
            // 添加对象映射
            services.AddAutoMapperSetup();
            // 添加Swagger
            services.AddSwaggerSetup();
        }

       
        public void ConfigureContainer(ContainerBuilder builder)
        {

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            #region 注册拦截器
            builder.RegisterType<LogAOP>();
            #endregion


            // 注册服务层
            var servicesDllFile = Path.Combine(basePath, "API.Core.Services.dll");
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);

            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      .EnableInterfaceInterceptors()
                      .InterceptedBy(typeof(LogAOP));

            // 注册仓储
            var repositoryDllFile = Path.Combine(basePath, "API.Core.Repository.dll");
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

        }

        // Configure 可将中间件组件添加到IApplicationBuilder 实例来配置请求管道
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                // 在开发环境中，使用异常页面，这样可以暴露错误堆栈信息，所以不要放在生产环境。
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Error");
            

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{ApiName} V1");

                // 路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的。
                // 去launchSettings.json把launchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
                c.RoutePrefix = "";
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           

        }
    
    
    
    }
}
