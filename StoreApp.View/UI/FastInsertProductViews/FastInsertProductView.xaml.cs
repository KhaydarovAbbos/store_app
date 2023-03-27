using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.View.UI.MainViews;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StoreApp.View.UI.FastInsertProductViews
{
    /// <summary>
    /// Логика взаимодействия для FastInsertProductView.xaml
    /// </summary>
    public partial class FastInsertProductView : UserControl
    {

        IProductService productService;
        IStoreProductService storeProductService;

        public FastInsertProductView()
        {
            InitializeComponent();
        }

        public async void WindowLoad()
        {
            productService = new ProductService();
            storeProductService = new StoreProductService();

            long storeId = long.Parse(StoreMainView.StoreId);

            var response = await storeProductService.GetAll(storeId);

            datagridProducts.ItemsSource = response;
            datagridProducts.Items.Refresh();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (datagridProducts.SelectedItems.Count > 0)
            {

                var product = datagridProducts.SelectedItems[0] as StoreProduct;

                if (product != null)
                {

                    product.StoreId = long.Parse(StoreMainView.StoreId);
                    FastAddProductWindow fastAddProductWindow = new FastAddProductWindow(product, this);
                    fastAddProductWindow.ShowDialog();

                    Keyboard.ClearFocus();
                }
            }
        }

        private async void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (datagridProducts.SelectedItems.Count > 0)
            {

                var product = datagridProducts.SelectedItems[0] as StoreProduct;

                if (product != null)
                {
                    product.StoreId = long.Parse(StoreMainView.StoreId);
                    FastEditProductWindow fastEditProductWindow = new FastEditProductWindow(product, this);
                    fastEditProductWindow.ShowDialog();

                    Keyboard.ClearFocus();
                }
            }
        }
    }
}
