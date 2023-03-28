using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
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
    /// Логика взаимодействия для EditCashWindow.xaml
    /// </summary>
    public partial class EditCashWindow : Window
    {
        CashMainView CashMainview { get; set; }
        long CashId;
        ICashService cashService = new CashService();

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

                Cash store = new Cash()
                {
                    Id = CashId,
                    Name = txtName.Text
                };


                if (!await cashService.IsExist(store.Name))
                {
                    var result = await cashService.Update(store);

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
