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
using ООО__Ароматный_мир_.Data;

namespace ООО__Ароматный_мир_.MainPages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public static int UserId = 0;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Btn_Click_Enter(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = AppConnect.modelBd.User.FirstOrDefault(x => x.UserLogin == tbLogin.Text && x.UserPassword == tbPass.Password);
                if(userObj == null)
                {
                    MessageBox.Show("Ошибка, такого пользователя нет");
                }
                else
                {
                    switch (userObj.UserRole)
                    {
                        case 1:
                            MessageBox.Show("Добро пожаловать " + userObj.UserName);
                            AppFrame.frameMain.Navigate(new TablePages.ProductsPageAdmin());
                            UserId = userObj.UserID;
                            break;
                        case 2:
                            MessageBox.Show("Добро пожаловать " + userObj.UserName);
                            AppFrame.frameMain.Navigate(new TablePages.PageUser());
                            UserId = userObj.UserID;
                            break;
                        case 3:
                            MessageBox.Show("Добро пожаловать " + userObj.UserName);
                            AppFrame.frameMain.Navigate(new TablePages.PageUser());
                            UserId = userObj.UserID;
                            break;
                        default:
                            MessageBox.Show("Ошибка, такого пользователя нет");
                            break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка, такого пользователя нет");
            }
        }

        private void Btn_Click_EnterU(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new TablePages.ProductsPageAdmin());
        }
    }
}
