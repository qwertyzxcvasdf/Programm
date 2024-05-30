using Programm.Model;
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

namespace Programm.Views
{
    /// <summary>
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();
            Loaded += This_Loaded;
        }
        private void This_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Height = 450;
            Window.GetWindow(this).Width = 400;
        }
        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = AppData.db.Client.FirstOrDefault(u => u.login.Equals(TxbLogin.Text) && u.password.Equals(TxbPassword.Password));
            MessageBox.Show("Добро пожаловать " + currentUser.login );

            if (currentUser != null)
            {
                Role role = AppData.db.Role.FirstOrDefault(w => w.id == currentUser.role);
                if (role.id.Equals(3))
                {
                    AppData.currentUser = currentUser;
                    NavigationService.Navigate(new DataPage());
                    MessageBox.Show("Ур Тех. Поддержка");
                }
                else if (role.id.Equals(2))
                {
                    AppData.currentUser = currentUser;
                   

                    NavigationService.Navigate(new UserCabinet());
                }
                else
                {
                    // Сохранение текущего пользователя в приложении
                    AppData.currentUser = currentUser;

                    // Переход на страницу WelcomePage

                    NavigationService.Navigate(new UserCabinet());
                }
            }
            else
            {
                MessageBox.Show("Данного пользователя не существует", "Ошибка");
            }
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUpPage());
        }
    }

}
