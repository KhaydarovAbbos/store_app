using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StoreApp.View.UI.ProductsView
{
    /// <summary>
    /// Логика взаимодействия для EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        MainWindow mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
        ProductView Productsview;
        long productId;
        StoreProduct _product;
        IProductService productService = new ProductService();
        IStoreProductService storeProductService = new StoreProductService();
        ITabControlProductService controlProductService = new TabControlProductService();
        IReceiveReportService receiveReportService = new ReceiveReportService();

        public EditProductWindow(StoreProduct product, ProductView productsview)
        {
            InitializeComponent();

            productId = product.Id;
            txtName.Text = product.Product.Name;
            txtQuantity.Text = product.Quantity.ToString();
            txtSellingPrice.Text = product.Product.Price.ToString();
            txtArrivalPrice.Text = product.Product.ArrivalPrice.ToString();
            txtBarcode.Text = product.Product.Barcode.ToString();
            Productsview = productsview;
            _product = product;

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
                NumberFormatInfo numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                numberFormatInfo.NumberGroupSeparator = " ";

                txtArrivalPricelabel.Text = double.Parse(txtArrivalPrice.Text == "" ? "0" : txtArrivalPrice.Text).ToString("#,##", numberFormatInfo);


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
                NumberFormatInfo numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                numberFormatInfo.NumberGroupSeparator = " ";

                txtSellingPricelabel.Text = double.Parse(txtSellingPrice.Text == "" ? "0" : txtSellingPrice.Text).ToString("#,##", numberFormatInfo);

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

                NumberFormatInfo numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                numberFormatInfo.NumberGroupSeparator = " ";

                txtQuantitylabel.Text = double.Parse(txtQuantity.Text == "" ? "0" : txtQuantity.Text).ToString("#,##", numberFormatInfo);

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
            if (txtBarcode.Text.Length != 13)
            {
                txtErrorBarocde.Text = "Должно быть только 13 цифр";
                txtBarcode.Focus();
                return;
            }

            mainWindow.SetEffect();

            Product product = new Product()
            {
                Id = _product.ProductId,
                Name = txtName.Text,
                ArrivalPrice = double.Parse(txtArrivalPrice.Text),
                Price = double.Parse(txtSellingPrice.Text),
                Barcode = txtBarcode.Text,
                CategoryName = _product.SubCategory.CategoryName,
                SubCategoryName = _product.SubcategoryName
            };

            var result = await productService.Update(product);

            if (_product.Id == 0)
            {
                StoreProductViewModel storeViewModel = new StoreProductViewModel()
                {
                    ProductId = result.Id,
                    StoreId = _product.StoreId,
                    SubcategoryId = result.SubCategoryId,
                    Quantity = double.Parse(txtQuantity.Text),
                    Barcode = result.Barcode,
                    ProductName = result.Name,
                    SubcategoryName = result.SubCategoryName,
                    Storename = _product.StoreName,
                    ArrivalPrice = result.ArrivalPrice,
                    Price = result.Price,
                };

                await storeProductService.Create(storeViewModel);
            }
            else
            {

                StoreProduct storeProduct = new StoreProduct()
                {
                    Id = _product.Id,
                    ProductId = result.Id,
                    ProductName = result.Name,
                    Quantity = double.Parse(txtQuantity.Text),
                    StoreId = _product.StoreId,
                    StoreName = _product.StoreName,
                    SubcategoryId = result.SubCategoryId,
                    SubcategoryName = result.SubCategoryName,
                    Barcode = result.Barcode,
                    Price = result.Price,
                    ArrivalPrice = result.ArrivalPrice,
                    CategoryId = result.CategoryId,
                    CategoryName = result.CategoryName,
                };

                await storeProductService.Update(storeProduct);
            }

            await controlProductService.UpdateProductName(result.Name, result.Id);
            await receiveReportService.UpdateProductName(result.Name, result.Id);

            Productsview.WindowLoad();

            this.Close();
            mainWindow.RemoveEffect();
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
            txtCategory.Text = Productsview.StoremainView.txtcategoryName.Text;
            txtSubCategory.Text = Productsview.StoremainView.txtSubCategoryName.Text;
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
