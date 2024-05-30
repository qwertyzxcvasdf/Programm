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
    /// Логика взаимодействия для CardAddPage.xaml
    /// </summary>
    public partial class CardAddPage : Page
    {
        public CardAddPage()
        {
            InitializeComponent();
            Loaded += This_Loaded;
        }

        private void This_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Height = 350;
            Window.GetWindow(this).Width = 400;
            if (AppData.currentUser.card != null)
            {

            
            DataBox.Text = AppData.currentUser.Card1.date;
            CVVBox.Password = AppData.currentUser.Card1.cvv;
            numberBox.Text = AppData.currentUser.Card1.number;
            }
        }


        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new businessBDEntities())
            {
                string number = numberBox.Text;
                string cvv = CVVBox.Password;
                string data = DataBox.Text;


                context.Card.Add(new Card
                {

                    number = number,
                    date = data,
                    cvv = cvv,
                 



                });
                




                try
                {


                    int result = context.SaveChanges();
                    if (result > 0)
                    {
                        MessageBox.Show("Успешно оплачено");
                        var currentFamily = context.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
                        int Money = int.Parse(currentFamily.money) + 10000;
                        currentFamily.money = Money.ToString();
                        context.SaveChanges();
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
