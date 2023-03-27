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
        ISubCategoryService categoryService = new SubCategoryService();


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

                SubCategory subCategory = new SubCategory()
                {
                    Id = subCategoryid,
                    Name = txtName.Text,
                };

                if (!await categoryService.IsExist(subCategory.Name))
                {
                    await categoryService.Update(subCategory);

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

            var subcategory = await categoryService.Get(subCategoryid);

            txtName.Text = subcategory.Name;
        }
    }
}
