using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MyVeryFirstApplication.Database;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Input;

namespace MyVeryFirstApplication
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Product> products { get; private set; } 
        private Calls API;
        private string CurrentFilter;

        public MainPage(string sender = null)
        {
            this.CurrentFilter = sender;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            API = new Calls();
            products = new ObservableCollection<Product>();
            BindingContext = this;
            ChangeBackground();
        }

        
        protected override async void OnAppearing()
        {
            var importedProducts = await ImportInventory();
            foreach (Product item in importedProducts)
            {
                if (item.category == CurrentFilter || "all" == CurrentFilter)
                {
                    products.Add(item);
                }
            }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    var importedProducts = await ImportInventory();
                    foreach (Product item in importedProducts)
                    {
                        if (item.category == CurrentFilter || "all" == CurrentFilter)
                        {
                            products.Add(item);
                        }
                    }

                    IsRefreshing = false;
                });
            }
        }

        private async void ImportCatagoryInventory()
        {
            ObservableCollection<Product> unfilteredRes = await API.RefreshDataAsync();
            ObservableCollection<Product> result = new ObservableCollection<Product>();
            if (result.Count == 0)
            {
                await DisplayAlert("No items found", "No items found, please add products or check your internet connection.", "ok");
            }
            else
            {
                products.Clear();
                foreach (Product item in unfilteredRes)
                {
                    if (item.category == CurrentFilter || "all" == CurrentFilter)
                    {
                        products.Add(item);
                    }
                }
            }
        }

        private async Task<ObservableCollection<Product>> ImportInventory()
        {
            ObservableCollection<Product> result = await API.RefreshDataAsync();
            if(result.Count == 0)
            {
                await DisplayAlert("No items found", "No items found, please add products or check your internet connection.", "ok");
            }
            else
            {
                products.Clear();
            }
            return result;
        }


        public async void OnDelete(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete item", "Are you sure you want to delete that item?", "Yes", "No");
            if (answer)
            {
                var mi = ((MenuItem)sender);
                int id = Int32.Parse(mi.CommandParameter.ToString());
               
                var result = await API.Deleteitem(id);
                if (result)
                {
                    Product listitem = (from itm in products
                                        where itm.id == id
                                        select itm).FirstOrDefault<Product>();
                    products.Remove(listitem);
                    await DisplayAlert("Item deleted", "The item was succesfully deleted!", "ok");
                }
                else
                {
                    await DisplayAlert("Failed to delete item", "The item was not deleted, try again later.", "ok");
                }

            }
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });
            return true;
        }

        //TODO:  importInventory is hard coded now 
        ObservableCollection<Product> createInventory()
        {
            ObservableCollection<Product> shopItems = new ObservableCollection<Product>();
            //for now adds hard coded items
            int X = 5;
            while (X > 0) {
                X--;
                shopItems.Add(new Product
                {
                    name = "melk",
                    description = "zeer lekkere melk",
                    expires = DateTime.Now.AddDays(4),
                    unit = "3 Liter",
                    category = "zuivel"
                });

                shopItems.Add(new Product
                {
                    name = "Boter",
                    description = "zeer lekkere Boter",
                    expires = DateTime.Now.AddDays(2),
                    unit = "400 gram",
                    category = "zuivel"
                });

                shopItems.Add(new Product
                {
                    name = "Albert Hein 1 liter cola koolzuurvrij",
                    description = "zeer lekkere Ham",
                    expires = DateTime.Now.AddDays(3),
                    unit = "100 gram",
                    category = "Vleeswaren"
                });

                shopItems.Add(new Product
                {
                    name = "Brood",
                    description = "zeer lekkere Broodaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                    expires = DateTime.Now.AddDays(6),
                    unit = "1 stuk",
                    category = "Brood"
                });

                shopItems.Add(new Product
                {
                    name = "Banaan",
                    description = "zeer lekkere Banaan",
                    expires = DateTime.Now.AddDays(6),
                    unit = "5 stuks",
                    category = "Fruit"
                });
            }
            return shopItems;
        }
        private void ChangeBackground()
        {
            switch (CurrentFilter)
            {
                case "sauce":
                    this.FindByName<Image>("BackgroundImage").Source = "screen56_sauce.png";
                    break;
                case "dairy":
                    this.FindByName<Image>("BackgroundImage").Source = "screen53_dairy.png";
                    break;
                case "meat":
                    this.FindByName<Image>("BackgroundImage").Source = "screen54_meatfish.png";
                    break;
                case "spread":
                    this.FindByName<Image>("BackgroundImage").Source = "screen55_spreads.png";
                    break;
                case "other":
                    this.FindByName<Image>("BackgroundImage").Source = "screen58_other.png";
                    break;
                case "drink":
                    this.FindByName<Image>("BackgroundImage").Source = "screen57_drinks.png";
                    break;
                case "vegetable":
                    this.FindByName<Image>("BackgroundImage").Source = "Screen52_vegetables.png";
                    break;
                case "fruit":
                    this.FindByName<Image>("BackgroundImage").Source = "screen51_fruit.png";
                    break;


            }
        }

    }
}
