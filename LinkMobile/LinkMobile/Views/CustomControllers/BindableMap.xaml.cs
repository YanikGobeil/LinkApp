using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace LinkMobile.Views.CustomControllers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BindableMap : Map
	{
        public BindableMap ()
		{
			InitializeComponent ();
            RouteCoordinates = new List<Position>();
        }

        public static readonly BindableProperty MapPinsProperty = BindableProperty.Create(
             nameof(Pins),
             typeof(ObservableCollection<Pin>),
             typeof(BindableMap),
             new ObservableCollection<Pin>(),
             propertyChanged: (b, o, n) =>
             {
                 var bindable = (BindableMap)b;
                 bindable.Pins.Clear();

                 var collection = (ObservableCollection<Pin>)n;
                 foreach (var item in collection)
                     bindable.Pins.Add(item);
                 collection.CollectionChanged += (sender, e) =>
                 {
                     Device.BeginInvokeOnMainThread(() =>
                     {
                         switch (e.Action)
                         {
                             case NotifyCollectionChangedAction.Add:
                             case NotifyCollectionChangedAction.Replace:
                                 if (e.OldItems != null)
                                     foreach (var item in e.OldItems)
                                         bindable.Pins.Remove((Pin)item);
                                 if (e.NewItems != null)
                                     foreach (var item in e.NewItems)
                                         bindable.Pins.Add((Pin)item);
                                 break;
                             case NotifyCollectionChangedAction.Reset:
                                 bindable.Pins.Clear();
                                 break;
                         }
                     });
                 };
             });
        public IList<Pin> MapPins { get; set; }

        public static readonly BindableProperty MapPositionProperty = BindableProperty.Create(
                 nameof(MapPosition),
                 typeof(Position),
                 typeof(BindableMap),
                 new Position(0, 0),
                 propertyChanged: (b, o, n) =>
                 {
                     ((BindableMap)b).MoveToRegion(MapSpan.FromCenterAndRadius(
                          (Position)n,
                          Distance.FromKilometers(0.2)));
                 });

        public Position MapPosition { get; set; }

        public static readonly BindableProperty RouteCoordinatesProperty = BindableProperty.Create(
                nameof(RouteCoordinates),
                typeof(ObservableCollection<Position>),
                typeof(BindableMap),
                new ObservableCollection<Position>(),
                propertyChanged: (b, o, n) =>
                {
                    
                    var bindable = (BindableMap)b;
                    
                    var collection = (ObservableCollection<Position>)n;
                    foreach (var item in collection)
                        bindable.RouteCoordinates.Add(item);
                    collection.CollectionChanged += (sender, e) =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            switch (e.Action)
                            {
                                case NotifyCollectionChangedAction.Add:
                                case NotifyCollectionChangedAction.Replace:
                                    if (e.OldItems != null)
                                        foreach (var item in e.OldItems)
                                            bindable.RouteCoordinates.Remove((Position)item);
                                    if (e.NewItems != null)
                                        foreach (var item in e.NewItems)
                                            bindable.RouteCoordinates.Add((Position)item);
                                    break;
                                case NotifyCollectionChangedAction.Reset:
                                    bindable.RouteCoordinates.Clear();
                                    break;
                            }
                        });
                    };
                });

        public List<Position> RouteCoordinates { get; set; }

    }
}