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
    /// Логика взаимодействия для AddCurator.xaml
    /// </summary>
    public partial class AddCurator : Window
    {
        CuratorWindow CW;
        bool Edit = false;
        Curator Curators;
        public AddCurator()
        {
            InitializeComponent();
        }
        public AddCurator(CuratorWindow window)
        {
            InitializeComponent();
            window = CW;
        }
        public AddCurator(CuratorWindow window, Curator curator)
        {
            InitializeComponent();
            Edit = true;
            Curators = curator;
            if (Edit == true)
            {
                TB_Title.Text = "Куратор редактирование";
                TB_FirstName.Text = Curators.FirstName;
                TB_LastName.Text = Curators.LastName;
                TB_MiddleName.Text = Curators.MiddleName;
                TB_Password.Text = Curators.Password;
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
                        foreach (var ID in DB.Curators.ToList()) { id = ID.CuratorID; }
                        DB.Curators.Add(new Curator()
                        {
                            CuratorID = id + 1,
                            FirstName = TB_FirstName.Text,
                            LastName = TB_LastName.Text,
                            MiddleName = TB_MiddleName.Text,
                            Password = TB_Password.Text
                        });
                        DB.SaveChanges();
                        CW.UpdateCurator();
                    }
                }
                else if (Edit == true)
                {
                    using (var DB = new StudentModel())
                    {
                        var item = DB.Curators.FirstOrDefault(i => i.CuratorID == Curators.CuratorID);
                        if (item != null)
                        {
                            item.FirstName = TB_FirstName.Text;
                            item.LastName = TB_LastName.Text;
                            item.MiddleName = TB_MiddleName.Text;
                            item.Password = TB_Password.Text;
                        }
                        DB.SaveChanges();
                        CW.UpdateCurator();
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
            CW.IsEnabled = true;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CW.IsEnabled = true;
        }
    }
}
