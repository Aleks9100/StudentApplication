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

namespace StudentApplication.AddEditWindow
{
    /// <summary>
    /// Логика взаимодействия для AddGroup.xaml
    /// </summary>
    public partial class AddGroup : Window
    {
        GroupWindow GW;
        bool Edit = false;
        Group Groups;
        public AddGroup()
        {
            InitializeComponent();
        }
        public AddGroup(GroupWindow Window)
        {
            InitializeComponent();
            AddCombo();
            GW = Window;
        }

        public void AddCombo()
        {
            CB_Curator.ItemsSource = null;
            using (var Db = new StudentModel())
            {
                CB_Curator.ItemsSource = Db.Curators.ToList();
            }
        }
        public AddGroup(GroupWindow Window, Group group)
        {
            InitializeComponent();
            GW = Window;
            Groups = group;
            Edit = true;
            AddCombo();
            if (Edit == true)
            {
                TB_Title.Text = "Группа редактирование";
                TB_TitleG.Text = Groups.Title;
                CB_Curator.SelectedItem = Groups.CuratorID - 1;
            }
        }
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Edit == false)
                {
                    int id = 0;
                    using (var DB = new StudentModel())
                    {
                        foreach (var ID in DB.Groups.ToList()) { id = ID.GroupID; }
                        DB.Groups.Add(new Group()
                        {
                            GroupID = id + 1,
                            Title = TB_TitleG.Text,
                            CuratorID = Convert.ToInt32(CB_Curator.SelectedValue)
                        });
                        DB.SaveChanges();
                        GW.UpdateGroup();
                    }
                }
                else if (Edit == true)
                {
                    using (var DB = new StudentModel())
                    {
                        var item = DB.Groups.FirstOrDefault(i => i.GroupID == Groups.GroupID);
                        if (item != null)
                        {
                            item.Title = TB_TitleG.Text;
                            item.CuratorID = Convert.ToInt32(CB_Curator.SelectedValue);
                        }
                        DB.SaveChanges();
                        GW.UpdateGroup();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            GW.IsEnabled = true;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GW.IsEnabled = true;
        }
    }
}
