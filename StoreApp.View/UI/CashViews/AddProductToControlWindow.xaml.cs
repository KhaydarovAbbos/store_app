using StoreApp.Domain.Entities.Products;
using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
using StoreApp.View.UI.MainViews;
using System.Windows;
using System.Windows.Input;

namespace StoreApp.View.UI.CashViews
{
    /// <summary>
    /// Логика взаимодействия для AddProductToControlWindow.xaml
    /// </summary>
    public partial class AddProductToControlWindow : Window
    {
        IStoreProductService storeProductService = new StoreProductService();
        ITabControlProductService tabControlProductService = new TabControlProductService();

        TabController TabController;
        CashView Cashview;

        public AddProductToControlWindow(TabController tabController, CashView cashView)
        {
            InitializeComponent();
            Cashview = cashView;
            TabController = tabController;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            long storeId = long.Parse(StoreMainView.StoreId);

            var response = await storeProductService.GetAll(storeId);

            datagridProducts.ItemsSource = response;
            datagridProducts.Items.Refresh();
        }

        private async void datagridProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StoreProduct storeProduct = datagridProducts.SelectedItem as StoreProduct;

            if (storeProduct != null)
            {
                bool result = await tabControlProductService.IsExist(storeProduct.Product.Name, TabController.Id);

                if (!result)
                {
                    TabControlProductViewModel model = new TabControlProductViewModel()
                    {
                        ProductId = storeProduct.Product.Id,
                        ProductName = storeProduct.Product.Name,
                        TabControllerId = TabController.Id,
                        TabControllerName = TabController.Name
                    };

                    await tabControlProductService.Create(model);

                    Cashview.WindowLoad();
                }

                this.Close();
            }

        }
    }
}
