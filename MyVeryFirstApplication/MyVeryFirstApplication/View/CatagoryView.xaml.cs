using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyVeryFirstApplication.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatagoryView : ContentPage
    {
        public CatagoryView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void CategoryClicked(object sender, EventArgs e)
        {

            string catagory = ((sender as Button).CommandParameter).ToString(); ;
            Application.Current.MainPage.Navigation.PushAsync(new MainPage(catagory)); ;
        }
    }
}