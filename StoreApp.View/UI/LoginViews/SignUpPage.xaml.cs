using MaterialDesignThemes.Wpf;
using StoreApp.Domain.Entities.Users;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using StoreApp.Service.ViewModels;
using StoreApp.View.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using XAct.Library.Settings;

namespace StoreApp.View.UI.LoginViews
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : UserControl
    {
        MainWindow targetWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
        IUserService userService = new UserService();
        public SignInPage signInPage { get; set; }
        bool isClear = false;
        public SignUpPage()
        {
            InitializeComponent();
        }

        public void GetSignInPage(SignInPage signInPage)
        {
            this.signInPage = signInPage;
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            targetWindow.AllCloseControls(1);
        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text == "")
            {
                txtLogin.Focus();
                txtErrorLogin.Text = "Необходимый";
                return;
            }
            if (txtPassword.Password == "")
            {
                txtPassword.Focus();
                txtPasswordCheck.Text = "Необходимый";
                return;
            }
            if (txtConiformPassword.Password == "")
            {
                txtConiformPassword.Focus();
                txtConiformPasswordCheck.Text = "Необходимый";
                return;
            }
            if (txtPasswordCheck.Text != "")
            {
                txtPassword.Focus();
                return;
            }
            if (txtPassword.Password != txtConiformPassword.Password)
            {
                txtError.Text = "Invalid coniform password";
                txtConiformPassword.Focus();
                return;
            }

            targetWindow.SetEffect();
            targetWindow.giff.Visibility = Visibility.Visible;

            UserSignUpViewModel userSignUpViewModel = new UserSignUpViewModel()
            {
                Login = txtLogin.Text,
                Password = HashPassword.Create(txtPassword.Password),
                ConiformPassword = HashPassword.Create(txtConiformPassword.Password)
            };


            var result = await userService.CheckUser(userSignUpViewModel.Login);

            if (result != null)
            {
                txtError.Text = "Login is already registered";
            }
            else
            {
                try
                {

                    var entity = await userService.UserSignUp(userSignUpViewModel);

                    if (entity != null)
                    {
                        signInPage.userSign = new UserSignIn()
                        {
                            Login = txtLogin.Text,
                            Password = txtPassword.Password
                        };
                        targetWindow.AllCloseControls(1);
                        
                        isClear = true;
                    }
                    else
                    {
                        throw new Exception("Xatolik iltimos boshqattan urinib ko'ring");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

            targetWindow.RemoveEffect();
            targetWindow.giff.Visibility = Visibility.Hidden;

            if (isClear)
            {
                txtLogin.Text = null;
                txtPassword.Password = null;
                txtConiformPassword.Password = null;
                txtError.Text = "";
                isClear = false;
            }
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == "" && isClear == false)
            {
                txtPasswordCheck.Text = "Необходимый";
                TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Red);
                return;
            }
            if (txtPassword.Password.Length < 8 && isClear == false)
            {
                txtPasswordCheck.Text = "Требуется минимум 8 символов";
                TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Red);
                return;
            }
            if (txtPassword.Password != "" && isClear == false)
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

                    if (txtPassword.Password != txtConiformPassword.Password && txtConiformPassword.Password != "")
                    {
                        txtConiformPasswordCheck.Text = "Неверный пароль";
                        TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Red);
                        txtConiformPasswordSucces.Visibility = Visibility.Hidden;
                    }
                    if (txtPassword.Password == txtConiformPassword.Password)
                    {
                        txtConiformPasswordCheck.Text = "";
                        TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Green);
                        txtConiformPasswordSucces.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void txtConiformPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtConiformPasswordSucces.Visibility = Visibility.Hidden;
            if (txtConiformPassword.Password == "" && isClear == false)
            {
                txtConiformPasswordCheck.Text = "Required";
                TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Red);
                return;
            }
            if (txtConiformPassword.Password.Length < 8 && isClear == false)
            {
                txtConiformPasswordCheck.Text = "Minimum 8 characters are required";
                TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Red);
                return;
            }
            if (txtConiformPassword.Password != "" && isClear == false)
            {
                var response = CheckPassword.CheckStrength(txtConiformPassword.Password);
                TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Red);

                if (response == Enums.PasswordScore.NoChar)
                {
                    txtConiformPasswordCheck.Text = "Must contain at least 1 letter";
                    return;
                }
                if (response == Enums.PasswordScore.NoNumber)
                {
                    txtConiformPasswordCheck.Text = "Must contain at least 1 digit";
                    return;
                }
                if (response == Enums.PasswordScore.NoNumberAndChar)
                {
                    txtConiformPasswordCheck.Text = "Must contain at least 1 digit and 1 letter";
                    return;
                }
                if (response == Enums.PasswordScore.Strong)
                {
                    txtConiformPasswordCheck.Text = "";
                    TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Green);
                    txtConiformPasswordSucces.Visibility = Visibility.Visible;
                }
                if (txtConiformPassword.Password != txtPassword.Password)
                {
                    txtConiformPasswordCheck.Text = "Invalid coniform password";
                    TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Red);
                    txtConiformPasswordSucces.Visibility = Visibility.Hidden;
                }
            }
        }

        private void ValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^a-zA-Z0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtLogin.Text.Length > 0)
            {
                txtErrorLogin.Text = "";
            }
            if (txtLogin.Text.Length == 0)
            {
                txtErrorLogin.Text = "Необходимый";
            }
        }
    }
}
