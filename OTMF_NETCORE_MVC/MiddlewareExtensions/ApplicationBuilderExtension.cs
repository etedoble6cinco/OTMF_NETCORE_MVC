using OTMF_NETCORE_MVC.SuscribeTableDependencies;

namespace OTMF_NETCORE_MVC.MiddlewareExtensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseProductTableDependency(this IApplicationBuilder applicationBuilder)
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            var service = serviceProvider.GetService<SuscribeOrdenTrabajoTableDependecy>();
            service.SuscribeTableDependecy();
        }
    }
}
