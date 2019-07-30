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
using System.Collections.ObjectModel;

namespace Demo0607.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
        int _index = 0;
        int _interval = 8;


        static Button LoadMoreBtn = new Button
        {
            Text = "Load More...",
            //BackgroundColor = Color.FromRgb(252, 179, 78)
                BackgroundColor = Color.FromHex("#ff77D065")
        };

        static Button AddBtn = new Button
        {
            Text = "Add...",
            //BackgroundColor = Color.FromRgb(252, 179, 78)
                BackgroundColor = Color.FromHex("#ff77D065")
        };

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();

            LoadMoreBtn.Clicked += LoadMoreBtn_Clicked;
            AddBtn.Clicked += AddItem_Clicked;


            MessagingCenter.Subscribe<ItemsViewModel, ObservableCollection<Toy>>(this, "AddList", async (obj, items) =>
            {

                AddListingToPage(items);
                
            });


            //double _width = 160;
            //double _smallheight = 60;
            //double _bigheight = 130;
        }

        private void LoadMoreBtn_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Btn", "Load more ...", "Cancel");
        }

        private void InitPageUI()
        {
            _index = 0;

            var GridLeft = new Grid
            {
                ColumnSpacing = 0,
                RowSpacing = 0,
                BackgroundColor = Color.Azure
            };

            var GridRight = new Grid
            {
                ColumnSpacing = 0,
                RowSpacing = 0,
                BackgroundColor = Color.Azure
            };

            //add first button on the right
            GridRight.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });
            GridRight.Children.Add(AddBtn, 0, 0);

            GridRoot.Children.Clear();
            GridRoot.Children.Add(GridLeft, 0, 0);
            GridRoot.Children.Add(GridRight, 1, 0);
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

            //1. fetch data
            //if (viewModel.ToyListing.Count == 0)
            viewModel.LoadItemsCommand.Execute(null);
            
            //2. initialise UI
            InitPageUI();

            //3. add elements into grid
            //AddListingToPage(viewModel.ToyListing);

        }

        private void AddListingToPage(ObservableCollection<Toy> toyListing)
        {
            foreach (var toy in toyListing)
            {
                AddItemLayout(toy, _index);
                if (_index == toyListing.Count - 1)
                {
                    //add bottome button when listing complete
                    Grid _gridLeft = (Grid)GridRoot.Children.ElementAt(0);
                    _gridLeft.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });
                    _gridLeft.Children.Add(LoadMoreBtn, 0, _index / 2 * 3 + 3);
                    return;
                }
                _index++;
            }
        }

        private void AddItemLayout(Toy SelectedToy, int index)
        {
            int _baseIndex = index / 2;
            var _flagRight = Convert.ToBoolean(index % 2);

            var _grid = (Grid)(_flagRight ? GridRoot.Children.ElementAt(1) : GridRoot.Children.ElementAt(0));
            var _rowIndex = _flagRight ? _baseIndex * 3 + 1 : _baseIndex * 3;

            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(130) });
            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });

            #region Label background
            var _LabelBackgroundView = new ContentView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Opacity = 0.8,
                BackgroundColor = Color.FromRgb(48, 164, 220)
            };

            _grid.Children.Add(_LabelBackgroundView, 0, _rowIndex + 1);
            Grid.SetRowSpan(_LabelBackgroundView, 2);
            #endregion

            var image1 = new Image { Source = SelectedToy.Image, Aspect = Aspect.AspectFill };
            var image2 = new Image { Source = "triangle3.png", Opacity = 0.6, HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.End, Margin = new Thickness(30, 0, 0, 0), WidthRequest = 24, HeightRequest = 12 };
            var entry1 = new Label {
                Text = SelectedToy.Name,
                FontSize = 17,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(10, 8, 0, 0)
            };
            var entry2 = new Label {
                Text = "Pc: " + SelectedToy.Pieces + "        Age: " + SelectedToy.Age,
                FontSize = 13,
                Margin = new Thickness(10, 6, 0, 0)
            };
            _grid.Children.Add(image1, 0, _rowIndex);
            _grid.Children.Add(image2, 0, _rowIndex);
            _grid.Children.Add(entry1, 0, _rowIndex + 1);
            _grid.Children.Add(entry2, 0, _rowIndex + 2);

            #region click grsture area
            var clickable = new ContentView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Opacity = 0.6
            };

            var tap = new TapGestureRecognizer();
            tap.Tapped += async (object sender, EventArgs e) =>
            {
                var view = (ContentView)sender;
                view.BackgroundColor = Color.Azure;
                await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(viewModel.ToyListing[index])));
                view.BackgroundColor = Color.Transparent;
            };
            clickable.GestureRecognizers.Add(tap);

            _grid.Children.Add(clickable, 0, _rowIndex);
            Grid.SetRowSpan(clickable, 3);
            #endregion
        }
    }
}