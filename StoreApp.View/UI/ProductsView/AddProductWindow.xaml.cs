using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
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
using XAct.Library.Settings;

namespace StoreApp.View.UI.ProductsView
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        Category Productcategory { get; set; }
        SubCategory ProductSubcategory { get; set; }
        ProductView Productsview;
        IProductService productService = new ProductService();
        IStoreProductService storeProductService = new StoreProductService();

        public AddProductWindow(Category category, SubCategory subCategory)
        {
            InitializeComponent();

            Productcategory = category;
            ProductSubcategory = subCategory;

            txtCategory.Text = category.Name;
            txtSubCategory.Text = subCategory.Name;
        }

        public void GetProductsView(ProductView productsView)
        {
            Productsview = productsView;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
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
            if (autoBarcodeGrid.Visibility == Visibility.Visible)
            {
                byte[] encoded = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(txtName.Text));
                var value = BitConverter.ToUInt32(encoded, 0) % 100000;
                //txtBarcode.Text = txtName.Text[0].ToString() + value.ToString();
                txtBarcode.Text = "478" + value.ToString();
            }
            if (barcodeGrid.Visibility == Visibility.Visible)
            {
                if (txtBarcode.Text == "")
                {

                    txtErrorBarocde.Text = "Необходимый";
                    txtBarcode.Focus();
                    return;

                }
            }

            ProductViewModel productViewModel = new ProductViewModel()
            {
                Name = txtName.Text,
                Arrivalprice = double.Parse(txtArrivalPrice.Text),
                Price = double.Parse(txtSellingPrice.Text),
                Barcode = txtBarcode.Text,
                SubCategoryId = ProductSubcategory.Id,
                CategoryId = Productcategory.Id
               
            };
            var result = await productService.Create(productViewModel);

            var store_id = long.Parse(Productsview.StoremainView.store_id.Content.ToString());

            StoreProductViewModel storeProductViewModel = new StoreProductViewModel()
            {
                ProductId = result.Id,
                SubcategoryId = result.SubCategoryId,
                StoreId = store_id,
                Quantity = double.Parse(txtQuantity.Text)
            };

            await storeProductService.Create(storeProductViewModel);

            Productsview.WindowLoad();

            this.Close();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtName.Text.Length == 0 || txtName.Text == "")
            {
                txtErrorName.Text = "Необходимый";
                txtName.Focus();
            }
            else
            {
                txtErrorName.Text = "";
            }
        }

        private void txtArrivalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtArrivalPrice.Text.Length == 0 || txtArrivalPrice.Text == "")
                {
                    txtErrorArrivalPrice.Text = "Необходимый";
                    txtArrivalPrice.Focus();
                }
                else
                {
                    txtErrorArrivalPrice.Text = "";
                }
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
                {
                    txtErrorSellingPrice.Text = "Необходимый";
                    txtSellingPrice.Focus();
                }
                else
                {
                    txtErrorSellingPrice.Text = "";
                }
            }
            catch (Exception)
            {

            }
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

        private void ckBarcode_Unchecked(object sender, RoutedEventArgs e)
        {
            txtBarcode.Clear();
            txtBarcode.Focus();

            barcodeGrid.Visibility = Visibility.Hidden;

            ckAutoBarcode.IsChecked = false;
            txtErrorBarocde.Text = "";
        }

        private void txtBarcode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ckAutoBarcode_Checked(object sender, RoutedEventArgs e)
        {
            barcodeGrid.Visibility = Visibility.Visible;
        }

        private void txtBarcode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
