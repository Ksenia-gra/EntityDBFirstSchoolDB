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
    /// Логика взаимодействия для ClassesWind.xaml
    /// </summary>
    public partial class ClassesWind : Window
    {
        private Class thisClass = new Class();
        public ClassesWind(Class selectedClass)
        {
            InitializeComponent();
            KindCMB.ItemsSource=MainWindow.db.KindsOfClasses.Local.ToBindingList();
            if (selectedClass != null)
                thisClass = selectedClass;
            DataContext = thisClass;
        }

        private void saveChangesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (thisClass.StudentsCount <= 0)
            {

                MessageBox.Show("Неверное количество студентов");
                return;
            }

            if (thisClass.StudyYear <= 0)
            {
                MessageBox.Show("Неверный год обучения");
                return;
            }
            DateTime dateTime= DateTime.Now;
            if (thisClass.CreateYear <= 0 || dateTime.Year-thisClass.StudyYear!=thisClass.CreateYear)
            {
                MessageBox.Show("Неверный год создания");
                return;
            }



            if (!MainWindow.db.Classes.Any(k => k.Id == thisClass.Id))
                MainWindow.db.Classes.Add(thisClass);
            MainWindow.db.SaveChanges();
            try
            {
               
                MessageBox.Show("Данные сохранены");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
