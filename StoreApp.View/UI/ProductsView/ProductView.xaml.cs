using StoreApp.Domain.Entities.Products;
using StoreApp.View.UI.MainViews;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static StoreApp.View.UI.StoreViews.StoreView;
using XAct.Library.Settings;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;

namespace StoreApp.View.UI.ProductsView
{
    /// <summary>
    /// Логика взаимодействия для ProductView.xaml
    /// </summary>
    public partial class ProductView : UserControl
    {

        public StoreMainView StoremainView;
        Product product;
        IStoreProductService productService { get; set; }

        public ProductView()
        {
            InitializeComponent();
        }
        public async void WindowLoad()
        {
            if (panel.Children.Count > 0)
            {
                panel.Children.Clear();
            }

            productService = new StoreProductService();

            NumberFormatInfo numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            numberFormatInfo.NumberGroupSeparator = " ";

            long storeId = long.Parse(StoremainView.store_id.Content.ToString());
            long subCategoryId = long.Parse(StoremainView.sub_category_id.Content.ToString());
            long categoryid = long.Parse(StoremainView.category_id.Content.ToString());

            var products = await productService.GetProducts(storeId, subCategoryId);

            #region Button add
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
            buttonAdd.Click += new RoutedEventHandler(AddBtn_Click);

            borderAdd.Child = buttonAdd;

            panel.Children.Add(borderAdd);

            #endregion

            foreach (var item in products)
            {

                product = new Product()
                {
                    Id = item.Product.Id,
                    Name = item.Product.Name,
                    ArrivalPrice = item.Product.ArrivalPrice,
                    Price = item.Product.Price,
                    SubCategoryId = item.Product.SubCategoryId,
                    Barcode = item.Product.Barcode
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

                #region ColumnDefinitions

                ColumnDefinition c1 = new ColumnDefinition
                {
                    Width = new GridLength(200, GridUnitType.Star)
                };

                ColumnDefinition c2 = new ColumnDefinition
                {
                    Width = new GridLength(50, GridUnitType.Star)
                };
                #endregion

                #region Product Info
                TextBlock txtProductName = new TextBlock
                {
                    Background = Brushes.Transparent,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(10, 10, 0, 0),
                    Text = product.Name,
                    TextWrapping = TextWrapping.Wrap,
                    FontWeight = FontWeights.Bold,
                    FontSize = 25,
                    Width = 200,
                    Height = 70
                };

                TextBlock txtArrivalPrice = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 16,
                    Text = $"Себестоимость : {product.ArrivalPrice.ToString("#,##", numberFormatInfo)}",
                    Margin = new Thickness(10, 0, 0, 0)
                };

                TextBlock txtSellingPrice = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 16,
                    Text = $"Цена : {product.Price.ToString("#,##", numberFormatInfo)}",
                    Margin = new Thickness(10, 0, 0, 0)
                };

                TextBlock txtQuantity = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 16,

                    Text = $"Количество : {item.Quantity.ToString("#,##", numberFormatInfo)}",
                    Margin = new Thickness(10, 0, 0, 0)
                };

                StackPanel stackPanelRow1 = new StackPanel
                {
                    Children = { txtProductName, txtArrivalPrice, txtSellingPrice, txtQuantity }
                };
                #endregion

                Grid grid = new Grid
                {
                    ColumnDefinitions = { c1, c2 },
                    Children = { stackPanelRow1 }
                };

                #region  Buttons edit and delete

                ProductButton btnDelete = new ProductButton
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
                btnDelete.StoreProduct = item;
                btnDelete.Click += new RoutedEventHandler(btnDelete_Click);

                ProductButton btnEdit = new ProductButton
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
                btnEdit.StoreProduct = item;
                btnEdit.Click += new RoutedEventHandler(btnEdit_Click);

                StackPanel stackPanel = new StackPanel
                {
                    Children = { btnEdit, btnDelete }
                };

                #endregion

                Grid.SetColumn(stackPanel, 1);

                grid.Children.Add(stackPanel);
                border.Child = grid;

                panel.Children.Add(border);
            }
        }

        public void GetMainView(StoreMainView storeMainView)
        {
            StoremainView = storeMainView;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            StoremainView.AllCloseControls(2);

            StoremainView.txtSubCategoryName.Text = "";

            StoremainView.nameSubCategory.Visibility = Visibility.Hidden;
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить", "Осторожность", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    ProductButton btnDelete = sender as ProductButton;

                    long id = btnDelete.StoreProduct.Product.Id;

                    IProductService serviceProduct = new ProductService();

                    if (id != 0)
                    {
                        await serviceProduct.Delete(id);

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
                ProductButton btnDelete = sender as ProductButton;

                StoreProduct product = btnDelete.StoreProduct;


                EditProductWindow editProductWindow = new EditProductWindow(product, this);
                editProductWindow.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Category category = new Category()
                {
                    Id = long.Parse(StoremainView.category_id.Content.ToString()),
                    Name = StoremainView.txtcategoryName.Text
                };

                SubCategory subCategory = new SubCategory
                {
                    Id = long.Parse(StoremainView.sub_category_id.Content.ToString()),
                    Name = StoremainView.txtSubCategoryName.Text,
                    CategoryId = category.Id
                };

                AddProductWindow addProductWindow = new AddProductWindow(category, subCategory);
                addProductWindow.GetProductsView(this);
                addProductWindow.ShowDialog();

            }
            catch (Exception)
            {

            }
        }
    }

    public class ProductButton : Button
    {
        public StoreProduct StoreProduct { get; set; }
    }
}
