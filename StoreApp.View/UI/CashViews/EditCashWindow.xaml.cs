using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.View.UI.MainViews;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StoreApp.View.UI.CashViews
{
    /// <summary>
    /// Логика взаимодействия для EditCashWindow.xaml
    /// </summary>
    public partial class EditCashWindow : Window
    {
        CashMainView CashMainview { get; set; }
        long CashId;
        ICashService cashService = new CashService();
        IStoreService storeService = new StoreService();

        public EditCashWindow()
        {
            InitializeComponent();
        }

        public async void WindowLoad(long id, CashMainView cashMainView)
        {
            CashId = id;

            var store = await cashService.Get(id);

            txtName.Text = store.Name;
            CashMainview = cashMainView;
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

                var store = await storeService.Get(long.Parse(StoreMainView.StoreId));

                Cash cash = new Cash()
                {
                    Id = CashId,
                    Name = txtName.Text,
                    StoreName = store.Name,
                };

                if (!await cashService.IsExist(cash.Name, store.Id))
                {
                    var result = await cashService.Update(cash);

                    CashMainview.WindowLoad();
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
