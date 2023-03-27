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

namespace StoreApp.View.UI.StoreViews
{
    /// <summary>
    /// Логика взаимодействия для AddStoreWindow.xaml
    /// </summary>
    public partial class AddStoreWindow : Window
    {
        StoreView Storeview { get; set; }
        IStoreService storeService = new StoreService();

        public AddStoreWindow(StoreView storeView)
        {
            InitializeComponent();
            Storeview = storeView;
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

                StoreViewModel storeView = new StoreViewModel()
                {
                    Name = txtName.Text,
                };

                if (!await storeService.IsExist(storeView.Name))
                {
                    var response = await storeService.Create(storeView);

                    Storeview.WindowLoad();
                    this.Close();

                }
                else
                {
                    txtError.Text = "Есть магазин с таким названием";
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
