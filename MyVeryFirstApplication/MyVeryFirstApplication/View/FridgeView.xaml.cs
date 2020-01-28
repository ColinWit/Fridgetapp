using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyVeryFirstApplication.Database;
using MyVeryFirstApplication.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace MyVeryFirstApplication.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FridgeView : ContentPage
    {
        //private Calls API;
        //StackLayout layout = new StackLayout();
        public FridgeView()
        {
            //StackLayout death =  AddFridgesToScreen();
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //API = new Calls();
        }
        /*
        private void onFridgeChosen(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
        private StackLayout AddFridgesToScreen()
        {
            ObservableCollection<Fridge> fridges = Importfridges();

            foreach (Fridge fridge in fridges)
            {
                //source dynamicly adding buttons: https://stackoverflow.com/questions/45229706/adding-buttons-dynamically-in-stacklayout
                Button newButton = new Button
                {
                    Text = fridge.name,
                    Command = new Command(() => {
                        Application.Current.MainPage = new MainPage();
                    }),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                newButton.SetBinding(Button.CommandParameterProperty, new Binding("ViewModelProperty"));

                //layout.Children.Add(newButton);
            }
            return layout;
        }*/

        private ObservableCollection<Fridge> Importfridges()
        {
            return new ObservableCollection<Fridge>
            {
                new Fridge
                {
                    name = "bob",
                    id = 1
                },
                new Fridge
                {
                    name = "sara",
                    id = 2
                },
                new Fridge
                {
                    name = "henk",
                    id = 3
                },
            };

        }

        private void UserClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new CatagoryView());
        }
    }
}
      