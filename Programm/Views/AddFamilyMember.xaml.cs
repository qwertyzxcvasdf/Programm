using Programm.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AddFamilyMember.xaml
    /// </summary>
    public partial class AddFamilyMember : Page
    {
        public AddFamilyMember()
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
                string nick = NickBox.Text;
                string password = passwordBox.Password;


                context.Client.Add(new Client
                {

                    login = username,
                    password = password,
                    nickname = nick,
                    role = 1,
                    money = "0",
                    familyID = AppData.currentUser.familyID,
                    avatar = 2,
                    isPremium = 2



                });





                try
                {


                    int result = context.SaveChanges();
                    if (result > 0)
                    {
                        MessageBox.Show("Добавлен");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                }
                
            }
        }

        private void BtnToUC_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserCabinet());
        }
    }
}
