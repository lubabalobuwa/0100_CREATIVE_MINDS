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

namespace S_L_M_C
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
 
        Random A = new Random();
        public MainWindow()
        {
            InitializeComponent();

          //  A = new Random(10);
            //logOrSign_Window.Visibility = Visibility.Hidden;
            //STARTS OF HIDING OTHER CANVASES
            SignUp_Details.Visibility = Visibility.Hidden;
            Doer_Window.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// randomly creates a 10 digit account number
        /// </summary>
        /// <returns></returns>
        public string generateNum()
        {
            string y = "";
            for(int u = 0; u < 10; u++)
            {
                int q = A.Next(0, 10);
                y += q.ToString();
            }
            return y;
        }
 
        /// <summary>
        /// clears all textboxes
        /// </summary>
        public void clearTxtB()
        {
            //clears all textboxes
            txtName.Clear();
            txtSurname.Clear();
            txtAge.Clear();
            txtOccupation.Clear();
            txtPassword.Clear();
            txtPhoneNumber.Clear();
            
        }

        private void SIGN_UP_Click(object sender, RoutedEventArgs e)
        {
          //shows desired canvas
            SignUp_Details.Visibility = Visibility.Visible;
            logOrSign_Window.Visibility = Visibility.Hidden;
        }

        private void DETAILER_Click(object sender, RoutedEventArgs e)//add your details
        {
            string r = generateNum();
            Person q = new Person(txtName.Text,txtSurname.Text,Convert.ToInt32(txtAge.Text), Convert.ToString(txtPhoneNumber.Text),txtPassword.Text,txtOccupation.Text);
            if (cmbACC.Text== "ChequeAccount")
            {
                //creates your bank account and saves information into a file
                chequeAcount w = new chequeAcount(r, "CHEQUEACCOUNT");
                q.accounts.Add(w);
                q.create();
                w.addToAC();
                MessageBox.Show("NEW CHEQUEACCOUNT HAS BEEN CREATED");
                clearTxtB();

                //moves on to the next step(or shows next canvas
                SignUp_Details.Visibility = Visibility.Hidden;
                Doer_Window.Visibility = Visibility.Hidden;
            }
            else if(cmbACC.Text == "SavingsAccount")
            {
                //creates your bank account and saves information into a file
                savingAcount w = new savingAcount(r, "SAVINGACCOUNT");
                q.accounts.Add(w);
                q.create();
                w.addToAC();
                MessageBox.Show("NEW SAVINGACCOUNT HAS BEEN CREATED");
                clearTxtB();

                //moves on to the next step(or shows next canvas
                SignUp_Details.Visibility = Visibility.Hidden;
                Doer_Window.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("please selects");

            }

        }

        private void LOGIN_Click(object sender, RoutedEventArgs e)
        {
            bool exists = false;//this will help show if the account number entered exists or not
            string[] accNums = File.ReadAllLines("ACCOUNTS");//reads all acccount numbers into arry
            while (true)
            {
                foreach (string q in accNums)
                {
                    if (q == tLOGtAccN.Text)//checks if account number entered by user is in the array of accountnumbers(checks if valid)
                    {
                        StreamReader w = File.OpenText(q);
                        string w1 = "";
                        while (w1 != null)
                        {
                            w1 = w.ReadLine();
                            //now we check if the password entered is correct
                            if (w1 != null && w1.StartsWith("PASSWORD"))
                            {
                                string[] w2 = w1.Split();
                                if (w2[1].ToString() == tLOGtPassword.Text)
                                {

                                    //shows next step(or next canvas)
                                    logOrSign_Window.Visibility = Visibility.Hidden;
                                    Doer_Window.Visibility = Visibility.Visible;
                                    exists = true;
                                    break;
                                }
                                else//does this when password is incorrect
                                {
                                    MessageBox.Show("invalid password");
                                    break;
                                }
                            }
                            
                        }
                    }

                }
                if(exists == true)//only does this if account number is valid and password is correct
                {
                    MessageBox.Show("LOGIN SUCCESSFULL");
                    break;
                }
                else
                {
                    MessageBox.Show("invalid Account Number");
                    break;
                }
            }
        }


    }
}
