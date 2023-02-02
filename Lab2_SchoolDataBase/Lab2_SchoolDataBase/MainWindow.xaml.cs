using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2_SchoolDataBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SchoolDbContext db { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }
        public void Load()
        {

            db = new SchoolDbContext();
            db.Students.Load();
            db.Classes.Load();
            db.KindsOfClasses.Load();

            studGrid.ItemsSource = db.Students.Local.ToBindingList();
            classGrid.ItemsSource = db.Classes.Local.ToBindingList();
            classKindGrid.ItemsSource=db.KindsOfClasses.Local.ToBindingList();

            

        }

        private void addKindOfClass_Click(object sender, RoutedEventArgs e)
        {
            KindOfClassWind kindOfClassWind = new KindOfClassWind(null);
            kindOfClassWind.ShowDialog();
        }

        private void editBtnClassKind_Click(object sender, RoutedEventArgs e)
        {
            KindOfClassWind kindOfClassWind = new KindOfClassWind((sender as Button).DataContext as KindsOfClass);
            kindOfClassWind.ShowDialog();
        }

        private void classKindGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Classes")
                e.Cancel = true;
        }

        private void deleteKindOfClass_Click(object sender, RoutedEventArgs e)
        {
            List<KindsOfClass> kindsOfClassesToDelete = classKindGrid.SelectedItems.Cast<KindsOfClass>().ToList();
            if (MessageBox.Show($"Удалить элементы?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {


                try
                {
                    db.KindsOfClasses.RemoveRange(kindsOfClassesToDelete);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void classGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Students" || e.Column.Header.ToString()=="Kind")
                e.Cancel = true;
        }

        private void addClass_Click(object sender, RoutedEventArgs e)
        {
            ClassesWind classWind = new ClassesWind(null);
            classWind.ShowDialog();
        }

        private void editBtnClass_Click(object sender, RoutedEventArgs e)
        {
            ClassesWind classesWind = new ClassesWind((sender as Button).DataContext as Class);
            classesWind.ShowDialog();
        }

        private void deleteClass_Click(object sender, RoutedEventArgs e)
        {
            List<Class> classesToDelete = classGrid.SelectedItems.Cast<Class>().ToList();
            if (MessageBox.Show($"Удалить элементы?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) 
                == MessageBoxResult.Yes)
            {


                try
                {
                    db.Classes.RemoveRange(classesToDelete);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void addStudent_Click(object sender, RoutedEventArgs e)
        {
            

            StudentsWind studentWind = new StudentsWind(null);
            studentWind.ShowDialog();
        }

        private void editBtnStd_Click(object sender, RoutedEventArgs e)
        {
            StudentsWind studentWind = new StudentsWind((sender as Button).DataContext as Student);
            studentWind.ShowDialog();
        }

        private void deleteStudent_Click(object sender, RoutedEventArgs e)
        {
            List<Student> studentsToDelete = studGrid.SelectedItems.Cast<Student>().ToList();
            if (MessageBox.Show($"Удалить элементы?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            {


                try
                {
                    db.Students.RemoveRange(studentsToDelete);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void studGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Class" )
                e.Cancel = true;
        }

        private void selectBtn_Click(object sender, RoutedEventArgs e)
        {
    
            if(int.TryParse(selectTxb.Text,out _))
            {
             
                
                resultsGrid.ItemsSource = (from student in db.Students
                                           where student.DateOfBirth.Contains($"{selectTxb.Text}")
                                    
                                           select student).ToList();
            }
            else
            {
                MessageBox.Show("Неправильно введен год");
                selectTxb.Text = null;
            }
        }

        private void maxStudyYear_Click(object sender, RoutedEventArgs e)
        {
            Class classR= new Class();
            classR.StudyYear= db.Classes.Max(c=>c.StudyYear);
            resultsGrid.ItemsSource = (from classI in db.Classes
                                       where classI.StudyYear==classR.StudyYear
                                       select classI).ToList();
        }

        private void joinBtn_Click(object sender, RoutedEventArgs e)
        {
            var students = (from student in db.Students join c in db.Classes on student.ClassId equals c.Id 
                            select new {Fio=student.Fio,Class=c.ClassLetter });
            
           resultsGrid.ItemsSource = students.ToList();
        }
    }
}
