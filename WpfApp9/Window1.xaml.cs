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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        Entities1 entities = new Entities1();
        public Window1()
        {
            InitializeComponent();
            foreach (var проекты in entities.Проекты)
                SOTRUDNIKIList.Items.Add(проекты);
            foreach (var Специализация in entities.Специализация)
                ComboBox_Doljnost.Items.Add(Специализация);
            foreach (var сотрудники in entities.Сотрудники)
                ComboBox_Sotrudnik.Items.Add(сотрудники);
            foreach (var клиент in entities.Клиент)
                ComboBox_rabotaklient.Items.Add(клиент);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_Проекты = SOTRUDNIKIList.SelectedItem as Проекты; // Предполагается, что вы используете класс Игроки для элементов ListBox

            if (selected_Проекты != null)
            {





                TextBoxProekt.Text = selected_Проекты.NazvanieProekta;
         
                TextBoxCena.Text = selected_Проекты.cena.ToString();
                TextBox6.Text = selected_Проекты.DateSroki.ToString();

                ComboBox_Doljnost.SelectedItem =
                    (from DoljnostSotrudnika in entities.Специализация
                     where DoljnostSotrudnika.SpezializaziaID == selected_Проекты.SpezializaziaID
                     select DoljnostSotrudnika).Single<Специализация>();

                ComboBox_Sotrudnik.SelectedItem =
                   (from Sotrudnik in entities.Сотрудники
                    where Sotrudnik.SorudnikId == selected_Проекты.SotrudnikId
                    select Sotrudnik).Single<Сотрудники>();

                ComboBox_rabotaklient.SelectedItem =
                (from rabotaklient in entities.Клиент
                 where rabotaklient.ZakazchikID == selected_Проекты.ZakazchikID
                 select rabotaklient).Single<Клиент>();

                if (!string.IsNullOrEmpty(selected_Проекты.Photo))
                {
                    cloth_image.Source = new BitmapImage(new Uri(selected_Проекты.Photo));
                }
                else
                {
                    cloth_image.Source = null;
                }
            }
            else
            {
                TextBoxProekt.Text = "";
                TextBoxCena.Text = "";
              
                ComboBox_Doljnost.SelectedIndex = -1;
                ComboBox_Sotrudnik.SelectedIndex = -1;
                ComboBox_rabotaklient.SelectedIndex = -1;
                cloth_image.ClearValue(Image.SourceProperty);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var проекты = SOTRUDNIKIList.SelectedItem as Проекты;

            if (TextBoxProekt.Text == "" || TextBoxCena.Text == "" || TextBox6.Text == ""  || ComboBox_Doljnost.SelectedIndex == -1 || ComboBox_Sotrudnik.SelectedIndex == -1 || ComboBox_rabotaklient.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    if (проекты == null)
                    {
                        проекты = new Проекты();
                        entities.Проекты.Add(проекты);
                        SOTRUDNIKIList.Items.Add(проекты);
                    }
                    проекты.NazvanieProekta = TextBoxProekt.Text;

                    проекты.DateSroki = TextBox6.SelectedDate.Value;
                    проекты.SpezializaziaID = (ComboBox_Doljnost.SelectedItem as Специализация).SpezializaziaID;
                    проекты.ZakazchikID = (ComboBox_rabotaklient.SelectedItem as Клиент).ZakazchikID;
                    проекты.SotrudnikId = (ComboBox_Sotrudnik.SelectedItem as Сотрудники).SorudnikId;
                    int vozrast;
                    if (int.TryParse(TextBoxCena.Text, out vozrast))
                    {
                        проекты.cena = vozrast;
                    }
                    else
                    {
                        MessageBox.Show("В поле стоимость доступен только числовой ввод данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return; // Exit the method
                    }

                    entities.SaveChanges();
                    SOTRUDNIKIList.Items.Refresh();
                    MessageBox.Show("Записи сохранены", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Handle the exception as per your requirement
                }
            }
        }

     





        private void textboxpoisk_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Dob_Foto_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;

                string rootDirectory = @"C:\Image\";

                string relativePath = imagePath.Replace(rootDirectory, string.Empty);
                var selected_stanc = SOTRUDNIKIList.SelectedItem as Проекты;

                if (selected_stanc != null)
                {
                    selected_stanc.Photo = relativePath;



                    SOTRUDNIKIList.Items.Refresh();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var rezult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rezult == MessageBoxResult.No)
                return;

            var delete_Проекты = SOTRUDNIKIList.SelectedItem as Проекты;
            if (delete_Проекты != null)
            {
                entities.Проекты.Remove(delete_Проекты);
                entities.SaveChanges();
                SOTRUDNIKIList.Items.Remove(delete_Проекты);
                TextBoxProekt.Clear();
                TextBoxCena.Clear();
                TextBox6.SelectedDate = null;
               
                ComboBox_Doljnost.SelectedIndex = -1;
                ComboBox_Sotrudnik.SelectedIndex = -1;
                ComboBox_rabotaklient.SelectedIndex = -1;

            }
            else
            {
                MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ___Button_Delete_ochistka_2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Ohistka_button_Click(object sender, RoutedEventArgs e)
        {
            TextBoxProekt.Text = "";
            TextBoxCena.Text = "";
            TextBox6.Text = "";
            SOTRUDNIKIList.SelectedIndex = -1;
            TextBoxProekt.Focus();
            textboxpoisk.Text = "";

            UpdateListBox(); // вызываем метод обновления списка при нажатии кнопки
        }

        private void UpdateListBox()
        {
            SOTRUDNIKIList.Items.Clear(); // очищаем список перед обновлением

            // Здесь добавьте вашу логику для обновления списка сотрудников
            var rez = entities.Проекты.ToList();
            foreach (var item in rez)
            {
                SOTRUDNIKIList.Items.Add(item);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textboxpoisk.Text))
            {
                var rez = entities.Проекты.ToList();
                var filteredRez = rez.Where(p => p.NazvanieProekta.ToLower().Contains(textboxpoisk.Text.ToLower())).ToList();
                SOTRUDNIKIList.Items.Clear();
                int totalRecords = rez.Count;
                int searchRecords = filteredRez.Count;
                if (searchRecords > 0)
                {
                    foreach (var item in filteredRez)
                    {
                        SOTRUDNIKIList.Items.Add(item);
                    }
                    // Отобразить общее количество записей и количество записей в режиме поиска 
                    MessageBox.Show($"Общее количество записей: {totalRecords}, Количество записей в режиме поиска: {searchRecords}");
                }
                else
                {
                    MessageBox.Show("Нет результатов");
                }
            }
            else
            {
                MessageBox.Show("Поле поиска пустое. Введите текст для выполнения поиска.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Window4 Window4 = new Window4();
            Window4.ShowDialog();
            ComboBox_Sotrudnik.Items.Clear();
            foreach (var сотрудники in entities.Сотрудники)
                ComboBox_Sotrudnik.Items.Add(сотрудники);
          
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Window2 Window2 = new Window2();
            Window2.ShowDialog();
            ComboBox_rabotaklient.Items.Clear();
            foreach (var клиент in entities.Клиент)
                ComboBox_rabotaklient.Items.Add(клиент);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Window3 Window3 = new Window3();
            Window3.ShowDialog();
            ComboBox_Doljnost.Items.Clear();
            foreach (var разработчик in entities.Специализация)
                ComboBox_Doljnost.Items.Add(разработчик);
        }
        private void Button_Click_Poisk(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textboxpoisk.Text))
            {
                var rez = entities.Сотрудники.ToList();
                var filteredRez = rez.Where(p => p.Familia.ToLower().Contains(textboxpoisk.Text.ToLower())).ToList();
                SOTRUDNIKIList.Items.Clear();
                int totalRecords = rez.Count;
                int searchRecords = filteredRez.Count;
                if (searchRecords > 0)
                {
                    foreach (var item in filteredRez)
                    {
                        SOTRUDNIKIList.Items.Add(item);
                    }
                    // Отобразить общее количество записей и количество записей в режиме поиска 
                    MessageBox.Show($"Общее количество записей: {totalRecords}, Количество записей в режиме поиска: {searchRecords}");
                }
                else
                {
                    MessageBox.Show("Нет результатов");
                }
            }
            else
            {
                MessageBox.Show("Поле поиска пустое. Введите текст для выполнения поиска.");
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            var Window5 = new Window5();
            Window5.ShowDialog();
            {
            }
        }

        private void Button_Click_Poisk2_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textboxpoisk.Text))
            {
                var rez = entities.Проекты.ToList();
                var filteredRez = rez.Where(p => p.NazvanieProekta.ToLower().Contains(textboxpoisk.Text.ToLower())).ToList();
                SOTRUDNIKIList.Items.Clear();
                int totalRecords = rez.Count;
                int searchRecords = filteredRez.Count;
                if (searchRecords > 0)
                {
                    foreach (var item in filteredRez)
                    {
                        SOTRUDNIKIList.Items.Add(item);
                    }
                    // Отобразить общее количество записей и количество записей в режиме поиска 
                    MessageBox.Show($"Общее количество записей: {totalRecords}, Количество записей в режиме поиска: {searchRecords}");
                }
                else
                {
                    MessageBox.Show("Нет результатов");
                }
            }
            else
            {
                MessageBox.Show("Поле поиска пустое. Введите текст для выполнения поиска.");
            }
        }

        private void ComboBox_Nationality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_proekt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_rabotaklient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBoxVozrast_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBoxFamilia_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}



