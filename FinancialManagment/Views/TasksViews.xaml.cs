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
using DatabaseConstructor;
using DatabaseAcess;

namespace FinancialManagment.Views
{
    /// <summary>
    /// Interação lógica para TasksViews.xam
    /// </summary>
    public partial class TasksViews : UserControl
    {
        private float paidValue = 0;
        private float totalValue = 0;
        
        //DbAcess dataBase = new DbAcess();
        MyDbContext dataBase = new MyDbContext();
        
        public TasksViews()
        {
            InitializeComponent();
            PaidValueBox.Text = "R$ 0,00";
            TotalValueBox.Text = "R$ 0,00";
        }

        public void NewTask() 
        {
            
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            PaymentInfo paymentTask = new PaymentInfo();

            paymentTask.Title = TitleBox.Text;
            paymentTask.Description = DescriptionBox.Text;
            paymentTask.PricePaid = paidValue;
            paymentTask.TotalPrice = totalValue;
            paymentTask.InstallmentsPaid = Convert.ToInt32(InstallmentsPaidBox.Text);
            paymentTask.TotalInstallments = Convert.ToInt32(InstallmentsTotalBox.Text);

            dataBase.PaymentInfos.Add(paymentTask);
            dataBase.SaveChanges();

            MessageBox.Show("Tarefa salva!");
        }

        private void PaidValueBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string text = PaidValueBox.Text;

            try
            {
                if (text.Contains("R$"))
                {
                    string withoutRS = text.Substring(text.IndexOf("R$") + 2);
                    paidValue = Convert.ToSingle(withoutRS);
                }
                else
                {
                    paidValue = Convert.ToSingle(text);
                    PaidValueBox.Text = paidValue.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
                }

                if (paidValue < 0)
                {
                    throw new Exception("invalid_input"); 
                }
            }

            catch
            {
                MessageBox.Show("Digite um valor válido!");
                PaidValueBox.Text = "";
            }

        }

        private void TotalValueBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string text = TotalValueBox.Text;

            try
            {
                if (text.Contains("R$"))
                {
                    string withoutRS = text.Substring(text.IndexOf("R$") + 2);
                    totalValue = Convert.ToSingle(withoutRS);
                }
                else
                {
                    totalValue = Convert.ToSingle(text);
                    TotalValueBox.Text = totalValue.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
                }

                if (paidValue < 0)
                {
                    throw new Exception("invalid_input");
                }
            }

            catch
            {
                MessageBox.Show("Digite um valor válido!");
                TotalValueBox.Text = "";
            }
        }

        private void paymentTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int selectedItem = paymentTable.SelectedIndex;
            InstallmentPaidPopUp popUp = new InstallmentPaidPopUp();
            popUp.ShowDialog();
        }
        private void addPaymentItemBtn_Click(object sender, RoutedEventArgs e)
        {
            InstallmentPaidPopUp popUp = new InstallmentPaidPopUp();
            popUp.ShowDialog();
        }
    }
}
