using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

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

                if (!await cashService.IsExist(cashViewView.Name, store.Id))
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
