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
    /// Логика взаимодействия для DataPage.xaml
    /// </summary>
    public partial class DataPage : Page
    {
        public DataPage()
        {
            InitializeComponent();
            Loaded += This_Loaded;
        }

        private void This_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Height = 550;
            Window.GetWindow(this).Width = 800;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignInPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            UsersGrid.ItemsSource = AppData.db.Payment.ToList();
        }

     
        private void RemoveBtn_click(object sender, RoutedEventArgs e)
        {
            var currentUser = AppData.currentUser;
            Role role = AppData.db.Role.FirstOrDefault(w => w.id == currentUser.role);
            if (role.id.Equals(3))
            {
                if (MessageBox.Show("Удалить", "уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var CurrentPayment = UsersGrid.SelectedItem as Payment;
                    AppData.db.Payment.Remove(CurrentPayment);

                    AppData.db.SaveChanges();
                    UsersGrid.ItemsSource = AppData.db.Payment.ToList();
                    MessageBox.Show("Сделано");
                }
            }
     
 

        }

        private void AddBtn_click(object sender, RoutedEventArgs e)
        {
            var currentUser = AppData.currentUser;
            string rolee = (currentUser.role).ToString();
            int Rolee;
            if (!int.TryParse(rolee, out Rolee))
            {
                // Если преобразование не удалось, выводим сообщение об ошибке
                MessageBox.Show("1 - Пользователь, 2 - Админ");

            }
            
            if (Rolee.Equals(3))
            {
                NavigationService.Navigate(new AddUserPage());
            }
     
            
        }

        private void ChangeBtn_click(object sender, RoutedEventArgs e)
        {
            var currentUser = AppData.currentUser;
            Role role = AppData.db.Role.FirstOrDefault(w => w.id == currentUser.role);
            if (role.id.Equals(3))
            {
                if (MessageBox.Show("Изменить?", "уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AppData.db.SaveChanges();
                    UsersGrid.ItemsSource = AppData.db.Payment.ToList();
                    MessageBox.Show("Сделано");
                }
                AppData.db.SaveChanges();
            }
       

        }


    }
}
