using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using PickleApplication.BusinessLayer.Services;
using PickleApplication.DataLayer.Repository;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PickleApplication.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

             var builder = new ContainerBuilder();
            var constr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"];
            IDbConnection conn = new SqlConnection(constr.ConnectionString);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //Inject Repository and service
            builder.RegisterInstance(conn);
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<LinkRepository>().As<ILinkRepository>();
            builder.RegisterType<LinkRepository>().As<ILinkRepository>();
            builder.RegisterType<CityRepository>().As<ICityRepository>();
            builder.RegisterType<CountryRepository>().As<ICountryRepository>();
            builder.RegisterType<RegionRepository>().As<IRegionRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<OrderLineRepository>().As<IOrderLineRepository>();
            builder.RegisterType<ContentRepository>().As<IContentRepository>();
            builder.RegisterType<PictureRepository>().As<IPictureRepository>();
            builder.RegisterType<DeliveryInfoRepository>().As<IDeliveryInfoRepository>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<UserReopository>().As<IUserReopository>();

            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<LinkService>().As<ILinkService>();
            builder.RegisterType<LinkService>().As<ILinkService>();
            builder.RegisterType<CityService>().As<ICityService>();
            builder.RegisterType<CountryService>().As<ICountryService>();
            builder.RegisterType<RegionService>().As<IRegionService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<OrderLineService>().As<IOrderLineService>();
            builder.RegisterType<ContentService>().As<IContentService>();
            builder.RegisterType<PictureService>().As<IPictureService>();
            builder.RegisterType<DeliveryInfoService>().As<IDeliveryInfoService>();
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterType<ApiController>().InstancePerRequest();
            IContainer container = builder.Build();

            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
            //Api CamelCase Response
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}
