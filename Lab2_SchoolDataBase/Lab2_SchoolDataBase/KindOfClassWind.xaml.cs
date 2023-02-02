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
    /// Логика взаимодействия для KindOfClassWind.xaml
    /// </summary>
    public partial class KindOfClassWind : Window
    {
        private KindsOfClass thisKindsOfClass = new KindsOfClass();
        public KindOfClassWind(KindsOfClass selectedKindsOfClass)
        {
            InitializeComponent();
            if(selectedKindsOfClass != null)
                    thisKindsOfClass = selectedKindsOfClass;
            DataContext = thisKindsOfClass;

        }

        private void saveChangesBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!MainWindow.db.KindsOfClasses.Any(k=>k.Id==thisKindsOfClass.Id))
                MainWindow.db.KindsOfClasses.Add(thisKindsOfClass);
            try
            {
                MainWindow.db.SaveChanges();
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
