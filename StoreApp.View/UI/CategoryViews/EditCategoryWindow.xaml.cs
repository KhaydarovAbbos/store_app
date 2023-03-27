using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
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
using XAct.Library.Settings;

namespace StoreApp.View.UI.CategoryViews
{
    /// <summary>
    /// Логика взаимодействия для EditCategoryWindow.xaml
    /// </summary>
    public partial class EditCategoryWindow : Window
    {

        CategoryView ProductCategory;
        long Categoryid;
        ICategoryService categoryService = new CategoryService();

        public EditCategoryWindow(CategoryView Categoryview, long id)
        {
            InitializeComponent();
            ProductCategory = Categoryview;
            Categoryid = id;
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

                Category category = new Category()
                {
                    Id = Categoryid,
                    Name = txtName.Text,
                };

                if (!await categoryService.IsExist(category.Name))
                {
                    await categoryService.Update(category);

                    ProductCategory.WindowLoad();
                    this.Close();
                }
                else
                {
                    txtError.Text = "Есть категория с таким названием";
                }

                
            }
            catch (Exception)
            {

                throw;
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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var category = await categoryService.Get(Categoryid);

            if (category != null)
                txtName.Text = category.Name;

        }
    }
}
