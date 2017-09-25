//using Autofac;
//using Autofac.Integration.Mvc;
using HLX.ZSZ.CommonMVC;
using HLX.ZSZ.FrontWeb.App_Start;
using HLX.ZSZ.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HLX.ZSZ.FrontWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //log4net.Config.XmlConfigurator.Configure();
            GlobalFilters.Filters.Add(new ZSZExceptionFilter());
            ModelBinders.Binders.Add(typeof(string), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(int), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(long), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(double), new TrimToDBCModelBinder());

            //var builder = new ContainerBuilder();
            //builder.RegisterControllers(typeof(MvcApplication).Assembly)
            //    .PropertiesAutowired();//把当前程序集中的Controller都注册                                                                                               //不要忘了.PropertiesAutowired()
            //                           // 获取所有相关类库的程序集
            //Assembly[] assemblies = new Assembly[] { Assembly.Load("ZSZ.Service") };
            //builder.RegisterAssemblyTypes(assemblies)
            //.Where(type => !type.IsAbstract
            //        && typeof(IServiceSupport).IsAssignableFrom(type))
            //        .AsImplementedInterfaces().PropertiesAutowired();
            //Assign：赋值


            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
