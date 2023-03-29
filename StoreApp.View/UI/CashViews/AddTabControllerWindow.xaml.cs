using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
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
