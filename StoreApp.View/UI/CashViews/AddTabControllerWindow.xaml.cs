using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StoreApp.View.UI.CashViews
{
    /// <summary>
    /// Логика взаимодействия для AddTabControllerWindow.xaml
    /// </summary>
    public partial class AddTabControllerWindow : Window
    {
        ITabControlService tabControlService = new TabControlService();
        CashView Cashview;

        public AddTabControllerWindow(CashView cashView)
        {
            InitializeComponent();

            Cashview = cashView;

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

                TabController model = new TabController()
                {
                    Name = txtName.Text,
                };

                if (!await tabControlService.IsExist(model.Name))
                {
                    var response = await tabControlService.Create(model);

                    Cashview.WindowLoad();
                    this.Close();

                }
                else
                {
                    txtError.Text = "Есть контроль с таким названием";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
