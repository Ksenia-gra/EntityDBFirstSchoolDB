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

namespace Lab2_SchoolDataBase
{
    /// <summary>
    /// Логика взаимодействия для StudentsWind.xaml
    /// </summary>
    public partial class StudentsWind : Window
    {
        private Student thisStudent = new Student();
        public StudentsWind(Student selectedStudent)
        {
            InitializeComponent();
            //ClassCMB.ItemsSource = MainWindow.db.Classes.Local.ToBindingList();
            if (selectedStudent != null)
                thisStudent = selectedStudent;
            DataContext = thisStudent;
        }

        private void saveChangesBtn_Click(object sender, RoutedEventArgs e)
        {

           
           
            if (string.IsNullOrEmpty(dateBirthPic.Text) || string.IsNullOrWhiteSpace(dateBirthPic.Text))
            {
                MessageBox.Show("Введите год рождения");
                return;
            }

            if (!MainWindow.db.Students.Any(k => k.Id == thisStudent.Id))

            {

                if (MainWindow.db.Classes.Any(c => c.Id == thisStudent.ClassId))
                {

                    MainWindow.db.Students.Add(thisStudent);
                }

                else
                {
                    MessageBox.Show("Код класса не найден");
                    return;
                }
            }


            try
            {
               
                MainWindow.db.SaveChanges();
                MessageBox.Show("Данные сохранены");
         
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }
    }
}
