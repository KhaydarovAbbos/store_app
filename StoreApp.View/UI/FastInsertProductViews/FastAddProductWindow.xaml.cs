using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
using StoreApp.View.UI.ProductsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace StoreApp.View.UI.FastInsertProductViews
{
    /// <summary>
    /// Логика взаимодействия для FastAddProductWindow.xaml
    /// </summary>
    public partial class FastAddProductWindow : Window
    {
        FastInsertProductView Productsview;
        IStoreProductService productService = new StoreProductService();


        StoreProduct _product { get; set; }


        public FastAddProductWindow(StoreProduct product, FastInsertProductView productsView)
        {
            InitializeComponent();

            _product = product;
            Productsview = productsView;

            txtCategory.Text = _product.Product.SubCategory.Category.Name;
            txtSubCategory.Text = _product.Product.SubCategory.Name;
            txtName.Text = _product.Product.Name;
            txtBarcode.Text = _product.Product.Barcode;
            txtArrivalPrice.Text = _product.Product.ArrivalPrice.ToString();
            txtSellingPrice.Text = _product.Product.Price.ToString();

            txtQuantity.Focus();
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (txtQuantity.Text == "")
            {
                txtErrorQuantity.Text = "Необходимый";
                txtQuantity.Focus();
                return;
            }

            if (_product.Id != 0)
            {
                _product.Quantity = double.Parse(txtQuantity.Text) + _product.Quantity;

                await productService.Update(_product);
            }
            else
            {
                StoreProductViewModel productViewModel = new StoreProductViewModel()
                {
                    ProductId = _product.Product.Id,
                    StoreId = _product.StoreId,
                    SubcategoryId = _product.Product.SubCategory.Id,
                    Quantity = double.Parse(txtQuantity.Text)
                };

                await productService.Create(productViewModel);
            }

            Productsview.WindowLoad();

            this.Close();
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            try
            {
                TextBox textbox = (TextBox)sender;

                char ch = e.Text[0];

                if ((Char.IsDigit(ch) || ch == '.'))
                {

                    if (ch == '.' && textbox.Text.Contains('.'))

                        e.Handled = true;
                }

                else
                    e.Handled = true;

            }
            catch (Exception)
            {

            }
        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtQuantity.Text.Length == 0 || txtQuantity.Text == "")
                {
                    txtErrorQuantity.Text = "Необходимый";
                    txtQuantity.Focus();
                }
                else
                {
                    txtErrorQuantity.Text = "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtBarcode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
