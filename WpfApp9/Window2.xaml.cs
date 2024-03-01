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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        Entities1 entities = new Entities1();
        public Window2()
        {
            InitializeComponent();
            foreach (var клиент in entities.Клиент)
                Listklient.Items.Add(клиент);
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var клиент = Listklient.SelectedItem as Клиент;

            if (TextBoxFamProekt.Text == "")
                MessageBox.Show("Заполните  поле!", "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Error);
            else
            {
                if (клиент == null)
                {
                    клиент = new Клиент();
                    entities.Клиент.Add(клиент);
                    Listklient.Items.Add(клиент);
                }
                клиент.FamiliaZakazchika = TextBoxFamProekt.Text;




                entities.SaveChanges();
                Listklient.Items.Refresh();
                MessageBox.Show("Запись сохранена", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ListProekt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_сотрудники = Listklient.SelectedItem as Клиент; // Предполагается, что вы используете класс Игроки для элементов ListBox

            if (selected_сотрудники != null)
            {





                TextBoxFamProekt.Text = selected_сотрудники.FamiliaZakazchika;



            }
            else
            {

                TextBoxFamProekt.Text = "";

            }
        }

        private void Save_Dalete_Click(object sender, RoutedEventArgs e)
        {
            var delete_клиент = Listklient.SelectedItem as Клиент;

            try
            {
                var exist_ = (from FamiliaZakazchika in entities.Проекты where FamiliaZakazchika.ZakazchikID == delete_клиент.ZakazchikID select FamiliaZakazchika).First();
                MessageBox.Show("Запись удалить нельзя!\nСуществуют проекты этих клиентов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {

                var rezult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezult == MessageBoxResult.No)
                    return;


                if (delete_клиент != null)
                {
                    entities.Клиент.Remove(delete_клиент);
                    entities.SaveChanges();
                    Listklient.Items.Remove(delete_клиент);
                    TextBoxFamProekt.Clear();

                    MessageBox.Show("Клиент успешно удален");
                }
                else
                {
                    MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Ohistka_Button_Click(object sender, RoutedEventArgs e)
        {

            TextBoxFamProekt.Text = "";

            Listklient.SelectedIndex = -1;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
