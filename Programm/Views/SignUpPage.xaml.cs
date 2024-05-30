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
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
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
               

                

                var maxId = context.Client.Max(u => (int)u.id);
                int familyId = maxId + 1;
                context.Family.Add(new Family
                {

                    id  = familyId,
                    name = "Название семьи",
                    money = "0",
        


                });






                int result = context.SaveChanges();

                if (result > 0)
                {
                    MessageBox.Show("Семья создана");
                    string username = usernameBox.Text;
                    string password = passwordBox.Password;

                    string nicknameRand = "Взрослый" + maxId;
                    context.Client.Add(new Client
                    {

                        login = username,
                        password = password,
                        nickname = nicknameRand,
                        money = "0",
                        role = 2,
                        avatar = 1,
                        familyID = familyId,
                        isPremium = 1,

                    }) ;






                    int result2 = context.SaveChanges();
                    if (result2 > 0)
                    {
                        MessageBox.Show("Успешно");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }

                
                

              
            }
        }

        private void BtnAut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignInPage());
        }
    }
}
