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
using System.Windows.Shapes;

namespace WpfApp9
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        Entities1 entities = new Entities1();
        public Window3()
        {
            InitializeComponent();
            foreach (var специализация in entities.Специализация)
                ListSpezializ.Items.Add(специализация);
        }

        private void ListProekt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_должность = ListSpezializ.SelectedItem as Специализация;
            if (selected_должность != null)
            {
                TextBoxFamSpezializ.Text = selected_должность.SpezializaziaSotrud;



            }
            else
            {

                TextBoxFamSpezializ.Text = "";

            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var должность = ListSpezializ.SelectedItem as Специализация;

            if (TextBoxFamSpezializ.Text == "")
                MessageBox.Show("Заполните  поле!", "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Error);
            else
            {
                if (должность == null)
                {
                    должность = new Специализация();
                    entities.Специализация.Add(должность);
                    ListSpezializ.Items.Add(должность);
                }
                должность.SpezializaziaSotrud = TextBoxFamSpezializ.Text;




                entities.SaveChanges();
                ListSpezializ.Items.Refresh();
                MessageBox.Show("Запись сохранена", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Save_Dalete_Click(object sender, RoutedEventArgs e)
        {
            var delete_должность = ListSpezializ.SelectedItem as Специализация;

            try
            {
                var exist_ = (from SpezializaziaSotrud in entities.Проекты where SpezializaziaSotrud.SpezializaziaID == delete_должность.SpezializaziaID select SpezializaziaSotrud).First();
                MessageBox.Show("Запись удалить нельзя!\nСуществуют проекты этих разработчиков!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {

                var rezult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezult == MessageBoxResult.No)
                    return;


                if (delete_должность != null)
                {
                    entities.Специализация.Remove(delete_должность);
                    entities.SaveChanges();
                    ListSpezializ.Items.Remove(delete_должность);
                    TextBoxFamSpezializ.Clear();

                    MessageBox.Show("Разработчик успешно удален");
                }
                else
                {
                    MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Ohistka_Button_Click(object sender, RoutedEventArgs e)
        {
            TextBoxFamSpezializ.Text = "";

            ListSpezializ.SelectedIndex = -1;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}

