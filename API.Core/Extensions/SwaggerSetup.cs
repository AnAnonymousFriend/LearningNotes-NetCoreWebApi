using API.Core.Common.Helper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core.Extensions
{

    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var basePath = AppContext.BaseDirectory;
            //var basePath2 = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            var ApiName = Appsettings.app(new string[] { "Startup", "ApiName" });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                // {ApiName} 定义成全局变量，方便修改
                Version = "V1",
                    Title = $"{ApiName} 接口文档——Netcore 3.0",
                    Description = $"{ApiName} HTTP API V1",
                    Contact = new OpenApiContact { Name = ApiName, Email = "API.Core@xxx.com" },
                    License = new OpenApiLicense { Name = ApiName }
                });
                c.OrderActionsBy(o => o.RelativePath);

            //这个就是刚刚配置的xml文件名
            var xmlPath = Path.Combine(basePath, "API.Core.xml");

            //默认的第二个参数是false，这个是controller的注释，记得修改
            c.IncludeXmlComments(xmlPath, true);

            });
        }
    }
}
