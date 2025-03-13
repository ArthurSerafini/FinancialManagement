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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinancialManagment.Views
{
    /// <summary>
    /// Interação lógica para TablesViews.xam
    /// </summary>
    public partial class TablesViews : UserControl
    {
        public delegate void Delegate();
        public static event Delegate? CallView;
        public TablesViews()
        {
            InitializeComponent();

            paymentTable.Items.Add("Teste 1");
            paymentTable.Items.Add("Teste 2");
            paymentTable.Items.Add("Teste 3");
            paymentTable.Items.Add("Teste 4");

        }

        private void addTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            CallView?.Invoke();
        }

        public static void subscribeEvent(Delegate callTaskView)
        {
            CallView += callTaskView;
        }
    }
}
