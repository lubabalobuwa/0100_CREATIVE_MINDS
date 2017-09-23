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
using System.IO;

namespace Bank_slmc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class WithDrawal : Deposit
    {public WithDrawal( )
        {

        }
     public   void reduce(double withdrawm)
        {
            balance -= withdrawm;
           
        }

    }
    public class Deposit
    {
        public double balance { get; set; }
       
        public double Deposited(double amount)
        {
            balance += amount;
           
            return balance;
        }
      
    }
    public class left : WithDrawal
    {public left( ) 
        {

        }
        
       
    }
    public partial class MainWindow : Window
    {
      
        public MainWindow()
        {
            InitializeComponent();
          
        
            withdrawal_Deposit.Visibility = Visibility.Hidden;
            BalanceCanvas.Visibility = Visibility.Hidden;
            

        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            
            transactions.Visibility = Visibility.Visible;
        }

        private void WithDraw__Click(object sender, RoutedEventArgs e)
        {
            transactions.Visibility = Visibility.Hidden; withdrawal_Deposit.Visibility = Visibility.Visible;
            WithDrawal withdrawing = new WithDrawal();
            int withdrw =Convert.ToInt32( textBox4.Text);
          
            withdrawing.reduce(withdrw);
      
        }

        private void Deposit__Click(object sender, RoutedEventArgs e)
        {
            transactions.Visibility = Visibility.Hidden;
            withdrawal_Deposit.Visibility = Visibility.Visible;
           
         
        }

        private void Balance_Click(object sender, RoutedEventArgs e)
        {
          
            BalanceCanvas.Visibility = Visibility.Visible;
            transactions.Visibility = Visibility.Hidden;
            Deposit deposit = new Deposit();
            StreamReader xs = File.OpenText("Amounts_Per_Bank");
            string read = "";
           
            while (xs != null)
            {

            }
            TxtDisplay.Text = $"SLMC BANK (Pty)Ltd\nCape Town South Africa\n DElft South\n Surbarban Symphony Way\n 7100\n\n\n You Have {0:C} Balnce Left in your Account.";
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                withdrawal_Deposit.Visibility = Visibility.Hidden;
                transactions.Visibility = Visibility.Visible;
                BalanceCanvas.Visibility = Visibility.Visible;
                Deposit depositing = new Deposit();
                double geld = Convert.ToDouble(textBox4.Text);
                depositing.Deposited(geld);
                TxtDisplay.Text += $"SLMC BANK (Pty)Ltd\n Cape Town South Africa\nDelft South\n  Surbarban Symphony Way\n7100\n\n{srnametxt.Text}  {nametxt.Text}\nBalnce on {acc_txt.Text} :{depositing.Deposited(geld):C}";
              
            }
            catch (Exception)
            {
                MessageBox.Show("Please put Amount you want to Deposit !","SLMC BANK (Pty)Ltd", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        
    }
}
