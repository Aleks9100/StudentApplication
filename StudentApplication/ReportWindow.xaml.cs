using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace StudentApplication
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new StudentModel())
            {
                string dir = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(@"Student.exe", "");
                string fileName = $@"{dir}\Otchet.xlsx";
                var workbook = new XLWorkbook(fileName);
                var worksheet = workbook.Worksheet(1);
                worksheet.Cell("C" + 4).Value = CB_DateBegin.SelectedValue;
                worksheet.Cell("E" + 4).Value = CB_DateEnd.SelectedValue;

                worksheet.Cell("E" + 27).Value = "Сумма стипендии группы: " + CB_Group.Text;
                worksheet.Cell("G" + 27).Value = DB.Students.Where(i => i.GroupID == Convert.ToInt32(CB_Group.SelectedValue)).Sum(s => s.Scholarship);

                int row = 8;
                foreach (var t in DB.Students
                    .Include(c => c.Group)
                    .Where(d => d.Year_of_admission >= Convert.ToInt32(CB_DateBegin.SelectedValue)
                    &&
                    d.Year_of_admission <= Convert.ToInt32(CB_DateEnd.SelectedValue))
                    .ToList())
                {
                    worksheet.Cell("B" + row).Value = t.LastName;
                    worksheet.Cell("C" + row).Value = t.FirstName;
                    worksheet.Cell("D" + row).Value = t.MiddleName;
                    worksheet.Cell("E" + row).Value = t.Scholarship;
                    worksheet.Cell("F" + row).Value = t.Group.Title;
                    worksheet.Cell("G" + row).Value = t.Group.Curator.LastName;
                }
                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(fileName);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            (new MenuWindow()).Show();
            this.Close();
        }
    }
}
