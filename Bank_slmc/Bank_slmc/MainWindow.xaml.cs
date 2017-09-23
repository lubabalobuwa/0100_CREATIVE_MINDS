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
    //all my classes
    #region
    public class AccountHolder
    {
        

            public string name { get; set; }

            public string surname { get; set; }

            public int age { get; set; }

            public string id { get; set; }

            public string address { get; set; }

            public string occupation { get; set; }

            public string gender { get; set; }

            public string cellphoneNumber { get; set; }
        public override string ToString()
        {
            return "";
        }
        public List<BankAccount> MyAccounts { get; set; }

        public AccountHolder(string givenID)
        {
            this.id = givenID;
            MyAccounts = new List<BankAccount>();
        }
    }
    public class BankAccount
    {
        public double Amount { get; set; }
        public string AccNumber { get; set; }
        public BankAccount(string AccNumber)
        {
            this.AccNumber = AccNumber;
        }
    }
    public class savingsAccount :BankAccount
    {
        public override string ToString()
        {
            return "savings";
        }
        public savingsAccount(string number) : base(number)
        {

        }
    }
    public class ChequeAccount : BankAccount {
        public override string ToString()
        {
            return "cheque";
        }
        public ChequeAccount(string number) : base(number)
        {

        }
    }

    public class BankAccountsList<T,U> :Dictionary<T,U>
    {

    }
    #endregion
    //exception Classes
    #region
        public class InvalidAccountException : ApplicationException
    {
        public override string Message
        {
            get
            {
                return "invalid account. your pin or account number is incorrect";
            }
        }
    }
        public class AgeException : ApplicationException
    {
        public override string Message
        {
            get
            {
                return "Under age. AccountHolders can only be 18 years old or above";
            }
        }
    }
    public class InValidIDException : ApplicationException
    {
        public override string Message
        {
            get
            {
                return "Invalid ID , please double check you Id or your choice of gender";
            }
        }
    }
    public class RepetitionOfAccountTypeException: ApplicationException
    {
        public override string Message
        {
            get
            {
                return "you already have this account type, consider your options";
            }
        }
    }
    public class PinDoesntMatchException : ApplicationException
    {
        public override string Message
        {
            get
            {
                return "you pin doesn't match";
            }
        }
    }

    #endregion

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<AccountHolder> OurAccountHolders;
        BankAccountsList<string, int> allAccounts;
        public MainWindow()
        {
            InitializeComponent();
            //hide some canvases when the program starts
            SignUpForm.Visibility = Visibility.Hidden;
            created.Visibility = Visibility.Hidden;
            transactions.Visibility = Visibility.Hidden;
            withdrawalCanvas.Visibility = Visibility.Hidden;
            withdrawal_Deposit.Visibility = Visibility.Hidden;
            StatementDisplay.Visibility = Visibility.Hidden;
            transfers.Visibility = Visibility.Hidden;
            checkBalanceCanvas.Visibility = Visibility.Hidden;


            OurAccountHolders = new List<AccountHolder>();
            allAccounts = new BankAccountsList<string, int>();


            //when the program starts , we display some animation
        }
        //all required methods
        #region
            /// <summary>
            /// check how many account types the user has
            /// </summary>
            /// <param name="NumberOfAccountTypes"></an accountholder>
            /// <returns></returns>
            public bool NumberOFAccountTypes(AccountHolder me)
        {
            int SA = 0;
            int CA = 0;
            for (int i =0; i < me.MyAccounts.Count; i++)
            {  
                if (me.MyAccounts[i].ToString() == "savings") SA++;
                else if (me.MyAccounts[i].ToString() == "cheque") CA++;  
            }
            if (SA < 2 || CA < 2) return true;
            else return false;
        }

            /// <summary>
            /// checks if the if the id is valid uisng the last digit
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
        public bool IDValid(string id,string gender)
        {
            int t = 0;
            string u = "";
            int v = 0;
            int w = 0;
            int z = 0;
            List<string> lis = new List<string>();
            foreach (char s in id)
            {
                lis.Add(s.ToString());
            }
            for(int i = 0; i < lis.Count() - 1; i = i + 2)
            {
                int x = Convert.ToInt16(lis[i]);
                t += x;
            }
            for(int i = 1; i < lis.Count; i = i + 2)
            {
                u += lis[i];
            }
            int uu = Convert.ToInt32(u);
            v = uu * 2;
            int sum = 0;
            int dig = 0;
            while (v != 0)
            {
                dig = v % 10;
                sum = sum + dig;
                v = v / 10;
            }
            w = t + sum;
            z = (10 - (w % 10) % 10);
            if (z == Convert.ToInt32(lis[lis.Count - 1]) && CheckGender(id,gender))
            {
                return true;
            }

            return false ;
        }
        /// <summary>
        /// checks if the relationship between the entered age and the id is valid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckGender(string id,string gender)
        {
            List<string> list = new List<string>();
            foreach( char s in id)
            {
                list.Add(s.ToString());
            }
            if (Convert.ToInt32(list[6]) <= 4 && Convert.ToInt32(list[6]) >= 0 && gender.ToLower()=="female") return true;
            if (Convert.ToInt32(list[6]) >= 5 && Convert.ToInt32(list[6]) <= 9 && gender.ToLower() == "male") return true;
            else return false;
        }
        public void UpdateMyDetails(string FileName,AccountHolder me) {
            StreamWriter write = File.CreateText(FileName);
            write.WriteLine(string.Format("name: {0}   surname: {1}", me.name, me.surname));
            write.WriteLine(string.Format("cellphone number:{0}    Age:{1}", me.cellphoneNumber, me.age));
            write.WriteLine(string.Format("ÏD No. : {0}  gender : {1}", me.id, me.gender));
            write.WriteLine(me.address);
            write.WriteLine(string.Format("occupation :{0} ", me.occupation));
            write.Close();
        }

        public bool PinMatches(string a, string b)
        {
            return a == b;
        }
        #endregion

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button9_Click(object sender, RoutedEventArgs e)  //enter button
        {
            try
            {
                if (allAccounts.ContainsKey(AccnText.Text))  // checks if the accountnumber entered already exist
                {
                    if (!(allAccounts[AccnText.Text] == Convert.ToInt32(PinText.Text))) throw new InvalidAccountException();
                    firstDisplay.Visibility = Visibility.Hidden;
                    transactions.Visibility = Visibility.Visible;
                }
                else throw new KeyNotFoundException();
                
            }
            catch(InvalidAccountException a) { MessageBox.Show(a.Message, "", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (KeyNotFoundException) { MessageBox.Show("Invalid account number.Re-entered your account number ", "", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            firstDisplay.Visibility = Visibility.Hidden;
            SignUpForm.Visibility = Visibility.Visible;
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)  // signup button after completing the form
        {
        }

        private void button_Copy_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string Accountnumber = "11009944";
                if (!IDValid(idtxt.Text, genderBox.Text)) throw new InValidIDException();
                if (Convert.ToInt32(agetxt.Text) < 18) throw new AgeException();
                if (addtxt.Text == "" || occupationBox.Text == "" || AccountTypeBox.Text == "")
                {
                    throw new FormatException();
                }
                if (!PinMatches(pintxt.Text, pintxt2.Text)) throw new PinDoesntMatchException();
                AccountHolder me = new AccountHolder(idtxt.Text);
                me.name = nametxt.Text;
                me.surname = srnametxt.Text;
                me.age = Convert.ToInt32(agetxt.Text);
                me.gender = genderBox.Text;
                me.address = addtxt.Text;
                me.occupation = occupationBox.Text;
                switch (AccountTypeBox.Text.ToLower())
                {
                    case "savings":
                        if (!NumberOFAccountTypes(me)) throw new RepetitionOfAccountTypeException();
                        Accountnumber = Accountnumber + "001";
                        savingsAccount mine = new savingsAccount(Accountnumber); me.MyAccounts.Add(mine);
                       
                         allAccounts[Accountnumber] = Convert.ToInt32(pintxt.Text); 

                        break;
                    case "cheque":
                        if (!NumberOFAccountTypes(me)) throw new RepetitionOfAccountTypeException();
                        Accountnumber = Accountnumber+ "002";
                        ChequeAccount mine2 = new ChequeAccount(Accountnumber); me.MyAccounts.Add(mine2);
                        allAccounts[Accountnumber] = Convert.ToInt32(pintxt.Text);
                        break;
                    default: break;
                }

                OurAccountHolders.Add(me);
                UpdateMyDetails(me.id, me);
                SignUpForm.Visibility = Visibility.Hidden;
                created.Visibility = Visibility.Visible;
                AccNuDisplay.Content = Accountnumber;
            }
            catch (PinDoesntMatchException a) { MessageBox.Show(a.Message); }
            catch (InValidIDException w) { MessageBox.Show(w.Message, "", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (AgeException s) { MessageBox.Show(s.Message, "", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (FormatException) { MessageBox.Show("fill in all the required feilds or check if you''ve entered correct details"); }
            catch (RepetitionOfAccountTypeException m) { MessageBox.Show(m.Message, "", MessageBoxButton.OK, MessageBoxImage.Error); }
           
        }
        /// <summary>
        /// user has options , either logsin with new accountnumber or exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonn_2_Click(object sender, RoutedEventArgs e)  //second LogIn button
        {
            created.Visibility = Visibility.Hidden;
            firstDisplay.Visibility = Visibility.Visible;
            Register.Visibility = Visibility.Hidden;

        }

        private void buttonn2_Click(object sender, RoutedEventArgs e)
        {
            transactions.Visibility = Visibility.Hidden;
            withdrawal_Deposit.Visibility = Visibility.Visible;
        }

        private void button19_Click(object sender, RoutedEventArgs e)
        {
            transactions.Visibility = Visibility.Hidden;
            StatementDisplay.Visibility = Visibility.Visible;
        }

        private void buttonn3_Click(object sender, RoutedEventArgs e)
        {
            transactions.Visibility = Visibility.Hidden;
            withdrawalCanvas.Visibility = Visibility.Visible;
        }

        private void button20_Click(object sender, RoutedEventArgs e)
        {
            transactions.Visibility = Visibility.Hidden;
            transfers.Visibility = Visibility.Visible;
        }

        private void button18_Click(object sender, RoutedEventArgs e)
        {
            checkBalanceCanvas.Visibility = Visibility.Visible;
        }
    }
}
