using Microsoft.AspNetCore.Builder;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ICan.Core.Service;
using ICan.Core.IService;

namespace ICan.Core.StartExtesion
{
    public static class AutoFacExtesion
    {
        public static void AutoFacRegister(this ConfigureHostBuilder host)
        {
            host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterType<MenuService>().As<IMenuService>();
                builder.RegisterType<UserService>().As<IUserService>();
                builder.RegisterType<JWTService>().As<IJWTService>();
            });
        }
    }
}
