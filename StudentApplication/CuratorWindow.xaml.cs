using StudentApplication.AddEditWindow;
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

namespace StudentApplication
{
    /// <summary>
    /// Логика взаимодействия для CuratorWindow.xaml
    /// </summary>
    public partial class CuratorWindow : Window
    {
        public CuratorWindow()
        {
            InitializeComponent();
            UpdateCurator();
        }

        public void UpdateCurator()
        {
            DGR_Curator.ItemsSource = null;
            using (var Db = new StudentModel())
            {
                DGR_Curator.ItemsSource = Db.Curators.ToList();
            }
        }

        private void StudentWin_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menu = new MenuWindow();
            menu.Show();
            this.Close();
        }
        private void GroupWin_Click(object sender, RoutedEventArgs e)
        {
            GroupWindow group = new GroupWindow();
            group.Show();
            this.Close();
        }
        private void ReportWin_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow report = new ReportWindow();
            report.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddCurator add = new AddCurator(this);
            add.Show();
            this.IsEnabled = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddCurator add = new AddCurator(this, (Curator)DGR_Curator.SelectedItem);
            add.Show();
            this.IsEnabled = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            try
            {
                using (var Db = new StudentModel())
                {
                    var item = Db.Curators.SingleOrDefault(i => i.CuratorID == ((Curator)DGR_Curator.SelectedItem).CuratorID);
                    Db.Curators.Remove(item);
                    Db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
