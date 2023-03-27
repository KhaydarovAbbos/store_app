using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StoreApp.View.UI.CategoryViews
{
    /// <summary>
    /// Логика взаимодействия для AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        CategoryView Categoryview;
        ICategoryService categoryService = new CategoryService();

        public AddCategoryWindow(CategoryView categoryview)
        {
            InitializeComponent();
            Categoryview = categoryview;
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

                CategoryViewModel categoryViewModel = new CategoryViewModel()
                {
                    Name = txtName.Text,
                };

                if (!await categoryService.IsExist(categoryViewModel.Name))
                {
                    await categoryService.Create(categoryViewModel);

                    Categoryview.WindowLoad();

                    this.Close();
                }
                else
                {
                    txtError.Text = "Есть категория с таким названием";
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
