using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebaseApp.Views;
using XamarinFirebaseApp.Views.Reseñas;

namespace XamarinFirebaseApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            var navigationStyle = new Style(typeof(NavigationPage))
            {
                Setters = {
            new Setter { Property = NavigationPage.BarBackgroundColorProperty, Value = Color.FromHex("#14580E") }, // Cambia el color aquí
            new Setter { Property = NavigationPage.BarTextColorProperty, Value = Color.White } // Cambia el color del texto si es necesario
        }
            };
            Resources = new ResourceDictionary();
            Resources.Add(navigationStyle);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
