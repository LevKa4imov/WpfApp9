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

namespace WpfApp9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities entities = new Entities();
        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string m_reg = "Регистрация";
            string m_error = "Ошибка! Пользователь с таким логином уже существует!";
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Password))
            {
                string login = txtUsername.Text;
                string password = txtPassword.Password;
                using (var entities = new Entities())
                {
                    bool userExists = entities.Роли.Any(u => u.username == login);
                    if (!userExists)
                    {
                        var newUser = new Роли
                        {
                            // Установка свойств нового пользователя в соответствии с логикой вашего приложения
                            // Например:
                            username = login,
                            password = password
                        };

                        entities.Роли.Add(newUser);
                        entities.SaveChanges();
                        MessageBox.Show("Регистрация прошла успешно!", m_reg, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show(m_error, m_reg, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", m_reg, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string m_aut = "Аутентификация";
            string m_errorincor = "Ошибка! Проверьте правильность данных!";
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Password))
            {
                string Login = txtUsername.Text;
                string Password = txtPassword.Password;
                var user = entities.Роли.FirstOrDefault(u => u.username == Login && u.password == Password);
                if (user != null)
                {
                    if (user.roli == "1")
                    {
                        var window1 = new Window1();
                        window1.ShowDialog();
                    }
                    else
                    {
                        var window4 = new Window2();
                        window4.ShowDialog();
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show(m_errorincor, m_aut, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(m_errorincor, m_aut, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}