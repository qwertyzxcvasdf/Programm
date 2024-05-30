using Programm.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для UserCabinet.xaml
    /// </summary>
    public partial class UserCabinet : Page
    {
       
        public UserCabinet()
        {
          
         

                InitializeComponent();

       
                var client = AppData.db.Client.FirstOrDefault(c => c.login.Equals(AppData.currentUser.login));
                var family = AppData.db.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
            if (client != null)
            {

                if (client.money != null)
                    money.Text = String.Concat("Ваши деньги: ", client.money); 

                if (client.login != null)
                    login.Text = String.Concat("Логин: ", client.login);
                if (family.money != null)
                    AllMoney.Text = String.Concat("Бюджет семьи: ", family.money);
                premium.Text = client.Premium.isPremium;
            }
            else
            {

                MessageBox.Show("Информация о клиенте не найдена.");
            }

            if (client != null)
          {
                if (client.avatar == 1)
               {
                   man.Visibility = Visibility.Visible;
                   kid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    man.Visibility = Visibility.Collapsed;
                    kid.Visibility = Visibility.Visible;
                }
            }


            Loaded += This_Loaded;
        }

        private void This_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Height = 450;
            Window.GetWindow(this).Width = 800;
            var currentUser = AppData.currentUser;
            Role role = AppData.db.Role.FirstOrDefault(w => w.id == currentUser.role);
            if (role.id.Equals(1))
            {
                Refr_Копировать.Visibility = Visibility.Collapsed;
                Refr_Копировать1.Visibility = Visibility.Collapsed;
                Refr_Копировать2.Visibility = Visibility.Collapsed;
                familyMember2.Visibility = Visibility.Collapsed;
                familyMember1.Visibility = Visibility.Collapsed;
                familyMember3.Visibility = Visibility.Collapsed;
                TxbMemberMoney2.Visibility = Visibility.Collapsed;
                TxbMemberMoney1.Visibility = Visibility.Collapsed;
                TxbMemberMoney3.Visibility = Visibility.Collapsed;
                FamilyGrid.Visibility = Visibility.Collapsed;
                balance_2.Visibility = Visibility.Collapsed;
                balance.Visibility = Visibility.Collapsed;
                balance_3.Visibility = Visibility.Collapsed;
                premium.Visibility = Visibility.Collapsed;
                premBuy.Visibility = Visibility.Collapsed;
                SaveCNG.Visibility = Visibility.Collapsed;
                GiveMoney3.Visibility = Visibility.Collapsed;
                GiveMoney2.Visibility = Visibility.Collapsed;
                GiveMoney.Visibility = Visibility.Collapsed;
                AddMember.Visibility = Visibility.Collapsed;
                AddMember_Копировать.Visibility = Visibility.Collapsed;
                MoneyPNG.Visibility = Visibility.Visible;
               
            }

                FamilyGrid.ItemsSource = AppData.db.Client.Where(c => c.familyID == AppData.currentUser.familyID).ToList();
            var currentFamily = AppData.db.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
              TxbFamilyName.Text = currentFamily.name;
            var client = AppData.db.Client.FirstOrDefault(c => c.login.Equals(AppData.currentUser.login));
            var family = AppData.db.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
            if (client != null)
            {

                if (client.money != null)
                    money.Text = String.Concat("Ваши деньги: ", client.money); ;

                if (client.login != null)
                    login.Text = String.Concat("Логин: ", client.login);
                if (family.money != null)
                    AllMoney.Text = String.Concat("Бюджет семьи: ", family.money);
            }
            else
            {

                MessageBox.Show("Информация о клиенте не найдена.");
            }
            using (var context = new businessBDEntities())
            {
                // Получаем текущий familyID пользователя
                int familyID = AppData.currentUser.familyID;

                // Получаем список клиентов с тем же familyID
                var familyMembers = context.Client
                                           .Where(c => c.familyID == familyID)
                                           .OrderBy(c => c.id) // Упорядочиваем по id или по другому критерию
                                           .ToList();

                // Устанавливаем значения TextBlock для первых трех членов семьи
                if (familyMembers.Count > 0)
                {
                    familyMember1.Text = familyMembers[0].nickname;
                    TxbMemberMoney1.Text = familyMembers[0].money;
                }
                    
                    
                else
                    familyMember1.Text = "Член семьи не найден";

                if (familyMembers.Count > 1)
                {
                    familyMember2.Text = familyMembers[1].nickname;
                    TxbMemberMoney2.Text = familyMembers[1].money;
                }
                    
                else
                    familyMember2.Text = "Член семьи не найден";

                if (familyMembers.Count > 2)
                {
                    familyMember3.Text = familyMembers[2].nickname;
                    TxbMemberMoney3.Text = familyMembers[2].money;
                }
                   
                else
                    familyMember3.Text = "Член семьи не найден";
            }
            
            if (AppData.currentUser.isPremium.Equals(1))
            {
                premBuy.Visibility = Visibility.Collapsed;
            }
        }



        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignInPage());
        }

     

        private void BtnAddMember1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddFamilyMember());
        }

     

      

        private void SaveCNG_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new businessBDEntities())
            {
                try
                {
                 

                    var currentFamily = context.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
                    if (currentFamily == null)
                    {
                        MessageBox.Show("Текущая семья не найдена.");
                        return;
                    }

                    string nameFamily = TxbFamilyName.Text;

                    currentFamily.name = nameFamily;

                    // Сохраняем изменения в базе данных
                    int result2 = context.SaveChanges();
                    if (result2 > 0)
                    {
                        MessageBox.Show("Успешно");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: изменения не были сохранены.");
                    }

                    TxbFamilyName.Text = currentFamily.name;
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Общая ошибка: {ex.Message}");
                }
            }
        }





        private void BtnGiveMoney1_Click(object sender, RoutedEventArgs e)
        {
            

            using (var context = new businessBDEntities())
            {
                int familyID = AppData.currentUser.familyID;

                // Получаем список клиентов с тем же familyID
                var familyMembers = context.Client
                                           .Where(c => c.familyID == familyID)
                                           .OrderBy(c => c.id) // Упорядочиваем по id или по другому критерию
                                           .ToList();

           
                try
                {

                    int NewMemberBalance = int.Parse(TxbMemberMoney1.Text);
                    int OldMemberBalance = int.Parse(familyMembers[0].money);



                    //|||||||||||||||||||||||||||||||||||

                    try
                    {


                        var currentFamily = context.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
                        if (currentFamily == null)
                        {
                            MessageBox.Show("Текущая семья не найдена.");
                            return;
                        }

                        int familyBalance = int.Parse(currentFamily.money) -(NewMemberBalance - OldMemberBalance) ;

                        currentFamily.money = (familyBalance).ToString();

                        // Сохраняем изменения в базе данных
                        int result3 = context.SaveChanges();
                        if (result3 > 0)
                        {
                            MessageBox.Show("Успешно");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка: изменения не были сохранены.");
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
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Общая ошибка: {ex.Message}");
                    }


                    //|||||||||||||||||||||||||||||||||||||






                    familyMembers[0].money = (NewMemberBalance).ToString();

                    // Сохраняем изменения в базе данных
                    int result2 = context.SaveChanges();
                    if (result2 > 0)
                    {
                        
                        context.Payment.Add(new Payment
                        {

                            type = 3,
                            cost = (NewMemberBalance - OldMemberBalance).ToString(),
                            paymentLogin = AppData.currentUser.login,



                        });
                        int result4 = context.SaveChanges();
                        if (result4 > 0)
                        {
                            int maxId = context.Payment.Max(u => (int)u.id);
                            var currentPayment = context.Payment.FirstOrDefault(p => p.id.Equals(maxId));
                            MessageBox.Show("Чек № " + (currentPayment.id).ToString() + Environment.NewLine +
                                "Тип " + currentPayment.type + Environment.NewLine +
                               "На сумму " + currentPayment.cost + Environment.NewLine +
                               "Пользователем " + currentPayment.paymentLogin);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка чека");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: изменения не были сохранены.");
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Общая ошибка: {ex.Message}");
                }
                var client = AppData.db.Client.FirstOrDefault(c => c.login.Equals(AppData.currentUser.login));
                var family = AppData.db.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
                if (client != null)
                {

                    if (client.money != null)
                        money.Text = String.Concat("Ваши деньги: ", client.money); ;

                    if (client.login != null)
                        login.Text = String.Concat("Логин: ", client.login);
                    if (family.money != null)
                        AllMoney.Text = String.Concat("Бюджет семьи: ", family.money);
                }
                else
                {

                    MessageBox.Show("Информация о клиенте не найдена.");
                }
            }
        }

        private void BtnGiveMoney2_Click(object sender, RoutedEventArgs e)
        {


            using (var context = new businessBDEntities())
            {
                int familyID = AppData.currentUser.familyID;

                // Получаем список клиентов с тем же familyID
                var familyMembers = context.Client
                                           .Where(c => c.familyID == familyID)
                                           .OrderBy(c => c.id) // Упорядочиваем по id или по другому критерию
                                           .ToList();


                try
                {

                    int NewMemberBalance = int.Parse(TxbMemberMoney2.Text);
                    int OldMemberBalance = int.Parse(familyMembers[1].money);



                    //|||||||||||||||||||||||||||||||||||

                    try
                    {


                        var currentFamily = context.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
                        if (currentFamily == null)
                        {
                            MessageBox.Show("Текущая семья не найдена.");
                            return;
                        }

                        int familyBalance = int.Parse(currentFamily.money) - (NewMemberBalance - OldMemberBalance);

                        currentFamily.money = (familyBalance).ToString();

                        // Сохраняем изменения в базе данных
                        int result3 = context.SaveChanges();
                        if (result3 > 0)
                        {
                            MessageBox.Show("Успешно");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка: изменения не были сохранены.");
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
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Общая ошибка: {ex.Message}");
                    }


                    //|||||||||||||||||||||||||||||||||||||






                    familyMembers[1].money = (NewMemberBalance).ToString();

                    // Сохраняем изменения в базе данных
                    int result2 = context.SaveChanges();
                    if (result2 > 0)
                    {

                        context.Payment.Add(new Payment
                        {

                            type = 3,
                            cost = (NewMemberBalance - OldMemberBalance).ToString(),
                            paymentLogin = AppData.currentUser.login,



                        });
                        int result4 = context.SaveChanges();
                        if (result4 > 0)
                        {
                            int maxId = context.Payment.Max(u => (int)u.id);
                            var currentPayment = context.Payment.FirstOrDefault(p => p.id.Equals(maxId));


                            var client = AppData.currentUser;
                            var family = AppData.db.Family.FirstOrDefault(f => f.id.Equals(familyID));
                           


                            MessageBox.Show("Чек № " + (currentPayment.id).ToString() + Environment.NewLine +
                                "Тип " + currentPayment.type + Environment.NewLine +
                               "На сумму " + currentPayment.cost + Environment.NewLine +
                               "Пользователем " + currentPayment.paymentLogin);
                            


                            

                            if (client.money != null)
                                    money.Text = String.Concat("Ваши деньги: ", client.money); ;

                                if (client.login != null)
                                    login.Text = String.Concat("Логин: ", client.login);
                                if (family.money != null)
                                    AllMoney.Text = String.Concat("Бюджет семьи: ", family.money);
                            
                        }
                        else
                        {
                            MessageBox.Show("Ошибка чека");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: изменения не были сохранены.");
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Общая ошибка: {ex.Message}");
                }
                context.SaveChanges();
                
                var family2 = AppData.db.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
             
                
            }
            
        }

        private void BtnGiveMoney3_Click(object sender, RoutedEventArgs e)
        {


            using (var context = new businessBDEntities())
            {
                int familyID = AppData.currentUser.familyID;

                // Получаем список клиентов с тем же familyID
                var familyMembers = context.Client
                                           .Where(c => c.familyID == familyID)
                                           .OrderBy(c => c.id) // Упорядочиваем по id или по другому критерию
                                           .ToList();


                try
                {

                    int NewMemberBalance = int.Parse(TxbMemberMoney3.Text);
                    int OldMemberBalance = int.Parse(familyMembers[2].money);



                    //|||||||||||||||||||||||||||||||||||

                    try
                    {


                        var currentFamily = context.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
                        if (currentFamily == null)
                        {
                            MessageBox.Show("Текущая семья не найдена.");
                            return;
                        }

                        int familyBalance = int.Parse(currentFamily.money) - (NewMemberBalance - OldMemberBalance);

                        currentFamily.money = (familyBalance).ToString();

                        // Сохраняем изменения в базе данных
                        int result3 = context.SaveChanges();
                        if (result3 > 0)
                        {
                            MessageBox.Show("Успешно");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка: изменения не были сохранены.");
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
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Общая ошибка: {ex.Message}");
                    }


                    //|||||||||||||||||||||||||||||||||||||






                    familyMembers[2].money = (NewMemberBalance).ToString();

                    // Сохраняем изменения в базе данных
                    int result2 = context.SaveChanges();
                    if (result2 > 0)
                    {

                        context.Payment.Add(new Payment
                        {

                            type = 3,
                            cost = (NewMemberBalance - OldMemberBalance).ToString(),
                            paymentLogin = AppData.currentUser.login,



                        });
                        int result4 = context.SaveChanges();
                        if (result4 > 0)
                        {
                            int maxId = context.Payment.Max(u => (int)u.id);
                            var currentPayment = context.Payment.FirstOrDefault(p => p.id.Equals(maxId));
                            MessageBox.Show("Чек № " + (currentPayment.id).ToString() + Environment.NewLine +
                                "Тип " + currentPayment.type + Environment.NewLine +
                               "На сумму " + currentPayment.cost + Environment.NewLine +
                               "Пользователем " + currentPayment.paymentLogin);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка чека");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: изменения не были сохранены.");
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Общая ошибка: {ex.Message}");
                }
                var client = AppData.db.Client.FirstOrDefault(c => c.login.Equals(AppData.currentUser.login));
                var family = AppData.db.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
                if (client != null)
                {

                    if (client.money != null)
                        money.Text = String.Concat("Ваши деньги: ", client.money); ;

                    if (client.login != null)
                        login.Text = String.Concat("Логин: ", client.login);
                    if (family.money != null)
                        AllMoney.Text = String.Concat("Бюджет семьи: ", family.money);
                }
                else
                {

                    MessageBox.Show("Информация о клиенте не найдена.");
                }
            }
        }

        private void Refr_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new businessBDEntities())
            {
                NavigationService.Navigate(new UserCabinet());
                MessageBox.Show("Обновлено");
            }
        }

        private void BtnAddCard_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CardAddPage());
        }

        private void PremBuy_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new businessBDEntities())
            {
                var currentUser = AppData.currentUser;
            currentUser.isPremium = 1;
                int currentMoney = int.Parse(currentUser.money) - 5000;
                currentUser.money = currentMoney.ToString();
                premium.Text = currentUser.Premium.isPremium;
                
                int result4 = context.SaveChanges();
                if (result4 > 0)
                {
                    int maxId = context.Payment.Max(u => (int)u.id);
                    var currentPayment = context.Payment.FirstOrDefault(p => p.id.Equals(maxId));
                    MessageBox.Show("Чек № "); MessageBox.Show("Куплено");
                }
                else
                {
                   
                    MessageBox.Show(currentUser.money);
                }
            }
        }

        private void Car_Click(object sender, RoutedEventArgs e)
        {
            AllMoneyCar.Visibility = Visibility.Visible;
            var family = AppData.db.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
            AllMoneyCar.Text = (family.money +"/1000000");
        }

        private void Phone_Click(object sender, RoutedEventArgs e)
        {
            AllMoneyCar.Visibility = Visibility.Visible;   
            var family = AppData.db.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
            AllMoneyCar.Text = (family.money + "/30000");
        }

        private void Ear_Click(object sender, RoutedEventArgs e)
        {
            AllMoneyCar.Visibility = Visibility.Visible;
            var family = AppData.db.Family.FirstOrDefault(f => f.id.Equals(AppData.currentUser.familyID));
            if (int.Parse(family.money) > 5000)
                
            { 
                AllMoneyCar.Text = (family.money + "/5000" +" Вам хватает");
            }
            else { AllMoneyCar.Text = (family.money + "/5000"); }
            
        }
    }

}
