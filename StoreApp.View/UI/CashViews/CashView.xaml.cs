using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.View.UI.MainViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
using XAct;
using static StoreApp.View.UI.StoreViews.StoreView;

namespace StoreApp.View.UI.CashViews
{
    /// <summary>
    /// Логика взаимодействия для CashView.xaml
    /// </summary>
    public partial class CashView : UserControl
    {
        IProductService productService;
        IStoreProductService storeProductService;
        ITabControlService tabControlService;
        ITabControlProductService tabControlProductService;

        public MainWindow mainWindow;
        public static string cashName;
        public static long cashId;
        long SelectTabIndex = 0;

        public CashView()
        {
            InitializeComponent();
        }

        public void GetmainWindow(MainWindow mainwindow)
        {
            mainWindow = mainwindow;
            txtName.Text = "Название кассы : " + cashName;

            
        }

        public async void WindowLoad()
        {
            productService = new ProductService();
            storeProductService = new StoreProductService();
            tabControlService = new TabControlService();
            tabControlProductService = new TabControlProductService();

            tabControl.Items.Clear();

            var tabitems = await tabControlService.GetAll();

            foreach (var item in tabitems)
            {
                MytabItem tabItem = new MytabItem();
                tabItem.Header = item.Name;
                tabItem.TabController = item;

                Border borderAdd = new Border
                {
                    Background = Brushes.White,
                    Width = 200,
                    Height = 100,
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
                    FontSize = 25,

                };
                buttonAdd.Click += new RoutedEventHandler(btnAddBorderProduct_Click);

                borderAdd.Child = buttonAdd;

                WrapPanel wrapPanel = new WrapPanel()
                {
                    Children = { borderAdd }
                };

                var products = await tabControlProductService.GetAll(item.Id);

                foreach (var product in products)
                {
                    Border borderProduct = new Border
                    {
                        Background = Brushes.White,
                        Width = 200,
                        Height = 100,
                        BorderBrush = Brushes.Gray,
                        BorderThickness = new Thickness(1),
                        Margin = new Thickness(10, 10, 0, 0),
                        CornerRadius = new CornerRadius(10),
                    };
                    borderProduct.MouseUp += new MouseButtonEventHandler(btnBorder_Click);

                    MyTextBlock txtProductName = new MyTextBlock
                    {
                        Background = Brushes.Transparent,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(0, 0, 0, 0),
                        Text = product.ProductName,
                        TextWrapping = TextWrapping.Wrap,
                        FontWeight = FontWeights.Bold,
                        TextAlignment = TextAlignment.Center,
                        FontSize = 25,
                        Width = 140,
                        TotalInfo = new TotalInfo { Id = product.ProductId, Name = product.ProductName }
                    };


                    MyButton btnDelete = new MyButton
                    {
                        Width = 40,
                        Height = 35,
                        Background = Brushes.White,
                        BorderBrush = Brushes.White,
                        Margin = new Thickness(0, 10, 0, 0),
                        Padding = new Thickness(0),
                        Content = new Image
                        {
                            Source = new BitmapImage(new Uri("../../Images/delete.png", UriKind.Relative)),
                            VerticalAlignment = VerticalAlignment.Center,
                            Width = 20,
                            Height = 20
                        }
                    };
                    btnDelete.Totalinfo = new TotalInfo { Id = product.Id, Name = product.ProductName};
                    btnDelete.Click += new RoutedEventHandler(btnDelete_Click);

                    Grid.SetColumn(txtProductName, 0);
                    Grid.SetColumn(btnDelete, 1);

                    ColumnDefinition c1 = new ColumnDefinition
                    {
                        Width = new GridLength(150, GridUnitType.Star)
                    };

                    ColumnDefinition c2 = new ColumnDefinition
                    {
                        Width = new GridLength(50, GridUnitType.Star)
                    };

                    Grid grid = new Grid
                    {
                        ColumnDefinitions = { c1, c2 },
                        Children = { txtProductName, btnDelete }
                    };

                    borderProduct.Child = grid;

                    wrapPanel.Children.Add(borderProduct);
                }

                tabItem.Content = wrapPanel;
                tabControl.Items.Add(tabItem);

                tabControl.SelectedIndex = tabControl.Items.Count - 1;

            }
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

                                Label label = new Label
                                {
                                    Content = product.Id,
                                    Visibility = Visibility.Hidden
                                };

                                StackPanel stackPanelRow1 = new StackPanel
                                {
                                    Children = { txtProductName, txtQuantity, label }
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

                            Label label = new Label
                            {
                                Content = product.Id,
                                Visibility = Visibility.Hidden
                            };

                            StackPanel stackPanelRow1 = new StackPanel
                            {
                                Children = { txtProductName, txtQuantity, label }
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

        private async void btnExit_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.AllCloseControls(4);

            txtBarcode.Clear();
            txtErrorBarcode.Text = "";

            if (panelProduct.Children.Count > 0)
            {
                var item = panelProduct.Children;

                for (int i = 0; i < panelProduct.Children.Count; i++)
                {
                    long productId = long.Parse((((item[i] as Border).Child as StackPanel).Children[2] as Label).Content.ToString());
                    double productQuantity = double.Parse((((item[i] as Border).Child as StackPanel).Children[1] as TextBlock).Text.Split(":")[1].Trim());

                    var product = await storeProductService.Get(productId);

                    product.Quantity += productQuantity;

                    await storeProductService.Update(product);

                }
                panelProduct.Children.Clear();
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MytabItem mytabItem = tabControl.SelectedItem as MytabItem;

                var result = MessageBox.Show("Вы уверены, что хотите удалить", "Осторожность", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    MyButton btnDelete = sender as MyButton;

                    long id = btnDelete.Totalinfo.Id;

                    if (id != 0)
                    {
                        await tabControlProductService.Delete(id);
                        int index = tabControl.Items.IndexOf(mytabItem);

                        WindowLoad();

                        tabControl.SelectedIndex = index;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddBorderProduct_Click(object sender, RoutedEventArgs e)
        {
            MytabItem mytabItem = tabControl.SelectedItem as MytabItem;

            if (mytabItem != null)
            {
                int index = tabControl.Items.IndexOf(mytabItem);

                AddProductToControlWindow addProductToControlWindow = new AddProductToControlWindow(mytabItem.TabController, this);
                addProductToControlWindow.ShowDialog();

                tabControl.SelectedIndex = index;
            }
        }

        private async void btnBorder_Click(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;

            if (border != null)
            {
                long productId = ((border.Child as Grid).Children[0] as MyTextBlock).TotalInfo.Id;

                AddToBasket(productId);              
            }
        }

        public async void AddToBasket(long id)
        {
            storeProductService = new StoreProductService();

            long storeId = long.Parse(StoreMainView.StoreId);
            var product = await storeProductService.Get(id, storeId);
            int itemIndex = -1;
            int quantity = 1;

            if (product != null && product.Quantity > 0)
            {
                var item = panelProduct.Children;

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

                        Label label = new Label
                        {
                            Content = product.Id,
                            Visibility = Visibility.Hidden
                        };

                        StackPanel stackPanelRow1 = new StackPanel
                        {
                            Children = { txtProductName, txtQuantity, label }
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

                    Label label = new Label
                    {
                        Content = product.Id,
                        Visibility = Visibility.Hidden
                    };

                    StackPanel stackPanelRow1 = new StackPanel
                    {
                        Children = { txtProductName, txtQuantity, label }
                    };

                    border.Child = stackPanelRow1;

                    panelProduct.Children.Add(border);

                    product.Quantity -= quantity;
                    await storeProductService.Update(product);
                }
            }
            else
            {
                MessageBox.Show("Количество товара недостаточно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDeleteTabitem_Click(object sender, RoutedEventArgs e)
        {
            MytabItem mytabItem = tabControl.SelectedItem as MytabItem;

            if (mytabItem != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить", "Осторожность", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    tabControl.Items.Remove(mytabItem);

                    await tabControlService.Delete(mytabItem.TabController.Id);

                    if (tabControl.Items.Count > 1)
                    {
                        tabControl.SelectedIndex = tabControl.Items.Count - 2;
                    }
                }
            }
        }

        private void btnaddtabItem_Click(object sender, RoutedEventArgs e)
        {
            AddTabControllerWindow addTabControllerWindow = new AddTabControllerWindow(this);
            addTabControllerWindow.ShowDialog();

            Keyboard.ClearFocus();
        }

        private void btnEdittabItem_Click(object sender, RoutedEventArgs e)
        {
            if(tabControl.Items.Count > 0)
            {
                EditTabItemWindow editTabItemWindow = new EditTabItemWindow(this);
                editTabItemWindow.ShowDialog();

                Keyboard.ClearFocus();

            }
        }

        private void txtBarcode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnAddProduct_Click(sender, e);
            }
        }

        private void txtBarcode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }


    }

    public class MytabItem : TabItem
    {
        public TabController TabController { get; set; }
    }

    public class MyTextBlock : TextBlock
    {
        public TotalInfo TotalInfo { get; set; }
    }
}
