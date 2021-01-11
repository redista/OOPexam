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

namespace OOPexam
{
    /* Ronnie Conlon, s00200671
     * Github Link : https://github.com/redista/OOPexam
     */

    public partial class MainWindow : Window
    {
        // Initialize the list of accounts, and the filtered list
        List<Account> Accounts = new List<Account>();
        List<Account> FilteredAccounts = new List<Account>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // On window load 
            
            // Create two new accounts, (I made one saving one current)
            Account Acc1 = new CurrentAccount() {AccNo = 1, FirstName = "John", LastName = "Doe", Balance = 10000, InterestDate = new DateTime(2019,05,10)};
            Account Acc2 = new SavingsAccount() {AccNo = 2, FirstName = "Jane", LastName = "Lyon", Balance = 20000, InterestDate = new DateTime(2010,10,20)};

            // Add them to the list
            Accounts.Add(Acc1);
            Accounts.Add(Acc2);

            //Set the source of the listbox to the accounts list
            LbxAccounts.ItemsSource = Accounts;

            // Check both boxes. This will call the selection event twice ( it's only 2 accounts right now though )
            CAcheck.IsChecked = true;
            SAcheck.IsChecked = true;
        }

        private void Tcheck_Checked(object sender, RoutedEventArgs e)
        {
            // Event for Current/Savings Account checkboxes. Filteres the list depending on which are checked.

            // Clear the list of filtered accounts
            FilteredAccounts.Clear();
            LbxAccounts.ItemsSource = null;

            // If both are checked, it's just the normal list
            if (CAcheck.IsChecked == true && SAcheck.IsChecked == true)
            {
                LbxAccounts.ItemsSource = Accounts;
            }
            else
            {
                // If current is checked
                if (CAcheck.IsChecked == true)
                {
                    foreach (Account account in Accounts)
                    {
                        if (account is CurrentAccount)
                            FilteredAccounts.Add(account);
                    }
                }
                // If savings is checked
                else if (SAcheck.IsChecked == true)
                {
                    foreach (Account account in Accounts)
                    {
                        if (account is SavingsAccount)
                            FilteredAccounts.Add(account);
                    }
                }

                // If neither are checked, the list will be empty anyways
                LbxAccounts.ItemsSource = FilteredAccounts;
            }

        }

        private void LbxAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Event for changing the selected item in the listbox
            // Adds values of selected item to the textbocks

            // Get the selected account
            Account SelectedAccount = LbxAccounts.SelectedItem as Account;

            // If it not null, set the textblocks to the values of the account
            if (SelectedAccount != null)
            {
                FNtb.Text = SelectedAccount.FirstName;
                LNtb.Text = SelectedAccount.LastName;
                Balancetb.Text = SelectedAccount.Balance.ToString();
                InterestDatetb.Text = SelectedAccount.InterestDate.ToString();
                
                // If the account is a current account, set the text as a current account, otherwise right now it's a savings account.
                AccountTypetb.Text = SelectedAccount is CurrentAccount ? "Current" : "Savings";
            }

        }

        private void DepositBtn_Click(object sender, RoutedEventArgs e)
        {
            // Click event for Deposit button. This adds the transaction amount in the tbx to the balance of the selected account

            Account SelectedAccount = LbxAccounts.SelectedItem as Account;

            decimal TransactionAmount = 0;

            if (SelectedAccount != null)
            {
                // Try to parse the text into a decimal
                if (decimal.TryParse(Transactiontbx.Text, out TransactionAmount))
                {
                    SelectedAccount.Balance += TransactionAmount;

                    // Update the showed balance. Empty the transaction field
                    Balancetb.Text = SelectedAccount.Balance.ToString();
                    Transactiontbx.Text = "";
                }
            }
        }

        private void WithdrawBtn_Click(object sender, RoutedEventArgs e)
        {
            // Click event for withdraw button. Removes the transaction tbx amount from the balance of the account

            Account SelectedAccount = LbxAccounts.SelectedItem as Account;

            decimal TransactionAmount = 0;

            if (SelectedAccount != null)
            {
                // Try to parse the tbx value into a decimal
                if (decimal.TryParse(Transactiontbx.Text, out TransactionAmount))
                {
                    SelectedAccount.Balance -= TransactionAmount;

                    // Update tb fields
                    Balancetb.Text = SelectedAccount.Balance.ToString();
                    Transactiontbx.Text = "";
                }
            }
        }

        private void InterestBtn_Click(object sender, RoutedEventArgs e)
        {
            // Click Event for interest button. This adds the interest to the current balance only if the last time 
            // the interest was updated was over a year ago.

            Account SelectedAccount = LbxAccounts.SelectedItem as Account;

            if (SelectedAccount != null)
            {
                // This will call the CalculateInterest method. It returns true if successful, false otherwise
                if (SelectedAccount.CalculateInterest())
                {
                    // Update the tb fields. 
                    Balancetb.Text = SelectedAccount.Balance.ToString();
                    InterestDatetb.Text = SelectedAccount.InterestDate.ToString();
                }
                else
                {
                    // Error box since interest has been applied within the year.
                    MessageBox.Show("Interest has already been applied within the past year");
                }
            }
        }
    }
}
