using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
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
using System.Windows.Shapes;

namespace StoreApp.View.UI.CashViews
{
    /// <summary>
    /// Логика взаимодействия для AddProductToControlWindow.xaml
    /// </summary>
    public partial class AddProductToControlWindow : Window
    {
        IStoreProductService storeProductService = new StoreProductService();
        ITabControlProductService tabControlProductService = new TabControlProductService();

        long ControlId;
        CashView Cashview;

        public AddProductToControlWindow(long controlId, CashView cashView)
        {
            InitializeComponent();
            ControlId = controlId;
            Cashview = cashView;
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
            StoreProduct storeProduct = datagridProducts.SelectedItems[0] as StoreProduct;

            if (storeProduct != null)
            {
                bool result = await tabControlProductService.IsExist(storeProduct.Product.Name, ControlId);

                if (!result)
                {
                    TabControlProductViewModel model = new TabControlProductViewModel()
                    {
                        ProductId = storeProduct.Product.Id,
                        ProductName = storeProduct.Product.Name,
                        TabControllerId = ControlId
                    };

                    await tabControlProductService.Create(model);

                    Cashview.WindowLoad();
                }

                this.Close();
    
            }

        }
    }
}
