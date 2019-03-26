using Autofac;
using LinkMobile.Services;
using LinkMobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace LinkMobile.ViewModels.Base
{
    public static class ViewModelLocator
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
                typeof(TrackingViewModel)

            };

            _serviceInstances = new Dictionary<Type, Type>
            {
                { typeof(IMasterNavigationService), typeof(MasterNavigationService) },
                { typeof(IPageService), typeof(PageService) },
                { typeof(IReservationService), typeof(ReservationService) },
                { typeof(IPositionService), typeof(PositionService) }
            };
        }

        public static void Initialize()
        {
            if (_container != null)
                throw new InvalidOperationException("ViewModelLocator is already initialized");

            var builder = new ContainerBuilder();

            _serviceInstances.ForEach(si => builder.RegisterType(si.Value).As(si.Key).SingleInstance());
            _viewModels.ForEach(vm => builder.RegisterType(vm));

            _container = builder.Build();
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
