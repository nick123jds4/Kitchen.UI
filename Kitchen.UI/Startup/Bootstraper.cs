using Autofac;
using Kitchen.UI.Data;
using Kitchen.UI.ViewModel; 

namespace Kitchen.UI.Startup
{
    public class Bootstraper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();

            return builder.Build();
        }
    }
}
