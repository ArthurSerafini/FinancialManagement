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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DatabaseConstructor;
using FinancialManagment.Enums;

namespace FinancialManagment.Views
{
    /// <summary>
    /// Interação lógica para TablesViews.xam
    /// </summary>
    public partial class TablesViews : UserControl
    {
        public delegate void Delegate(ViewsNamesEnum viewToOpen, int id = -1);
        public static event Delegate? CallView;

        MyDbContext dataBase = new MyDbContext();


        public TablesViews()
        {
            InitializeComponent();
            UpdateTablesView();
        }

        private void addTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            CallView?.Invoke(ViewsNamesEnum.TasksViews);
        }

        public static void subscribeEvent(Delegate callTaskView)
        {
            CallView += callTaskView;
        }

        private void UpdateTablesView()
        {
            paymentTable.Items.Clear();

            foreach (PaymentInfo paymentInfo in dataBase.PaymentInfos)
            {
                paymentTable.Items.Add(paymentInfo.PaymentId + "-" + paymentInfo.Title);
            }
        }

        private void paymentTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string[] taskName = paymentTable.SelectedValue.ToString().Split("-");
            int id = Convert.ToInt32(taskName[0]);
            PaymentInfo selectedTask = dataBase.PaymentInfos.Find(id);
            CallView?.Invoke(ViewsNamesEnum.TasksViewsEditMode, id);
        }

        private void removeTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] taskName = paymentTable.SelectedValue.ToString().Split("-");
                int id = Convert.ToInt32(taskName[0]);
                PaymentInfo selectedTask = dataBase.PaymentInfos.Find(id);


                MessageBoxResult result = MessageBox.Show($"Tem certeza que deseja remover o item {selectedTask?.Title} ?", "Atenção",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.Yes)
                {

                    dataBase.Remove(selectedTask);

                    dataBase.SaveChanges();
                    UpdateTablesView();
                }
            }
            catch
            {
                MessageBox.Show("Remoção inválida!");
            }
        }
    }
}
