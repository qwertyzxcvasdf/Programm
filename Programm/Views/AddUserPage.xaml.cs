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
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        public AddUserPage()
        {
            InitializeComponent();
                Loaded += This_Loaded;
        }

        private void This_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Height = 350;
            Window.GetWindow(this).Width = 400;
        }


        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new businessBDEntities())
            {
                string username = usernameBox.Text;
                string password = passwordBox.Password;

                    
                        context.Client.Add(new Client
                        {

                            login = username,
                            password = password,
                            nickname = "ТехПод",
                            role = 3,
                            avatar = 1,
                            isPremium = 1,
                            familyID = 9999,
                            money= "999",


                        });
                    
                
                
               


                int result = context.SaveChanges();

                if (result > 0)
                {
                    MessageBox.Show("ТехПод добавлен");
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }

        private void BtnToDataPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DataPage());
        }
    }
}
