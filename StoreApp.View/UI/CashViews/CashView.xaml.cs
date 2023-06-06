using StoreApp.Domain.Entities.Products;
using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.View.UI.MainViews;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

                    var storeProduct = await storeProductService.Get(product.ProductId, long.Parse(StoreMainView.StoreId));

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
                        Margin = new Thickness(0, 10, 0, 0),
                        Text = product.ProductName,
                        TextWrapping = TextWrapping.Wrap,
                        FontWeight = FontWeights.Bold,
                        TextAlignment = TextAlignment.Center,
                        FontSize = 25,
                        Height = 60,
                        Width = 140,
                        TotalInfo = new TotalInfo { Id = product.ProductId, Name = product.ProductName }
                    };

                    TextBlock txtQuantity = new TextBlock
                    {
                        VerticalAlignment = VerticalAlignment.Bottom,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = 140,
                        Height = 20,
                        FontSize = 16,
                        Text = $"Количество : {storeProduct.Quantity}",
                        Margin = new Thickness(10, 0, 0, 0)
                    };

                    StackPanel stackPanelRow1 = new StackPanel
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        Children = { txtProductName, txtQuantity}
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
                    btnDelete.Totalinfo = new TotalInfo { Id = product.Id, Name = product.ProductName };
                    btnDelete.Click += new RoutedEventHandler(btnDelete_Click);

                    Grid.SetColumn(stackPanelRow1, 0);
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
                        Children = { stackPanelRow1, btnDelete }
                    };

                    borderProduct.Child = grid;

                    wrapPanel.Children.Add(borderProduct);
                }

                tabItem.Content = wrapPanel;
                tabControl.Items.Add(tabItem);

                tabControl.SelectedIndex = 0;
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
                        AddToBasket(product.ProductId);
                        //txtBarcode.Clear();
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
            storeProductService = new StoreProductService();

            txtBarcode.Clear();
            txtErrorBarcode.Text = "";

            if (panelProduct.Children.Count > 0)
            {
                var item = panelProduct.Children;

                for (int i = 0; i < panelProduct.Children.Count; i++)
                {
                    long productId = (item[i] as MyBorder).TotalInfo.Id;
                    double productQuantity = double.Parse(((((item[i] as MyBorder).Child as Grid).Children[0] as StackPanel).Children[1] as MyTextBlock).Text.Split("шт")[0].Trim());

                    var product = await storeProductService.Get(productId);

                    product.Quantity += productQuantity;

                    await storeProductService.Update(product);

                }
                panelProduct.Children.Clear();
            }

            mainWindow.AllCloseControls(4);
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

        private async void btnAddBorderProduct_Click(object sender, RoutedEventArgs e)
        {
            MytabItem mytabItem = tabControl.SelectedItem as MytabItem;

            if (mytabItem != null)
            {
                int index = tabControl.Items.IndexOf(mytabItem);

                AddProductToControlWindow addProductToControlWindow = new AddProductToControlWindow(mytabItem.TabController, this);
                addProductToControlWindow.ShowDialog();

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

                var products = await tabControlProductService.GetAll(mytabItem.TabController.Id);

                foreach (var product in products)
                {

                    var storeProduct = await storeProductService.Get(product.ProductId, long.Parse(StoreMainView.StoreId));

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
                        Margin = new Thickness(0, 10, 0, 0),
                        Text = product.ProductName,
                        TextWrapping = TextWrapping.Wrap,
                        FontWeight = FontWeights.Bold,
                        TextAlignment = TextAlignment.Center,
                        FontSize = 25,
                        Height = 60,
                        Width = 140,
                        TotalInfo = new TotalInfo { Id = product.ProductId, Name = product.ProductName }
                    };

                    TextBlock txtQuantity = new TextBlock
                    {
                        VerticalAlignment = VerticalAlignment.Bottom,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = 140,
                        Height = 20,
                        FontSize = 16,
                        Text = $"Количество : {storeProduct.Quantity}",
                        Margin = new Thickness(10, 0, 0, 0)
                    };

                    StackPanel stackPanelRow1 = new StackPanel
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        Children = { txtProductName, txtQuantity }
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
                    btnDelete.Totalinfo = new TotalInfo { Id = product.Id, Name = product.ProductName };
                    btnDelete.Click += new RoutedEventHandler(btnDelete_Click);

                    Grid.SetColumn(stackPanelRow1, 0);
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
                        Children = { stackPanelRow1, btnDelete }
                    };

                    borderProduct.Child = grid;

                    wrapPanel.Children.Add(borderProduct);
                }

                mytabItem.Content = wrapPanel;
                tabControl.Items.Refresh();
                tabControl.SelectedIndex = index;
            }
        }

        private async void btnBorder_Click(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;

            if (border != null)
            {
                long productId = (((border.Child as Grid).Children[0] as StackPanel).Children[0] as MyTextBlock).TotalInfo.Id;

                AddToBasket(productId);
            }
        }

        public async void AddToBasket(long id)
        {
            storeProductService = new StoreProductService();
            NumberFormatInfo numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            numberFormatInfo.NumberGroupSeparator = " ";

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
                        string productName = (item[i] as MyBorder).TotalInfo.Name;

                        if (product.Product.Name == productName)
                        {
                            itemIndex = i;
                        }
                    }

                    if (itemIndex != -1)
                    {
                        double productQuantity = double.Parse(((((item[itemIndex] as MyBorder).Child as Grid).Children[0] as StackPanel).Children[1] as MyTextBlock).Text.Split("шт")[0].Trim()) + quantity;
                        ((((item[itemIndex] as MyBorder).Child as Grid).Children[0] as StackPanel).Children[1] as MyTextBlock).Text = $"{productQuantity} шт  X  {(product.Price).ToString("#,##", numberFormatInfo)}  =  {(productQuantity * product.Price).ToString("#,##", numberFormatInfo)}";

                        product.Quantity = product.Quantity - quantity;
                        await storeProductService.Update(product);
                    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
                    else
                    {
                        MyBorder border = new MyBorder
                        {
                            Background = Brushes.White,
                            Width = 299,
                            Height = 60,
                            BorderBrush = Brushes.Gray,
                            BorderThickness = new Thickness(1),
                            Margin = new Thickness(9, 10, 0, 0),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            CornerRadius = new CornerRadius(10),
                            TotalInfo = new TotalInfo { Id = product.Id, Name = product.ProductName },
                        };

                        MyTextBlock txtProductName = new MyTextBlock
                        {
                            Background = Brushes.Transparent,
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Margin = new Thickness(10, 10, 0, 0),
                            Text = product.Product.Name,
                            TextWrapping = TextWrapping.Wrap,
                            FontWeight = FontWeights.Bold,
                            TotalInfo = new TotalInfo { Id = product.Id, Name = product.ProductName },
                            FontSize = 25,
                            Width = 200,
                            Height = 60
                        };

                        MyTextBlock txtQuantity = new MyTextBlock
                        {
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Width = 239,
                            Height = 20,
                            FontWeight = FontWeights.Bold,
                            FontSize = 16,
                            TotalInfo = new TotalInfo { Id = product.Id, Name = product.ProductName },
                            Text = $"{quantity} шт  X  {(product.Price).ToString("#,##", numberFormatInfo)}  =  {(quantity * product.Price).ToString("#,##", numberFormatInfo)}",
                            Margin = new Thickness(10, 0, 0, 50)
                        };

                        Label label = new Label
                        {
                            Content = product.Id,
                            Visibility = Visibility.Hidden
                        };

                        ColumnDefinition c1 = new ColumnDefinition
                        {
                            Width = new GridLength(249, GridUnitType.Star)
                        };

                        ColumnDefinition c2 = new ColumnDefinition
                        {
                            Width = new GridLength(50, GridUnitType.Star)
                        };

                        StackPanel stackPanelRow1 = new StackPanel
                        {
                            Children = { txtProductName, txtQuantity, label }
                        };

                        MyButton btnEdit = new MyButton
                        {
                            Width = 40,
                            Height = 35,
                            Background = Brushes.White,
                            BorderBrush = Brushes.White,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(0, 10, 0, 0),
                            Padding = new Thickness(0),
                            Totalinfo = new TotalInfo { Id = product.Id, Name = product.ProductName },
                            Content = new Image
                            {
                                Source = new BitmapImage(new Uri("../../Images/pencil.png", UriKind.Relative)),
                                VerticalAlignment = VerticalAlignment.Center,
                                Width = 20,
                                Height = 20

                            }
                        };
                        btnEdit.Click += new RoutedEventHandler(btnEdit_Click);


                        Grid.SetColumn(stackPanelRow1, 0);
                        Grid.SetColumn(btnEdit, 1);

                        Grid grid = new Grid
                        {
                            ColumnDefinitions = { c1, c2 },
                            Children = { stackPanelRow1, btnEdit }
                        };

                        border.Child = grid;

                        panelProduct.Children.Add(border);

                        product.Quantity -= quantity;
                        await storeProductService.Update(product);
                    }
                }
                else
                {
                    MyBorder border = new MyBorder
                    {
                        Background = Brushes.White,
                        Width = 299,
                        Height = 60,
                        BorderBrush = Brushes.Gray,
                        BorderThickness = new Thickness(1),
                        Margin = new Thickness(9, 5, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        CornerRadius = new CornerRadius(10),
                        TotalInfo = new TotalInfo { Id = product.Id, Name = product.ProductName },
                    };

                    MyTextBlock txtProductName = new MyTextBlock
                    {
                        Background = Brushes.Transparent,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(5, 5, 0, 0),
                        Text = product.Product.Name,
                        TextWrapping = TextWrapping.Wrap,
                        FontWeight = FontWeights.Bold,
                        TotalInfo = new TotalInfo { Id = product.Id, Name = product.ProductName },
                        FontSize = 22,
                        Width = 238,
                        Height = 30
                    };

                    MyTextBlock txtQuantity = new MyTextBlock
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = 239,
                        Height = 18,
                        FontWeight = FontWeights.Bold,
                        FontSize = 16,
                        TotalInfo = new TotalInfo { Id = product.Id, Name = product.ProductName },
                        Text = $"{quantity} шт  X  {(product.Price).ToString("#,##", numberFormatInfo)}  =  {(quantity * product.Price).ToString("#,##", numberFormatInfo)}",
                        Margin = new Thickness(10, 0, 0, 70)
                    };

                    Label label = new Label
                    {
                        Content = product.Id,
                        Visibility = Visibility.Hidden
                    };

                    ColumnDefinition c1 = new ColumnDefinition
                    {
                        Width = new GridLength(249, GridUnitType.Star)
                    };

                    ColumnDefinition c2 = new ColumnDefinition
                    {
                        Width = new GridLength(50, GridUnitType.Star)
                    };

                    StackPanel stackPanelRow1 = new StackPanel
                    {
                        Children = { txtProductName, txtQuantity, label }
                    };

                    MyButton btnEdit = new MyButton
                    {
                        Width = 40,
                        Height = 35,
                        Background = Brushes.White,
                        BorderBrush = Brushes.White,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(0, 10, 0, 0),
                        Padding = new Thickness(0),
                        Totalinfo = new TotalInfo { Id = product.Id, Name = product.ProductName },
                        Content = new Image
                        {
                            Source = new BitmapImage(new Uri("../../Images/pencil.png", UriKind.Relative)),
                            VerticalAlignment = VerticalAlignment.Center,
                            Width = 20,
                            Height = 20

                        }

                    };
                    btnEdit.Click += new RoutedEventHandler(btnEdit_Click);


                    Grid.SetColumn(stackPanelRow1, 0);
                    Grid.SetColumn(btnEdit, 1);

                    Grid grid = new Grid
                    {
                        ColumnDefinitions = {c1, c2},
                        Children = { stackPanelRow1, btnEdit }

                    };

                    border.Child = grid;

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
            if (tabControl.Items.Count > 0)
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

        private async void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            MyButton myButton = sender as MyButton;

            if (myButton != null)
            {
                string productName = myButton.Totalinfo.Name;

                if (panelProduct.Children.Count > 0)
                {
                    var item = panelProduct.Children;

                    for (int i = 0; i < panelProduct.Children.Count; i++)
                    {
                        if ((item[i] as MyBorder).TotalInfo.Name == productName)
                        {
                            var product = await storeProductService.Get(myButton.Totalinfo.Id);

                            var quantity =  double.Parse(((((item[i] as MyBorder).Child as Grid).Children[0] as StackPanel).Children[1] as MyTextBlock).Text.Split("шт")[0].Trim());

                            product.Quantity = quantity;

                            EditBasketProduct editBasketProduct = new EditBasketProduct(product, this);
                            editBasketProduct.ShowDialog();

                            Keyboard.ClearFocus();
                        }
                    }
                }
            }
        }

        public async void UpdateBasketProductQuantity(StoreProduct product)
        {
            NumberFormatInfo numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            numberFormatInfo.NumberGroupSeparator = " ";

            if (panelProduct.Children.Count > 0)
            {
                var item = panelProduct.Children;

                for (int i = 0; i < panelProduct.Children.Count; i++)
                {
                    if ((item[i] as MyBorder).TotalInfo.Name == product.ProductName)
                    {
                        ((((item[i] as MyBorder).Child as Grid).Children[0] as StackPanel).Children[1] as MyTextBlock).Text = $"{product.Quantity} шт  X  {(product.Price).ToString("#,##", numberFormatInfo)}  =  {(product.Quantity * product.Price).ToString("#,##", numberFormatInfo)}";

                    }
                }
            }
        }


        private async void btnBasketDelete_Click(object sender, RoutedEventArgs e)
        {
            MyButton myButton = sender as MyButton;

            if (myButton != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить", "Осторожность", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    string productName = myButton.Totalinfo.Name;

                    if (panelProduct.Children.Count > 0)
                    {
                        var item = panelProduct.Children;

                        for (int i = 0; i < panelProduct.Children.Count; i++)
                        {
                            if ((item[i] as MyBorder).TotalInfo.Name == productName)
                            {
                                storeProductService = new StoreProductService();
                                var product = await storeProductService.Get(myButton.Totalinfo.Id);

                                double productQuantity = double.Parse(((((item[i] as MyBorder).Child as Grid).Children[0] as StackPanel).Children[1] as MyTextBlock).Text.Split("шт")[0].Trim());
                                product.Quantity += productQuantity;

                                await storeProductService.Update(product);

                                panelProduct.Children.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }

        public async Task WindowClose()
        {
            try
            {
                storeProductService = new StoreProductService();
                mainWindow.SetEffect();

                if (panelProduct.Children.Count > 0)
                {
                    var item = panelProduct.Children;

                    for (int i = 0; i < panelProduct.Children.Count; i++)
                    {
                        long productId = (item[i] as MyBorder).TotalInfo.Id; 

                        double productQuantity = double.Parse(((((item[i] as MyBorder).Child as Grid).Children[0] as StackPanel).Children[1] as MyTextBlock).Text.Split("шт")[0].Trim());

                        var product = await storeProductService.Get(productId);

                        product.Quantity += productQuantity;

                        await storeProductService.Update(product);
                    }
                    panelProduct.Children.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            mainWindow.RemoveEffect();
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

    public class MyBorder : Border
    {
        public TotalInfo TotalInfo { get; set; }
    }
}
