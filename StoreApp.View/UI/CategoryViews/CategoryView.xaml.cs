using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.View.UI.MainViews;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static StoreApp.View.UI.StoreViews.StoreView;

namespace StoreApp.View.UI.CategoryViews
{
    /// <summary>
    /// Логика взаимодействия для CategoryView.xaml
    /// </summary>
    public partial class CategoryView : UserControl
    {
        StoreMainView StoremainView { get; set; }
        ICategoryService categoryService { get; set; }

        public CategoryView()
        {
            InitializeComponent();
        }

        public void GetMainView(StoreMainView storeMainView)
        {
            StoremainView = storeMainView;

        }

        public async void WindowLoad()
        {
            if (panel.Children.Count > 0)
            {
                panel.Children.Clear();
            }

            categoryService = new CategoryService();
            var categories = await categoryService.GetAll();

            Border borderAdd = new Border
            {
                Background = Brushes.White,
                Width = 250,
                Height = 150,
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(10, 10, 0, 0),
                CornerRadius = new CornerRadius(10),
            };

            MyButton buttonAdd = new MyButton
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Content = "+ Добавить",
                FontWeight = FontWeights.Bold,
                FontSize = 25
            };
            buttonAdd.Click += new RoutedEventHandler(Button_Click);

            borderAdd.Child = buttonAdd;

            panel.Children.Add(borderAdd);

            foreach (var item in categories)
            {

                TotalInfo totalInfo = new TotalInfo()
                {
                    Id = item.Id,
                    Name = item.Name,

                };

                Border border = new Border
                {
                    Background = Brushes.White,
                    Width = 250,
                    Height = 150,
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(10, 10, 0, 0),
                    CornerRadius = new CornerRadius(10)
                };

                ColumnDefinition c1 = new ColumnDefinition
                {
                    Width = new GridLength(200, GridUnitType.Star)
                };

                ColumnDefinition c2 = new ColumnDefinition
                {
                    Width = new GridLength(50, GridUnitType.Star)
                };

                MyButton button = new MyButton
                {
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.Transparent,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 200,
                    Height = 150,
                    Content = new TextBlock
                    {
                        Text = totalInfo.Name,
                        FontSize = 25,
                        FontWeight = FontWeights.Bold,
                        TextWrapping = TextWrapping.Wrap
                    }
                };
                button.Totalinfo = totalInfo;
                button.Click += new RoutedEventHandler(btnEnter_Click);

                Grid grid = new Grid
                {
                    ColumnDefinitions = { c1, c2 },
                    Children = { button }
                };


                MyButton btnDelete = new MyButton
                {
                    Width = 40,
                    Height = 35,
                    Background = Brushes.White,
                    BorderBrush = Brushes.White,
                    Margin = new Thickness(0, 10, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    Padding = new Thickness(0),
                    Content = new Image
                    {
                        Source = new BitmapImage(new Uri("../../Images/delete.png", UriKind.Relative)),
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 20,
                        Height = 20
                    }
                };
                btnDelete.Totalinfo = totalInfo;
                btnDelete.Click += new RoutedEventHandler(btnDelete_Click);

                MyButton btnEdit = new MyButton
                {
                    Width = 40,
                    Height = 35,
                    Background = Brushes.White,
                    BorderBrush = Brushes.White,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 40, 0, 0),
                    Padding = new Thickness(0),
                    Content = new Image
                    {
                        Source = new BitmapImage(new Uri("../../Images/pencil.png", UriKind.Relative)),
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 20,
                        Height = 20

                    }

                };
                btnEdit.Totalinfo = totalInfo;
                btnEdit.Click += new RoutedEventHandler(btnEdit_Click);

                StackPanel stackPanel = new StackPanel
                {
                    Children = { btnEdit, btnDelete }
                };

                Grid.SetColumn(stackPanel, 1);
                grid.Children.Add(stackPanel);
                border.Child = grid;

                panel.Children.Add(border);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            StoremainView.txtcategoryName.Text = "";
            StoremainView.category_id.Content = "";
            StoremainView.nameCategory.Visibility = Visibility.Hidden;
            StoremainView.AllCloseControls(2);
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить", "Осторожность", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    categoryService = new CategoryService();
                    MyButton btnDelete = sender as MyButton;
                    long id = btnDelete.Totalinfo.Id;

                    if (id != 0)
                    {
                        await categoryService.Delete(id);

                        WindowLoad();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyButton btnDelete = sender as MyButton;

                long id = btnDelete.Totalinfo.Id;

                EditCategoryWindow editProductCategoryWindow = new EditCategoryWindow(this, id);
                editProductCategoryWindow.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyButton myButton = (MyButton)sender;

                StoremainView.txtcategoryName.Text = myButton.Totalinfo.Name;
                StoremainView.category_id.Content = myButton.Totalinfo.Id;
                StoremainView.nameCategory.Visibility = Visibility.Visible;
                StoremainView.AllCloseControls(2);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryWindow addProductcategory = new AddCategoryWindow(this);
            addProductcategory.ShowDialog();
        }
    }
}
