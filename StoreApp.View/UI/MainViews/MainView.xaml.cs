using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StoreApp.View.UI.MainViews
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        MainWindow Mainwindow;
        public static int gridColumn1Width = 200, gridColumn2Width = 1120;

        public MainView()
        {
            InitializeComponent();
        }

        public void GetMainWindow(MainWindow mainWindow)
        {
            Mainwindow = mainWindow;

            MainGrid.ColumnDefinitions[0].Width = new GridLength(StoreMainView.gridColumn1Width, GridUnitType.Pixel);
            MainGrid.ColumnDefinitions[1].Width = new GridLength(StoreMainView.gridColumn2Width, GridUnitType.Pixel);
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

        private void shops_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AllCloseControls(1);
        }

        public void AllCloseControls(int i)
        {
            shop_view.Visibility = Visibility.Hidden;
            store_main_view.Visibility = Visibility.Hidden;

            if (i == -1)
            {
                this.Visibility = Visibility.Visible;
            }
            if (i == 1)
            {
                shop_view.Visibility = Visibility.Visible;
                shop_view.GetMainView(this);
                shop_view.WindowLoad();
            }
            if (i == 2)
            {
                Mainwindow.AllCloseControls(4);
            }

        }

        private void settings_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AllCloseControls(-1);
        }

        private void shops_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void exit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mainwindow.AllCloseControls(1);

        }
    }
}
