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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoreApp.View.UI.MainViews
{
    /// <summary>
    /// Логика взаимодействия для StoreMainView.xaml
    /// </summary>
    public partial class StoreMainView : UserControl
    {
        MainWindow Mainwindow;
        public static string Storename, StoreId = "0";
        public static int gridColumn1Width = 200, gridColumn2Width = 1120;

        public StoreMainView()
        {
            InitializeComponent();
        }

        public void GetMainWindow(MainWindow mainWindow)
        {
            Mainwindow = mainWindow;

            txtStoreName.Text = Storename;
            store_id.Content = StoreId;

            MainGrid.ColumnDefinitions[0].Width = new GridLength(MainView.gridColumn1Width, GridUnitType.Pixel);
            MainGrid.ColumnDefinitions[1].Width = new GridLength(MainView.gridColumn2Width, GridUnitType.Pixel);

        }

        private void create_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AllCloseControls(1);
        }

        public void AllCloseControls(int i)
        {

            productcategory_view.Visibility = Visibility.Hidden;
            productSubCategory_view.Visibility = Visibility.Hidden;
            products_view.Visibility = Visibility.Hidden;
            insert_products_view.Visibility = Visibility.Hidden;

            if (i == -1)
            {
                Mainwindow.AllCloseControls(3);

                nameCategory.Visibility = Visibility.Hidden;
                nameSubCategory.Visibility = Visibility.Hidden;
            }
            if (i == 1)
            {
                productcategory_view.Visibility = Visibility.Visible;
                productcategory_view.GetMainView(this);
                productcategory_view.WindowLoad();

                nameCategory.Visibility = Visibility.Hidden;
                nameSubCategory.Visibility = Visibility.Hidden;
            }
            if (i == 2)
            {
                productSubCategory_view.Visibility = Visibility.Visible;
                productSubCategory_view.GetMainView(this);
                productSubCategory_view.WindowLoad();
            }
            if (i == 3)
            {
                products_view.Visibility = Visibility.Visible;
                products_view.GetMainView(this);
                products_view.WindowLoad();
            }
            if (i == 4)
            {
                insert_products_view.Visibility = Visibility.Visible;
                insert_products_view.WindowLoad();
            }
        }

        private void fastInsert_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AllCloseControls(4);
        }

        private void main_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AllCloseControls(-1);

            txtStoreName.Text = "";
            store_id.Content = "0";
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            if (MainGrid.ColumnDefinitions[0].Width.Value == 200)
            {
                MainGrid.ColumnDefinitions[0].Width = new GridLength(60, GridUnitType.Pixel);
                MainGrid.ColumnDefinitions[1].Width = new GridLength(1260, GridUnitType.Pixel);

                gridColumn1Width = 60;
                gridColumn2Width = 1260;
            }
            else
            {
                MainGrid.ColumnDefinitions[0].Width = new GridLength(200, GridUnitType.Pixel);
                MainGrid.ColumnDefinitions[1].Width = new GridLength(1120, GridUnitType.Pixel);

                gridColumn1Width = 200;
                gridColumn2Width = 1120;
            }
        }
    }
}
