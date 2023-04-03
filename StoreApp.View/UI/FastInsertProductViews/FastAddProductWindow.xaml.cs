using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
using StoreApp.View.UI.MainViews;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StoreApp.View.UI.FastInsertProductViews
{
    /// <summary>
    /// Логика взаимодействия для FastAddProductWindow.xaml
    /// </summary>
    public partial class FastAddProductWindow : Window
    {
        FastInsertProductView Productsview;
        IStoreProductService productService = new StoreProductService();
        IReceiveReportService receiveReportService = new ReceiveReportService();
        NumberFormatInfo numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();



        StoreProduct _product { get; set; }


        public FastAddProductWindow(StoreProduct product, FastInsertProductView productsView)
        {
            InitializeComponent();

            _product = product;
            Productsview = productsView;

            numberFormatInfo.NumberGroupSeparator = " ";

            txtCategory.Text = _product.Product.SubCategory.Category.Name;
            txtSubCategory.Text = _product.Product.SubCategory.Name;
            txtName.Text = _product.Product.Name;
            txtBarcode.Text = _product.Product.Barcode;
            txtArrivalPrice.Text = _product.Product.ArrivalPrice.ToString("#,##", numberFormatInfo); ;
            txtSellingPrice.Text = _product.Product.Price.ToString("#,##", numberFormatInfo); ;

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
            if (txtBarcode.Text.Length != 13)
            {
                txtBarcode.Focus();
                return;

            }

            if (_product.Id != 0)
            {
                _product.Quantity = double.Parse(txtQuantity.Text) + _product.Quantity;
                _product.ArrivalPrice = _product.ArrivalPrice;
                _product.Price = _product.Price;

                await productService.Update(_product);
            }
            else
            {
                StoreProductViewModel productViewModel = new StoreProductViewModel()
                {
                    ProductId = _product.Product.Id,
                    ProductName = _product.Product.Name,
                    StoreId = long.Parse(StoreMainView.StoreId),
                    Storename = StoreMainView.Storename,
                    CategoryId = _product.CategoryId,
                    CategoryName = _product.CategoryName,
                    SubcategoryId = _product.Product.SubCategory.Id,
                    SubcategoryName = _product.SubcategoryName,
                    Quantity = double.Parse(txtQuantity.Text),
                    ArrivalPrice = _product.ArrivalPrice,
                    Price = _product.Price,
                    Barcode = _product.Product.Barcode,

                };
                await productService.Create(productViewModel);
            }

            ReceiveReportViewModel receiveReportViewModel = new ReceiveReportViewModel()
            {
                ProductId = _product.Product.Id,
                ProductName = _product.Product.Name,
                Quantity = double.Parse(txtQuantity.Text)

            };
            await receiveReportService.CreateAsync(receiveReportViewModel);

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
