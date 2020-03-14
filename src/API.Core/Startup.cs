using System;
using System.IO;
using System.Reflection;
using API.Core.AOP;
using API.Core.Common.Helper;
using API.Core.Common.MemoryCache;
using API.Core.Common.Redis;
using API.Core.Extensions;
using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API.Core
{
    public class Startup
    {
        public string ApiName { get; set; } = "API.Core";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // ����ע��
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(new Appsettings(Env.ContentRootPath));
            services.AddScoped<ICaching, MemoryCaching>();
            // Redis
            services.AddScoped<IRedisCacheManager, RedisCacheManager>();
            // ��Ӷ���ӳ��
            services.AddAutoMapperSetup();
            services.AddSwaggerSetup();
        }

       
        public void ConfigureContainer(ContainerBuilder builder)
        {

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            #region ע��������
            builder.RegisterType<BlogCacheAOP>();
            builder.RegisterType<BlogLogAOP>();
            #endregion


            //ע��Ҫͨ�����䴴�������

            // ע������
            var servicesDllFile = Path.Combine(basePath, "API.Core.Services.dll");
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);

            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      .EnableInterfaceInterceptors()
                      .InterceptedBy(typeof(BlogLogAOP));

            // ע��ִ�
            var repositoryDllFile = Path.Combine(basePath, "API.Core.Repository.dll");
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

        }

        // Configure �ɽ��м�������ӵ�IApplicationBuilder ʵ������������ܵ�
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                // �ڿ��������У�ʹ���쳣ҳ�棬�������Ա�¶�����ջ��Ϣ�����Բ�Ҫ��������������
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Error");
            

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{ApiName} V1");

                //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ����������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "doc";
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
