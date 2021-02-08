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
    /// Логика взаимодействия для GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        public GroupWindow()
        {
            InitializeComponent();
            UpdateGroup();
        }

        public void UpdateGroup()
        {
            using (var DB = new StudentModel())
            {
                DGR_Group.ItemsSource = null;
                DGR_Group.ItemsSource = DB.Groups.ToList();
            }
        }
        private void StudentWin_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menu = new MenuWindow();
            menu.Show();
            this.Close();

        }

        private void CuratorWin_Click(object sender, RoutedEventArgs e)
        {
            CuratorWindow curator = new CuratorWindow();
            curator.Show();
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
            AddGroup add = new AddGroup(this);
            add.Show();
            this.IsEnabled = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddGroup add = new AddGroup(this, (Group)DGR_Group.SelectedItem);
            add.Show();
            this.IsEnabled = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var Db = new StudentModel())
                {
                    var item = Db.Groups.SingleOrDefault(i => i.GroupID == ((Group)DGR_Group.SelectedItem).GroupID);
                    Db.Groups.Remove(item);
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
