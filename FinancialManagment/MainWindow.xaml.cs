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
using FinancialManagment.Views;

namespace FinancialManagment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public delegate void Delegate();
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuTableBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext =  new TablesViews();
            TablesViews.subscribeEvent(openTasksView);
        }

        private void MenuPage2Btn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Page2();
        }

        private void openTasksView()
        {
            DataContext = new TasksViews();
        }
    }
}