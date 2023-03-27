using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StoreApp.View.UI.SubCategoryViews
{
    /// <summary>
    /// Логика взаимодействия для AddSubCategoryWindow.xaml
    /// </summary>
    public partial class AddSubCategoryWindow : Window
    {
        SubCategoryView subCategoryView;
        long CategoryId = 0;
        ISubCategoryService SubCategoryService = new SubCategoryService();

        public AddSubCategoryWindow(SubCategoryView productSubCategoryView, int categoryId)
        {
            InitializeComponent();
            subCategoryView = productSubCategoryView;
            CategoryId = categoryId;
            txtName.Focus();
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text.Trim().Length == 0)
                {
                    txtError.Text = "Необходимый";
                    return;
                }

                SubCategoryViewModel model = new SubCategoryViewModel()
                {
                    Name = txtName.Text,
                    CategoryId = CategoryId
                };

                if (!await SubCategoryService.IsExist(model.Name))
                {
                    await SubCategoryService.Create(model);

                    subCategoryView.WindowLoad();

                    this.Close();

                }
                else
                {
                    txtError.Text = "Есть подкатегория с таким названием";
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
    }
}
