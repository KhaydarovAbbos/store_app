using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StoreApp.View.UI.SubCategoryViews
{
    /// <summary>
    /// Логика взаимодействия для EditSubCategoryWindow.xaml
    /// </summary>
    public partial class EditSubCategoryWindow : Window
    {
        SubCategoryView ProductsubCategoryView;
        long subCategoryid = 0;
        long categoryId = 0;
        ISubCategoryService subCategoryService = new SubCategoryService();
        ICategoryService categoryService = new CategoryService();
        IProductService productService = new ProductService();
        IStoreProductService storeProductService = new StoreProductService();   


        public EditSubCategoryWindow(SubCategoryView productSubCategoryView, long id)
        {
            InitializeComponent();

            ProductsubCategoryView = productSubCategoryView;
            subCategoryid = id;
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtError.Text = "Необходимый";
                return;
            }

            txtError.Text = "";
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text.Trim().Length == 0)
                {
                    txtError.Text = "Необходимый";
                    return;
                }

                var category = await categoryService.Get(categoryId);

                SubCategory subCategory = new SubCategory()
                {
                    Id = subCategoryid,
                    Name = txtName.Text,
                    CategoryName = category.Name
                };

                if (!await subCategoryService.IsExist(subCategory.Name))
                {
                    var result =  await subCategoryService.Update(subCategory);

                    await productService.UpdateSubcategoryname(result.Name, result.Id);
                    await storeProductService.UpdateSubcategoryname(result.Name, result.Id);

                    ProductsubCategoryView.WindowLoad();
                    this.Close();
                }
                else
                {
                    txtError.Text = "Есть подкатегория с таким названием";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var subcategory = await subCategoryService.Get(subCategoryid);

            txtName.Text = subcategory.Name;
            categoryId = subcategory.CategoryId;
        }
    }
}
