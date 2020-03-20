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
            var ApiName = Appsettings.app(new string[] { "Startup", "ApiName" });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = $"{ApiName} 接口文档——Netcore 3.1",
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
