using Autofac;
using LinkMobile.Services;
using LinkMobile.Services.Interfaces;
using LinkMobile.Services.Interfaces.Persistence;
using LinkMobile.Services.Persistence;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace LinkMobile.ViewModels.Base
{
    public class ViewModelLocator : Module
    {
        private static IContainer _container;
        private static List<Type> _viewModels;
        private static Dictionary<Type, Type> _serviceInstances;

        static ViewModelLocator()
        {
            _viewModels = new List<Type>
            {
                typeof(HomePageViewModel),
                typeof(MenuPageViewModel),
                typeof(ReservationViewModel),
                typeof(TrackingViewModel),
                typeof(GoogleViewModel),
                typeof(FacebookViewModel),
                typeof(LoginViewModel)

            };

            _serviceInstances = new Dictionary<Type, Type>
            {
                { typeof(IMasterNavigationService), typeof(MasterNavigationService) },
                { typeof(IPageService), typeof(PageService) },
                { typeof(IReservationService), typeof(ReservationService) },
                { typeof(IPositionService), typeof(PositionService) },
                { typeof(INetworkService), typeof(NetworkService) },
                { typeof(IUserService), typeof(UserService) },
                { typeof(IPersistenceService), typeof(PersistenceService) }
            };
        }

        public static void Initialize()
        {
            if (_container == null)
            {
                var builder = new ContainerBuilder();

                _serviceInstances.ForEach(si => builder.RegisterType(si.Value).As(si.Key).SingleInstance());
                _viewModels.ForEach(vm => builder.RegisterType(vm));

                _container = builder.Build();
            }
            else
            {
                //already init. Do nothing
            }
               

        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public static void RegisterService<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _serviceInstances[typeof(TInterface)] = typeof(T);
        }
    }
}
