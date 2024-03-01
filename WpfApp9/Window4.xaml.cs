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
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        Entities1 entities = new Entities1();
        public Window4()
        {
            InitializeComponent();
            foreach (var сотрудники in entities.Сотрудники)
                ListSotrudnik1.Items.Add(сотрудники);
        }

        private void ListProekt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_сотрудники= ListSotrudnik1.SelectedItem as Сотрудники;
            if (selected_сотрудники != null)
            {
                TextBoxSotrudnik.Text = selected_сотрудники.Familia;



            }
            else
            {

                TextBoxSotrudnik.Text = "";

            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var сотрудники = ListSotrudnik1.SelectedItem as Сотрудники;

            if (TextBoxSotrudnik.Text == "")
                MessageBox.Show("Заполните  поле!", "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Error);
            else
            {
                if (сотрудники == null)
                {
                    сотрудники = new Сотрудники();
                    entities.Сотрудники.Add(сотрудники);
                    ListSotrudnik1.Items.Add(сотрудники);
                }
                сотрудники.Familia = TextBoxSotrudnik.Text;




                entities.SaveChanges();
                ListSotrudnik1.Items.Refresh();
                MessageBox.Show("Запись сохранена", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Save_Dalete_Click(object sender, RoutedEventArgs e)
        {
            
        var delete_сотрудники = ListSotrudnik1.SelectedItem as Сотрудники;

            try
            {
                var exist_ = (from Familia in entities.Проекты where Familia.SotrudnikId == delete_сотрудники.SorudnikId select Familia).First();
        MessageBox.Show("Запись удалить нельзя!\nСуществуют проекты этих разработчиков!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {

                var rezult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezult == MessageBoxResult.No)
                    return;


                if (delete_сотрудники != null)
                {
                    entities.Сотрудники.Remove(delete_сотрудники);
                    entities.SaveChanges();
                    ListSotrudnik1.Items.Remove(delete_сотрудники);
                    TextBoxSotrudnik.Clear();

                    MessageBox.Show("Сотрудник успешно удален");
                }
                else
{
    MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
}
            }
        }
        private void Ohistka_Button_Click(object sender, RoutedEventArgs e)
        {
            TextBoxSotrudnik.Text = "";

            ListSotrudnik1.SelectedIndex = -1;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
