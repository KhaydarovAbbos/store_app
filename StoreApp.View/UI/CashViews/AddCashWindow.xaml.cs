using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
using StoreApp.View.UI.StoreViews;
using System;
using System.Collections.Generic;
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

namespace StoreApp.View.UI.CashViews
{
    /// <summary>
    /// Логика взаимодействия для AddCashWindow.xaml
    /// </summary>
    public partial class AddCashWindow : Window
    {
        CashMainView CashMainview { get; set; }
        ICashService cashService = new CashService();
        StoreService storeService = new StoreService();
        long StoreId;

        public AddCashWindow(CashMainView cashMainView, long storeId)
        {
            InitializeComponent();
            CashMainview = cashMainView;
            StoreId = storeId;
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

                var store = await storeService.Get(StoreId);

                CashViewModel cashViewView = new CashViewModel()
                {
                    Name = txtName.Text,
                    StoreId = store.Id,
                    StoreName = store.Name                    
                };

                if (!await cashService.IsExist(cashViewView.Name))
                {
                    var response = await cashService.Create(cashViewView);

                    CashMainview.WindowLoad();
                    this.Close();

                }
                else
                {
                    txtError.Text = "Есть касса с таким названием";
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
