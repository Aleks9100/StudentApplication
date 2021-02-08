using Microsoft.Win32;
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
    /// Логика взаимодействия для AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        MenuWindow Menu;
        bool Edit = false;
        Student Students;
        string FilePath;
        public AddStudent()
        {
            InitializeComponent();
        }
        public AddStudent(MenuWindow menu)
        {
            InitializeComponent();
            Menu = menu;
            AddCombo();
            for (int i = 2007; i < 2020; i++)
            {
                CB_Year_of_admission.Items.Add(i);
            }

        }
        public void AddCombo()
        {
            CB_Group.ItemsSource = null;
            using (var Db = new StudentModel())
            {
                CB_Group.ItemsSource = Db.Groups.ToList();
            }
        }
        public AddStudent(MenuWindow menu, Student student)
        {
            InitializeComponent();
            AddCombo();
            Menu = menu;
            Students = student;
            Edit = true;
            for (int i = 2007; i < 2020; i++)
            {
                CB_Year_of_admission.Items.Add(i);
            }

            if (Edit == true)
            {
                TB_Title.Text = "Студент редактирование";
                TB_FirstName.Text = Students.FirstName;
                TB_LastName.Text = Students.LastName;
                TB_MiddleName.Text = Students.MiddleName;
                CB_Year_of_admission.SelectedItem = Students.Year_of_admission - 2007;
                CB_Group.SelectedItem = Students.GroupID - 1;
                TB_Scholarship.Text = Students.Scholarship.ToString();
                BitmapImage bm = new BitmapImage();
                bm.BeginInit();
                bm.UriSource = new Uri(Students.Photo, UriKind.Relative);
                bm.CacheOption = BitmapCacheOption.OnLoad;
                bm.EndInit();
                I_Student.Source = bm;
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
                        foreach (var ID in DB.Students.ToList()) { id = ID.StudentID; }
                        DB.Students.Add(new Student()
                        {
                            FirstName = TB_FirstName.Text,
                            LastName = TB_LastName.Text,
                            MiddleName = TB_MiddleName.Text,
                            Scholarship = Convert.ToInt32(TB_Scholarship.Text),
                            GroupID = Convert.ToInt32(CB_Group.SelectedValue),
                            Year_of_admission = Convert.ToInt32(CB_Year_of_admission.SelectedItem),
                            Photo = FilePath
                        });
                        DB.SaveChanges();
                        Menu.UpdateStudent();
                    }
                }
                else if (Edit == true)
                {
                    using (var DB = new StudentModel())
                    {
                        var item = DB.Students.FirstOrDefault(i => i.StudentID == Students.StudentID);
                        if (item != null)
                        {
                            item.FirstName = TB_FirstName.Text;
                            item.LastName = TB_LastName.Text;
                            item.MiddleName = TB_MiddleName.Text;
                            item.Scholarship = Convert.ToInt32(TB_Scholarship.Text);
                            item.GroupID = Convert.ToInt32(CB_Group.SelectedValue);
                            item.Year_of_admission = Convert.ToInt32(CB_Year_of_admission.SelectedItem);
                            item.Photo = FilePath;
                        }
                        DB.SaveChanges();
                        Menu.UpdateStudent();
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
            Menu.IsEnabled = true;
            this.Close();
        }

        private void I_Student_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.png, *.jpg, *.jpeg, *.bmp, *.gif)|*.png, *.jpg, *.jpeg, *.bmp|All files (*.*)|*.*";
            FilePath = openFileDialog.FileName;
            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.UriSource = new Uri(FilePath, UriKind.Relative);
            bm.CacheOption = BitmapCacheOption.OnLoad;
            bm.EndInit();
            I_Student.Source = bm;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Menu.IsEnabled = true;
        }
    }
}
