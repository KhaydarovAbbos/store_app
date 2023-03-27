using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
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
    /// Логика взаимодействия для EditStoreWindow.xaml
    /// </summary>
    public partial class EditStoreWindow : Window
    {
        StoreView Storeview { get; set; }
        long ShopId;
        IStoreService storeService = new StoreService();

        public EditStoreWindow()
        {
            InitializeComponent();
        }

        public async void WindowLoad(long id, StoreView storeView)
        {
            ShopId = id;

            var store = await storeService.Get(id);

            txtName.Text = store.Name;
            Storeview = storeView;
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

                Store store = new Store()
                {
                    Id = ShopId,
                    Name = txtName.Text
                };


                if (!await storeService.IsExist(store.Name))
                {
                    var result = await storeService.Update(store);

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
