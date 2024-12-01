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
using Organization.Model;
using Organizer.UI.ViewModels;
using TaskStatus = Organization.Model.TaskStatus;
using Task = Organization.Model.Task;

namespace Organizer.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonCloseTask_Click(object sender, RoutedEventArgs e)
        {
            var task = (TaskViewModel)dataGridTasks.SelectedItem;
            task.Status = TaskStatus.Closed;
        }
    }
}