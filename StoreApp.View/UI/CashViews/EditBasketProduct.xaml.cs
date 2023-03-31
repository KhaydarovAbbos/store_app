using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace StoreApp.View.UI.CashViews
{
    /// <summary>
    /// Логика взаимодействия для EditBasketProduct.xaml
    /// </summary>
    public partial class EditBasketProduct : Window
    {
        StoreProduct _product;
        IStoreProductService storeProductService = new StoreProductService();

        public EditBasketProduct(StoreProduct product)
        {
            InitializeComponent();
            _product = product;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (txtQuantity.Text.Trim().Length == 0)
            {
                txtError.Text = "Необходимый";
                return;
            }
            if (txtError.Text != "")
            {
                return;
            }

            _product.Quantity -= double.Parse(txtQuantity.Text);

            storeProductService.Update(_product);

        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                txtError.Text = "Необходимый";
                return;
            }
            if (_product.Quantity < double.Parse(txtQuantity.Text))
            {
                txtError.Text = "Не так много продуктов в наличии";
                return;
            }

            txtError.Text = "";
        }

        private void txtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
