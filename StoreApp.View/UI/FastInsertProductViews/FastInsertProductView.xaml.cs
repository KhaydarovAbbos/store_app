using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.View.UI.MainViews;
using StoreApp.View.UI.ProductsView;
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

namespace StoreApp.View.UI.FastInsertProductViews
{
    /// <summary>
    /// Логика взаимодействия для FastInsertProductView.xaml
    /// </summary>
    public partial class FastInsertProductView : UserControl
    {

        IProductService productService;
        public FastInsertProductView()
        {
            InitializeComponent();
        }

        public async void WindowLoad()
        {
            productService = new ProductService();

            long storeId = long.Parse(StoreMainView.StoreId);

            var products = await productService.GetProducts(storeId);

            datagridProducts.ItemsSource = products;
            datagridProducts.Items.Refresh();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (datagridProducts.SelectedItems.Count > 0)
            {

                var product = datagridProducts.SelectedItems[0] as Product;

                if (product != null)
                {
                    FastAddProductWindow fastAddProductWindow = new FastAddProductWindow(product, this);
                    fastAddProductWindow.ShowDialog();

                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (datagridProducts.SelectedItems.Count > 0)
            {

                var product = datagridProducts.SelectedItems[0] as Product;

                if (product != null)
                {

                    FastEditProductWindow fastEditProductWindow = new FastEditProductWindow(product, this);
                    fastEditProductWindow.ShowDialog();

                }
            }
        }
    }
}
