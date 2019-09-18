using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LinkMobile.Views.CustomControllers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationItem : Grid
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(NavigationItem));

        public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(NavigationItem));

        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Resource), typeof(string), typeof(NavigationItem));

        public string Resource
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public ICommand TapCommand
        {
            get { return (ICommand)GetValue(TapCommandProperty); }
            set { SetValue(TapCommandProperty, value); }
        }

        public NavigationItem()
        {
            InitializeComponent();
        }
    }
}