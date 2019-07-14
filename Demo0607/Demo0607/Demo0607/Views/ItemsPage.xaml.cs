using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Demo0607.Models;
using Demo0607.Views;
using Demo0607.ViewModels;

namespace Demo0607.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();

            double _width = 160;
            double _smallheight = 60;
            double _bigheight = 130;


            var grid0 = new Grid { ColumnSpacing = 0, HorizontalOptions = LayoutOptions.FillAndExpand };
            grid0.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid0.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


            var grid1 = new Grid { HorizontalOptions = LayoutOptions.CenterAndExpand };
            grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(_bigheight) });
            grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(_smallheight) });
            grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(_bigheight) });
            grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(_smallheight) });


            var image1 = new Image { Source = "sp01.jpg" };
            var entry1 = new Label { Text = "test0001" };
            grid1.Children.Add(image1, 0, 0);
            grid1.Children.Add(entry1, 0, 1);

            //AddItemLayout(grid1, "sp01.jpg", "test0001", "", "");


            var image3 = new Image { Source = "sp03.jpg" };
            var entry3 = new Label { Text = "test0003" };
            grid1.Children.Add(image3, 0, 2);
            grid1.Children.Add(entry3, 0, 3);


            var image99 = new Image { Source = "triangle2.png", HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.End, Margin = new Thickness(30, 0, 0, 0), WidthRequest = 24, HeightRequest = 12 };
            grid1.Children.Add(image99, 0, 0);
            var image88 = new Image { Source = "triangle2.png", HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.End, Margin = new Thickness(30, 0, 0, 0), WidthRequest = 24, HeightRequest = 12 };
            grid1.Children.Add(image88, 0, 2);


            var grid2 = new Grid { HorizontalOptions = LayoutOptions.CenterAndExpand };
            grid2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(_smallheight) });
            grid2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            grid2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(_smallheight) });
            //grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(_width) });

            var imageButton = new ImageButton
            {
                Source = "asd",
                BackgroundColor = Color.Pink
            };
            grid2.Children.Add (imageButton, 0, 0);

            var image2 = new Image { Source = "sp02.jpg" };
            var entry2 = new Label { Text = "test0002" };

            grid2.Children.Add(image2, 0, 1);
            grid2.Children.Add(entry2, 0, 2);

            grid0.Children.Add (grid1, 0, 0);
            grid0.Children.Add (grid2, 1, 0);
            RootLayout.Children.Add (grid0);

        }

        private void AddItemLayout(Grid grid1, string v1, string v2, string v3, string v4)
        {
            //grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(130) });
            //grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });

            ////triangle2.png

            //var image1 = new Image { Source = v1 };
            //var image2 = new Image { Source = "triangle2.png", VerticalOptions = LayoutOptions.End, Margin = new Thickness (30, 0, 0, 0) };
            //var entry1 = new Label { Text = v2 };
            //grid1.Children.Add(image1, 0, 0);
            //grid1.Children.Add(image2, 0, 0);
            //grid1.Children.Add(entry1, 0, 1);

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Toy;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ItemAddPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.ToyListing.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}