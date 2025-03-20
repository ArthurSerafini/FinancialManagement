using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DatabaseConstructor;

namespace FinancialManagment.Views
{
    /// <summary>
    /// Interaction logic for InstallmentPaidPopUp.xaml
    /// </summary>
    public partial class InstallmentPaidPopUp : Window
    {
        public bool PopUpResponse = false;
        public InstallmentsTable installmentsData = new InstallmentsTable();
        private float paidValue;

        public InstallmentPaidPopUp()
        {
            InitializeComponent();
        }


        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            installmentsData.Date = DateBox.Text;
            installmentsData.PaidValue = paidValue;
            installmentsData.Paid = PaidCheck.IsChecked.Value;
            PopUpResponse = true;
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            PopUpResponse = false;
            Close();
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

        public void SetFields(InstallmentsTable data)
        {
            DateBox.Text = data.Date;
            PaidValueBox.Text = data.PaidValue.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            paidValue = data.PaidValue;
            PaidCheck.IsChecked = data.Paid;
        }
    }
}
