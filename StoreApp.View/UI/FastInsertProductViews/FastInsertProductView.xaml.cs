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

namespace StoreApp.View.UI.FastInsertProductViews
{
    /// <summary>
    /// Логика взаимодействия для FastInsertProductView.xaml
    /// </summary>
    public partial class FastInsertProductView : UserControl
    {

        IProductService productService = new ProductService();
        public FastInsertProductView()
        {
            InitializeComponent();
        }

        public async void WindowLoad()
        {
            long storeId = long.Parse(StoreMainView.StoreId);

            var products = await productService.GetProducts(storeId);

            datagridProducts.ItemsSource = products;
            datagridProducts.Items.Refresh();

        }
    }
}
