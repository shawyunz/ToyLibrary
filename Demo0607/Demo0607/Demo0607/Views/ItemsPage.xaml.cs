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

            var grid1 = new Grid
            {
                ColumnSpacing = 0,
                RowSpacing = 0
            };
            var grid2 = new Grid
            {
                ColumnSpacing = 0,
                RowSpacing = 0
            };

            AddItemLayout(grid1, "sp01.jpg", "test0001", "descsc", "", 0, 0);
            AddItemLayout(grid1, "sp03.jpg", "test0003333", "dedededsc", "dedededsc", 2, 3);

            grid2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(_smallheight) });
            //grid2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

            var imageButton = new ImageButton
            {
                Source = "asd",
                BackgroundColor = Color.Pink
            };
            grid2.Children.Add (imageButton, 0, 0);
            
            AddItemLayout(grid2, "sp02.jpg", "test002222", "qqqqqqc", "", 1, 1);
            
            grid0.Children.Add (grid1, 0, 0);
            grid0.Children.Add (grid2, 1, 0);
            //RootLayout.Children.Add (grid0);
            RootLayout.Content = grid0;

        }

        private void AddItemLayout(Grid grid1, string v1, string v2, string v3, string v4, int index, int column)
        {
            grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(130) });
            //grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });


            var image1 = new Image { Source = v1, Aspect = Aspect.AspectFill };
            var image2 = new Image { Source = "triangle2.png", HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.End, Margin = new Thickness(30, 0, 0, 0), WidthRequest = 24, HeightRequest = 12 };
            var entry1 = new Label { Text = v2, FontSize = 17, FontAttributes = FontAttributes.Bold, Margin = new Thickness(10, 8, 0, 0) };
            var entry2 = new Label { Text = v3, FontSize = 13, Margin = new Thickness(10, 6, 0, 0) };
            grid1.Children.Add(image1, 0, column);
            grid1.Children.Add(image2, 0, column);
            grid1.Children.Add(entry1, 0, column + 1);
            grid1.Children.Add(entry2, 0, column + 2);

            #region click grsture area
            var clickable = new ContentView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Opacity = 0.6
            };

            var tap = new TapGestureRecognizer();
            tap.Tapped += async (object sender, EventArgs e) => {
                var view = (ContentView)sender;
                view.BackgroundColor = Color.Azure;
                await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(viewModel.ToyListing[index])));
                view.BackgroundColor = Color.Transparent;
            };
            clickable.GestureRecognizers.Add(tap);

            grid1.Children.Add(clickable, 0, column);

            Grid.SetRowSpan(clickable, 3);
            #endregion

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Toy;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            //ItemsListView.SelectedItem = null;
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