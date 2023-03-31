using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace StoreApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlurEffect myEffect = new BlurEffect();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        public void SetEffect()
        {
            myEffect.Radius = 5;
            Effect = myEffect;
            giff.Visibility = Visibility.Visible;
        }

        public void RemoveEffect()
        {
            myEffect.Radius = 0;
            Effect = myEffect;
            giff.Visibility = Visibility.Hidden;
        }

        public void AllCloseControls(int i)
        {

            sign_in_view.Visibility = Visibility.Hidden;
            sign_up_view.Visibility = Visibility.Hidden;
            main_view.Visibility = Visibility.Hidden;
            store_main_view.Visibility = Visibility.Hidden;
            cash_view.Visibility = Visibility.Hidden;

            if (i == 1)
            {
                sign_in_view.Visibility = Visibility.Visible;
                sign_in_view.LoadWindow();
            }
            if (i == 2)
            {
                sign_up_view.Visibility = Visibility.Visible;
                sign_up_view.GetSignInPage(sign_in_view);
            }
            if (i == 3)
            {
                main_view.Visibility = Visibility.Visible;
                main_view.GetMainWindow(this);
            }
            if (i == 4)
            {
                store_main_view.Visibility = Visibility.Visible;
                store_main_view.GetMainWindow(this);
            }
            if (i == 5)
            {
                cash_view.Visibility = Visibility.Visible;
                cash_view.GetmainWindow(this);
                cash_view.WindowLoad();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AllCloseControls(1);
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cash_view.Visibility == Visibility.Visible)
            {
                int count = cash_view.panelProduct.Children.Count;

                if (count !=  0)
                {
                    e.Cancel = true;
                    await cash_view.WindowClose();
                    AllCloseControls(-1);
                    this.Close();
                }
            }
        }
    }
}
