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
using FinancialManagment.Enums;
using FinancialManagment.Views;

namespace FinancialManagment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public delegate void Delegate(ViewsNamesEnum viewToOpen, int id);
        

        public MainWindow()
        {
            InitializeComponent();
            openView(ViewsNamesEnum.TablesViews);
        }

        private void MenuTableBtn_Click(object sender, RoutedEventArgs e)
        {
            openView(ViewsNamesEnum.TablesViews);
        }

        private void MenuPage2Btn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Page2();
        }

        private void openView(ViewsNamesEnum viewToOpen, int id = -1)
        {
            switch(viewToOpen)
            {
                case ViewsNamesEnum.TablesViews:
                    DataContext = new TablesViews();
                    TablesViews.subscribeEvent(openView);
                    break;

                case ViewsNamesEnum.TasksViews:
                    DataContext = new TasksViews();
                    TasksViews.subscribeEvent(openView);
                    break;

                case ViewsNamesEnum.TasksViewsEditMode:
                    DataContext = new TasksViews(id);
                    TasksViews.subscribeEvent(openView);
                    break;

                default:
                    break;
            }
        }
    }
}