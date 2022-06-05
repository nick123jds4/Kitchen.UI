using Autofac;
using Kitchen.UI.Data;
using Kitchen.UI.Data.Lookups;
using Kitchen.UI.Data.Repositories;
using Kitchen.UI.View;
using Kitchen.UI.View.Services;
using Kitchen.UI.ViewModel;
using Prism.Events;

namespace Kitchen.UI.Startup
{
    public class Bootstraper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            //prism
            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<ClientRepository>().As<IClientRepository>();
            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<MeetingRepository>().As<IMeetingRepository>();

            builder.RegisterType<FriendDetailViewModel>().Keyed<IDetailViewModel>(nameof(FriendDetailViewModel));
            builder.RegisterType<MeetingDetailViewModel>().Keyed<IDetailViewModel>(nameof(MeetingDetailViewModel));
            return builder.Build();
        }
    }
}
