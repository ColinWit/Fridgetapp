using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyVeryFirstApplication.View;


namespace MyVeryFirstApplication
{

    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            var navPage = new NavigationPage(new FridgeView());
            Application.Current.MainPage = navPage;

            //MainPage = new MainPage();
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
