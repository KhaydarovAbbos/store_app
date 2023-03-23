using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
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

                await categoryService.Update(subCategory);

                ProductsubCategoryView.WindowLoad();
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var subcategory = await categoryService.Get(subCategoryid);

            txtName.Text = subcategory.Name;
        }
    }
}
