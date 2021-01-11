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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Account> Accounts = new List<Account>();
        List<Account> FilteredAccounts = new List<Account>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Account Acc1 = new CurrentAccount() {AccNo = 1, FirstName = "John", LastName = "Doe", Balance = 10000, InterestDate = new DateTime(2019,05,10)};
            Account Acc2 = new SavingsAccount() {AccNo = 2, FirstName = "Jane", LastName = "Lyon", Balance = 20000, InterestDate = new DateTime(2010,10,20)};

            Accounts.Add(Acc1);
            Accounts.Add(Acc2);

            LbxAccounts.ItemsSource = Accounts;

            CAcheck.IsChecked = true;
            SAcheck.IsChecked = true;
        }

        private void Tcheck_Checked(object sender, RoutedEventArgs e)
        {
            FilteredAccounts.Clear();

            LbxAccounts.ItemsSource = null;

            if (CAcheck.IsChecked == true && SAcheck.IsChecked == true)
            {
                LbxAccounts.ItemsSource = Accounts;
            }
            else
            {
                if (CAcheck.IsChecked == true)
                {
                    foreach (Account account in Accounts)
                    {
                        if (account is CurrentAccount)
                            FilteredAccounts.Add(account);
                    }
                }
                else if (SAcheck.IsChecked == true)
                {
                    foreach (Account account in Accounts)
                    {
                        if (account is SavingsAccount)
                            FilteredAccounts.Add(account);
                    }
                }

                LbxAccounts.ItemsSource = FilteredAccounts;
            }

        }

        private void LbxAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Account SelectedAccount = LbxAccounts.SelectedItem as Account;

            // If it is null, don't do anything
            if (SelectedAccount != null)
            {
                FNtb.Text = SelectedAccount.FirstName;
                LNtb.Text = SelectedAccount.LastName;
                Balancetb.Text = SelectedAccount.Balance.ToString();
                InterestDatetb.Text = SelectedAccount.InterestDate.ToString();
                AccountTypetb.Text = SelectedAccount is CurrentAccount ? "Current" : "Savings";
            }

        }

        public void Clear()
        {
            FNtb.Text = "";
            LNtb.Text = "";
            Balancetb.Text = "";
            AccountTypetb.Text = "";
            InterestDatetb.Text = "";
        }

        private void DepositBtn_Click(object sender, RoutedEventArgs e)
        {
            Account SelectedAccount = LbxAccounts.SelectedItem as Account;

            decimal TransactionAmount = 0;

            if (SelectedAccount != null)
            {
                if (decimal.TryParse(Transactiontbx.Text, out TransactionAmount))
                {
                    SelectedAccount.Balance += TransactionAmount;

                    Balancetb.Text = SelectedAccount.Balance.ToString();
                    Transactiontbx.Text = "";
                }
            }
        }

        private void WithdrawBtn_Click(object sender, RoutedEventArgs e)
        {
            Account SelectedAccount = LbxAccounts.SelectedItem as Account;

            decimal TransactionAmount = 0;

            if (SelectedAccount != null)
            {
                if (decimal.TryParse(Transactiontbx.Text, out TransactionAmount))
                {
                    SelectedAccount.Balance -= TransactionAmount;

                    Balancetb.Text = SelectedAccount.Balance.ToString();
                    Transactiontbx.Text = "";
                }
            }
        }

        private void InterestBtn_Click(object sender, RoutedEventArgs e)
        {
            

            Account SelectedAccount = LbxAccounts.SelectedItem as Account;

            if (SelectedAccount != null)
            {
                if (!SelectedAccount.CalculateInterest())
                {
                    MessageBox.Show("Interest has already been applied within the past year");
                }
                else
                {
                    Balancetb.Text = SelectedAccount.Balance.ToString();
                    InterestDatetb.Text = SelectedAccount.InterestDate.ToString();
                }
            }
        }
    }
}
