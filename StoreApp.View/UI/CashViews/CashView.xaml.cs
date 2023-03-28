using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.View.UI.MainViews;
using System;
using System.Collections.Generic;
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

namespace StoreApp.View.UI.CashViews
{
    /// <summary>
    /// Логика взаимодействия для CashView.xaml
    /// </summary>
    public partial class CashView : UserControl
    {
        IProductService productService = new ProductService();
        IStoreProductService storeProductService = new StoreProductService();
        public MainWindow mainWindow;
        public static string cashName;
        public static long cashId;

        public CashView()
        {
            InitializeComponent();
        }

        public void GetmainWindow(MainWindow mainwindow)
        {
            mainWindow = mainwindow;
            txtName.Text = "Название кассы : " + cashName;
        }

        private async void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (txtBarcode.Text != "")
            {
                var item = panelProduct.Children;

                double quantity = 1;
                int itemIndex = -1;
                string producBarcode = txtBarcode.Text;
                long storeId = long.Parse(StoreMainView.StoreId);

                var product = await storeProductService.Get(storeId, producBarcode);

                if (product != null)
                {
                    if (product.Quantity > 0)
                    {

                        if (item.Count > 0)
                        {
                            for (int i = 0; i < item.Count; i++)
                            {
                                string productName = (((item[i] as Border).Child as StackPanel).Children[0] as TextBlock).Text;

                                if (product.Product.Name == productName)
                                {
                                    itemIndex = i;
                                }
                            }

                            if (itemIndex != -1)
                            {
                                double productQuantity = double.Parse((((item[itemIndex] as Border).Child as StackPanel).Children[1] as TextBlock).Text.Split(":")[1].Trim());
                                (((item[itemIndex] as Border).Child as StackPanel).Children[1] as TextBlock).Text = $"Количество : {productQuantity + quantity}";

                                product.Quantity = product.Quantity - quantity;
                                await storeProductService.Update(product);
                            }
                            else
                            {
                                Border border = new Border
                                {
                                    Background = Brushes.White,
                                    Width = 230,
                                    Height = 100,
                                    BorderBrush = Brushes.Gray,
                                    BorderThickness = new Thickness(1),
                                    Margin = new Thickness(10, 10, 0, 0),
                                    CornerRadius = new CornerRadius(10)

                                };

                                TextBlock txtProductName = new TextBlock
                                {
                                    Background = Brushes.Transparent,
                                    VerticalAlignment = VerticalAlignment.Center,
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    Margin = new Thickness(10, 10, 0, 0),
                                    Text = product.Product.Name,
                                    TextWrapping = TextWrapping.Wrap,
                                    FontWeight = FontWeights.Bold,
                                    FontSize = 25,
                                    Width = 200,
                                    Height = 60
                                };

                                TextBlock txtQuantity = new TextBlock
                                {
                                    VerticalAlignment = VerticalAlignment.Top,
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    Width = 200,
                                    Height = 20,
                                    FontSize = 16,
                                    Text = $"Количество : {quantity}",
                                    Margin = new Thickness(10, 0, 0, 50)
                                };

                                StackPanel stackPanelRow1 = new StackPanel
                                {
                                    Children = { txtProductName, txtQuantity }
                                };

                                border.Child = stackPanelRow1;

                                panelProduct.Children.Add(border);

                                product.Quantity = product.Quantity - quantity;
                                await storeProductService.Update(product);
                            }
                        }
                        else
                        {
                            Border border = new Border
                            {
                                Background = Brushes.White,
                                Width = 230,
                                Height = 100,
                                BorderBrush = Brushes.Gray,
                                BorderThickness = new Thickness(1),
                                Margin = new Thickness(10, 10, 0, 0),
                                CornerRadius = new CornerRadius(10)
                            };

                            TextBlock txtProductName = new TextBlock
                            {
                                Background = Brushes.Transparent,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Margin = new Thickness(10, 10, 0, 0),
                                Text = product.Product.Name,
                                TextWrapping = TextWrapping.Wrap,
                                FontWeight = FontWeights.Bold,
                                FontSize = 25,
                                Width = 200,
                                Height = 60
                            };

                            TextBlock txtQuantity = new TextBlock
                            {
                                VerticalAlignment = VerticalAlignment.Top,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Width = 200,
                                Height = 20,
                                FontSize = 16,
                                Text = $"Количество : {quantity}",
                                Margin = new Thickness(10, 0, 0, 50)
                            };

                            StackPanel stackPanelRow1 = new StackPanel
                            {
                                Children = { txtProductName, txtQuantity }
                            };

                            border.Child = stackPanelRow1;

                            panelProduct.Children.Add(border);

                            product.Quantity = product.Quantity - quantity;
                            await storeProductService.Update(product);
                        }
                    }
                    else
                    {
                        txtErrorBarcode.Text = "Количество товара недостаточно";
                    }

                }
                else
                {
                    txtErrorBarcode.Text = "Товар не найден";
                }
            }
        }

        private void txtBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtErrorBarcode.Text != "")
            {
                txtErrorBarcode.Text = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.AllCloseControls(4);
        }
    }
}
