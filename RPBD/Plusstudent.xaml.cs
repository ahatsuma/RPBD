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

namespace RPBD
{
    /// <summary>
    /// Логика взаимодействия для Plusstudent.xaml
    /// </summary>
    public partial class Plusstudent : Window
    {
        private Database db = new();
        public Plusstudent()
        {
            InitializeComponent();
            LoadGroups();
        }

        
           
           
        private void LoadGroups()
        {
            var groups = db.GetGroups(); 
            groupCb.ItemsSource = groups;
            groupCb.DisplayMemberPath = "GroupName";
            groupCb.SelectedValuePath = "GroupId";
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string firstName = firstNameTb.Text;
            string lastName = lastNameTb.Text;
            int groupId = (int)groupCb.SelectedValue;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || groupId == 0)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;

            db.AddLearner(firstName, lastName, groupId);
            Close();
        }
    }
}
    
