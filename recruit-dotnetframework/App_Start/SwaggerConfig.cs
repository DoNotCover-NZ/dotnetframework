using recruit_dotnetframework;
using Swashbuckle.Application;
using System.Web;
using System.Web.Http;
using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace recruit_dotnetframework
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c => {
                    c.SingleApiVersion("v1", "Recruit API");
                })
                .EnableSwaggerUi();
        }
    }
}
