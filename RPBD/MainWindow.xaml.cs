using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RPBD;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Database db;

    public MainWindow()
    {
        InitializeComponent(); 
        db = new Database();   
        LoadLearners();
    }

    private void LoadLearners()
    {
        var learners = db.GetLearners();
        learnersGrid.ItemsSource = learners;
    }

    private void AddLearnerBtn_Click(object sender, RoutedEventArgs e)
    {
        Plusstudent plusstudent = new Plusstudent();
        plusstudent.ShowDialog();
        LoadLearners();
    }

    private void DeleteLearnerBtn_Click(object sender, RoutedEventArgs e)
    {
        var selectedLearner = learnersGrid.SelectedItem as Learner;
        if (selectedLearner == null)
        {
            MessageBox.Show("Пожалуйста, выберите студента для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (MessageBox.Show($"Вы действительно хотите удалить студента {selectedLearner.FirstName} {selectedLearner.LastName}?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            db.DeleteLearner(selectedLearner.LearnerId);
            LoadLearners();
        }
    }
}