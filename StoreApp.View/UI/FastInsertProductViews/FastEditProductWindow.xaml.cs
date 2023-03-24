using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
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
using System.Windows.Shapes;

namespace StoreApp.View.UI.FastInsertProductViews
{
    /// <summary>
    /// Логика взаимодействия для FastEditProductWindow.xaml
    /// </summary>
    public partial class FastEditProductWindow : Window
    {
        FastInsertProductView Productsview;
        Product _product;
        IProductService productService = new ProductService();

        public FastEditProductWindow(Product product, FastInsertProductView productsview)
        {
            InitializeComponent();

            _product = product;
            txtName.Text = _product.Name;
            txtQuantity.Text = "0";
            txtSellingPrice.Text = _product.Price.ToString();
            txtArrivalPrice.Text = _product.ArrivalPrice.ToString();
            txtBarcode.Text = _product.Barcode.ToString();
            txtSubCategory.Text = _product.SubCategory.Name;
            txtCategory.Text = _product.SubCategory.Category.Name;

            Productsview = productsview;

        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtName.Text.Length == 0 || txtName.Text == "")
                txtErrorName.Text = "Необходимый";
            else
                txtErrorName.Text = "";
        }

        private void txtArrivalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                if (txtArrivalPrice.Text.Length == 0 || txtArrivalPrice.Text == "")
                    txtErrorArrivalPrice.Text = "Необходимый";
                else
                    txtErrorArrivalPrice.Text = "";
            }
            catch (Exception)
            {

            }

        }

        private void txtSellingPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {

                if (txtSellingPrice.Text.Length == 0 || txtSellingPrice.Text == "")
                    txtErrorSellingPrice.Text = "Необходимый";
                else
                    txtErrorSellingPrice.Text = "";
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
                    txtErrorQuantity.Text = "Необходимый";
                else
                    txtErrorQuantity.Text = "";
            }
            catch (Exception)
            {
            }
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "")
            {
                txtErrorName.Text = "Необходимый";
                txtName.Focus();
                return;
            }
            if (txtArrivalPrice.Text == "")
            {
                txtErrorArrivalPrice.Text = "Необходимый";
                txtArrivalPrice.Focus();
                return;
            }
            if (txtSellingPrice.Text == "")
            {
                txtErrorSellingPrice.Text = "Необходимый";
                txtSellingPrice.Focus();
                return;
            }
            if (txtQuantity.Text == "")
            {
                txtErrorQuantity.Text = "Необходимый";
                txtQuantity.Focus();
                return;
            }
            if (txtBarcode.Text == "")
            {
                txtErrorBarocde.Text = "Необходимый";
                txtBarcode.Focus();
                return;
            }

            Product product = new Product()
            {
                Id = _product.Id,
                Name = txtName.Text,
                ArrivalPrice = double.Parse(txtArrivalPrice.Text),
                Price = double.Parse(txtSellingPrice.Text),
                Barcode = txtBarcode.Text
            };

            await productService.Update(product);

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void txtBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtBarcode.Text.Contains(" "))
                {
                    var text = txtBarcode.Text;
                    int index = text.IndexOf(" ");
                    txtBarcode.Text = text.Remove(index);
                    txtBarcode.CaretIndex = txtBarcode.Text.Length;
                }

                if (txtBarcode.Text.Length == 0 || txtBarcode.Text == "")
                {
                    txtErrorBarocde.Text = "Необходимый";
                    txtBarcode.Focus();
                }
                else if (txtBarcode.Text.Length > 0 && txtBarcode.Text.Length < 13)
                {
                    txtErrorBarocde.Text = "Должно быть только 13 цифр";
                    txtBarcode.Focus();
                }
                else if (txtBarcode.Text.Length > 13)
                {
                    var text = txtBarcode.Text;
                    txtBarcode.Text = text.Remove(text.Length - 1);
                    txtBarcode.CaretIndex = txtBarcode.Text.Length;
                    return;
                }
                else
                {
                    txtErrorBarocde.Text = "";
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
