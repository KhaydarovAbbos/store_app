using MaterialDesignThemes.Wpf;
using StoreApp.Domain.Entities.Users;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
using StoreApp.View.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StoreApp.View.UI.LoginViews
{
    /// <summary>
    /// Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class SignInPage : UserControl
    {
        MainWindow targetWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
        public UserSignIn userSign { get; set; }
        IList<UserSignIn> userSignIns = new List<UserSignIn>();
        UserService userService = new UserService();

        public SignInPage()
        {
            InitializeComponent();
        }

        public void LoadWindow()
        {
            if (userSign != null)
            {
                txtLogin.Text = userSign.Login;
                txtPassword.Password = userSign.Password;
            }
            else
            {
                userSignIns = ReadFileHelper.GetUsers();

                if (userSignIns != null && userSignIns.Count > 0)
                {
                    var user = userSignIns.Last();

                    if (user != null)
                    {
                        txtLogin.Text = user.Login;
                        txtPassword.Password = user.Password;
                        ckRememberMe.IsChecked = true;
                    }
                }
            }
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text == "")
            {
                txtLoginCheck.Text = "Необходимый";

                txtLogin.Focus();
                return;
            }
            if (txtPassword.Password == "")
            {
                txtPasswordCheck.Text = "Необходимый";
                txtPassword.Focus();
                return;
            }

            targetWindow.SetEffect();
            targetWindow.giff.Visibility = Visibility.Visible;



            UserLoginViewModel userLoginViewModel = new UserLoginViewModel()
            {
                Login = txtLogin.Text,
                Password = HashPassword.Create(txtPassword.Password)

            };

            var result = await userService.UserLogin(userLoginViewModel);

            if (result != null)
            {
                targetWindow.AllCloseControls(3);

                txtError.Text = "";

                if (ckRememberMe.IsChecked == true)
                {
                    UserSignIn user = new UserSignIn()
                    {
                        Login = txtLogin.Text,
                        Password = txtPassword.Password
                    };

                    if (userSignIns.FirstOrDefault(x => x.Login == user.Login && x.Password == user.Password) == null)
                    {
                        using (StreamWriter writer = new StreamWriter(App.FilePath, true))
                        {
                            writer.WriteLine(txtLogin.Text + " " + txtPassword.Password);
                        }
                    }
                }
            }
            else
            {
                txtError.Text = "Invalid username or password.";
            }

            targetWindow.RemoveEffect();
            targetWindow.giff.Visibility = Visibility.Hidden;
            userSign = null;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            targetWindow.AllCloseControls(2);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == "")
            {
                txtPasswordCheck.Text = "Необходимый";
                TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Red);
                return;
            }
            if (txtPassword.Password.Length < 8)
            {
                txtPasswordCheck.Text = "Требуется минимум 8 символов";
                TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Red);
                return;
            }
            if (txtPassword.Password != "")
            {
                var response = CheckPassword.CheckStrength(txtPassword.Password);
                TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Red);

                if (response == Enums.PasswordScore.NoChar)
                {
                    txtPasswordCheck.Text = "Должен содержать хотя бы 1 букву";
                    return;
                }
                if (response == Enums.PasswordScore.NoNumber)
                {
                    txtPasswordCheck.Text = "Должен содержать хотя бы 1 цифру";
                    return;
                }
                if (response == Enums.PasswordScore.NoNumberAndChar)
                {
                    txtPasswordCheck.Text = "Должен содержать как минимум 1 цифру и 1 букву";
                    return;
                }
                if (response == Enums.PasswordScore.Strong)
                {
                    txtPasswordCheck.Text = "";
                    TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Green);
                }
            }
        }

        private void txtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtLogin.Text.Length > 0)
            {
                txtLoginCheck.Text = "";
            }
            if (txtLogin.Text.Length == 0)
            {
                txtLoginCheck.Text = "Необходимый";
            }
        }
    }
}
