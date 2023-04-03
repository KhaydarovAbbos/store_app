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
        double productQuantity;
        CashView Cashview { get; set; }
        IStoreProductService storeProductService = new StoreProductService();

        public EditBasketProduct(StoreProduct product, CashView cashview)
        {
            InitializeComponent();
            _product = product;
            txtQuantity.Focus();
            Cashview = cashview;

        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
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

            double quantity = double.Parse(txtQuantity.Text);

            _product.Quantity = productQuantity - quantity;
            await storeProductService.Update(_product);

            _product.Quantity = quantity;
            Cashview.UpdateBasketProductQuantity(_product);
            this.Close();
        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                txtError.Text = "Необходимый";
                return;
            }
            if (productQuantity < double.Parse(txtQuantity.Text))
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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
             var storeProduct = await storeProductService.Get(_product.Id);

            productQuantity = storeProduct.Quantity + _product.Quantity;
        }
    }
}
