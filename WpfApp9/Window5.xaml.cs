using System;
using OfficeOpenXml;
using System.IO;
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
using Microsoft.Win32;



namespace WpfApp9
{
    /// <summary>
    /// Логика взаимодействия для Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        Entities1 entities = new Entities1();
        public Window5()
        {
            InitializeComponent();
            var query = from проекты in entities.Проекты
                        join сотрудники in entities.Сотрудники on проекты.SotrudnikId equals сотрудники.SorudnikId
                        join специализация in entities.Специализация on проекты.SpezializaziaID equals специализация.SpezializaziaID
                        join клиент in entities.Клиент on проекты.ZakazchikID equals клиент.ZakazchikID
                        select new
                        {
                            проекты.ProektID,
                            проекты.NazvanieProekta,
                            проекты.cena,
                            проекты.DateSroki,

                            сотрудники.Familia,
                            специализация.SpezializaziaSotrud,
                            клиент.FamiliaZakazchika
                        };

            // Проверяем, есть ли результаты запроса
            if (query.Count() == 0)
            {
                MessageBox.Show("Ошибка компиляции");
                return;
            }

            // Очищаем содержимое DataGrid
            dataGrid.Items.Clear();

            // Добавляем каждый элемент из результата запроса в DataGrid
            foreach (var item in query)
            {
                dataGrid.Items.Add(item);
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (saveFileDialog.ShowDialog() == true)
            {
                FileInfo file = new FileInfo(saveFileDialog.FileName);
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Data");

                    // Записываем заголовки столбцов
                    for (int i = 0; i < dataGrid.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = dataGrid.Columns[i].Header;
                    }

                    // Записываем данные из DataGrid в Excel
                    for (int i = 0; i < dataGrid.Items.Count; i++)
                    {
                        for (int j = 0; j < dataGrid.Columns.Count; j++)
                        {
                            var cellValue = ((TextBlock)dataGrid.Columns[j].GetCellContent(dataGrid.Items[i])).Text;
                            worksheet.Cells[i + 2, j + 1].Value = cellValue;
                        }
                    }

                    package.Save();
                }

                MessageBox.Show("Экспорт завершен!");
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
