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
using FinancialManagment.Enums;

namespace FinancialManagment.Views
{
    /// <summary>
    /// Interação lógica para TasksViews.xam
    /// </summary>
    public partial class TasksViews : UserControl
    {
        private static bool editMode;
        private static int selectedId;
        private float paidValue = 0;
        private float totalValue = 0;
        private List<InstallmentsTable> installmentTableList = new();
        public delegate void Delegate(ViewsNamesEnum viewToOpen, int id=-1);
        public static event Delegate? CallView;


        //DbAcess dataBase = new DbAcess();
        MyDbContext dataBase = new MyDbContext();
        
        public TasksViews()
        {
            InitializeComponent();
            PaidValueBox.Text = "R$ 0,00";
            TotalValueBox.Text = "R$ 0,00";
            InstallmentsPaidBox.Text = "0";
            InstallmentsTotalBox.Text = "0";
            editMode = false;
        }

        public TasksViews(int id)
        {
            InitializeComponent();
            editMode = true;
            selectedId = id;

            PaymentInfo selectedTask = dataBase.PaymentInfos.Find(selectedId);
            
            TitleBox.Text = selectedTask?.Title;
            DescriptionBox.Text = selectedTask?.Description;
            TotalValueBox.Text = selectedTask?.TotalPrice.ToString();
            paidValue = Convert.ToSingle(selectedTask.PricePaid);
            totalValue = Convert.ToSingle(selectedTask.TotalPrice);

            var resultado = dataBase.InstallmentsTable.Where(x => x.paymentInfo.PaymentId.ToString() == selectedId.ToString()).ToList();

            installmentTableList = resultado;
            UpdatePaidTableList();
            UpdatePaidValue();
            UpdateInstallmentsBoxes();
        }

        public static void subscribeEvent(Delegate callView)
        {
            CallView += callView;
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            PaymentInfo paymentTask;

            if (editMode)
            {
                paymentTask = dataBase.PaymentInfos.Find(selectedId);
                paymentTask.Title = TitleBox.Text;
                paymentTask.Description = DescriptionBox.Text;
                paymentTask.PricePaid = paidValue;
                paymentTask.TotalPrice = totalValue;
                paymentTask.InstallmentsPaid = Convert.ToInt32(InstallmentsPaidBox.Text);
                paymentTask.TotalInstallments = Convert.ToInt32(InstallmentsTotalBox.Text);

                dataBase.SaveChanges();
            }

            else
            {
                paymentTask = new PaymentInfo();
                paymentTask.Title = TitleBox.Text;
                paymentTask.Description = DescriptionBox.Text;
                paymentTask.PricePaid = paidValue;
                paymentTask.TotalPrice = totalValue;
                paymentTask.InstallmentsPaid = Convert.ToInt32(InstallmentsPaidBox.Text);
                paymentTask.TotalInstallments = Convert.ToInt32(InstallmentsTotalBox.Text);

                dataBase.PaymentInfos.Add(paymentTask);

                dataBase.SaveChanges();
            }

            foreach (InstallmentsTable installmentData in installmentTableList)
            {
                paymentTask.installmentsTable.Add(installmentData);
            }
            dataBase.SaveChanges();
            
            MessageBox.Show("Tarefa salva!");
            CallView?.Invoke(ViewsNamesEnum.TablesViews);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            CallView?.Invoke(ViewsNamesEnum.TablesViews);
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
            popUp.SetFields(installmentTableList[selectedItem]);
            popUp.ShowDialog();

            if (popUp.PopUpResponse)
            {
                installmentTableList[selectedItem] = popUp.installmentsData;
            }
            UpdatePaidTableList();
            UpdatePaidValue();
            UpdateInstallmentsBoxes();
        }

        private void addPaymentItemBtn_Click(object sender, RoutedEventArgs e)
        {
            InstallmentPaidPopUp popUp = new InstallmentPaidPopUp();
            popUp.ShowDialog();
            
            if (popUp.PopUpResponse)
            {
                installmentTableList.Add(popUp.installmentsData);
                UpdatePaidTableList();
                UpdatePaidValue();
                UpdateInstallmentsBoxes();
            }
        }

        private void removePaymentItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedItem = paymentTable.SelectedIndex;
                installmentTableList.RemoveAt(selectedItem);

                MessageBoxResult result = MessageBox.Show($"Tem certeza que deseja remover o item selecionado?", "Atenção",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.Yes)
                {
                    UpdatePaidTableList();
                    UpdatePaidValue();
                    UpdateInstallmentsBoxes();
                }
            }
            catch
            {
                MessageBox.Show("Remoção inválida");
            }
        }

        private void UpdatePaidTableList()
        {
            paymentTable.Items.Clear();
            
            foreach (InstallmentsTable installmentData in installmentTableList)
            {
                paymentTable.Items.Add(installmentData.Date);
            }
        }

        private void UpdatePaidValue()
        {
            float sum = 0;
            foreach (InstallmentsTable installmentData in installmentTableList)
            {
                if (installmentData.Paid)
                {
                    sum += installmentData.PaidValue;
                }
            }

            PaidValueBox.Text = sum.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
        }

        private void UpdateInstallmentsBoxes()
        {
            int qntInstallmentPaid = 0;
            foreach (InstallmentsTable installmentData in installmentTableList)
            {
                if (installmentData.Paid)
                {
                    qntInstallmentPaid += 1;
                }
            }

            InstallmentsPaidBox.Text = qntInstallmentPaid.ToString();
            InstallmentsTotalBox.Text = installmentTableList.Count().ToString();
        }

        
    }
}
