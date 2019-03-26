using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LinkMobile.Views.CustomControllers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationButton : ContentView
    {
        private Color _backgroundColor;

        public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(NavigationButton));

        public static readonly BindableProperty SizeProperty =
            BindableProperty.Create(nameof(Size), typeof(int), typeof(NavigationButton), 0);

        public static readonly BindableProperty ResourceProperty =
            BindableProperty.Create(nameof(Resource), typeof(string), typeof(NavigationButton));

        public static readonly BindableProperty ClickColorProperty =
            BindableProperty.Create(nameof(ClickColor), typeof(string), typeof(NavigationButton), "#DCDCDC");

        public ICommand TapCommand
        {
            get { return (ICommand)GetValue(TapCommandProperty); }
            set { SetValue(TapCommandProperty, value); }
        }

        public int Size
        {
            get { return (int)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public string Resource
        {
            get { return (string)GetValue(ResourceProperty); }
            set { SetValue(ResourceProperty, value); }
        }

        public string ClickColor
        {
            get { return (string)GetValue(ClickColorProperty); }
            set { SetValue(ClickColorProperty, value); }
        }

        public NavigationButton()
        {
            InitializeComponent();
            _backgroundColor = BackgroundColor;
        }

        private void Button_Pressed(object sender, System.EventArgs e)
        {
            BackgroundColor = Color.FromHex(ClickColor);
        }

        private void Button_Released(object sender, System.EventArgs e)
        {
            BackgroundColor = _backgroundColor;
        }
    }
}