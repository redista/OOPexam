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
            Account Acc1 = new CurrentAccount() {AccNo = 1, FirstName = "John", LastName = "Doe", Balance = 10000, InterestDate = DateTime.Now};
            Account Acc2 = new SavingsAccount() {AccNo = 2, FirstName = "Jane", LastName = "Lyon", Balance = 20000};

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
            // Sets the employee selected to geneiric employee class
            Account SelectedAccount = LbxAccounts.SelectedItem as Account;

            // If it is null, don't do anything
            if (SelectedAccount != null)
            {
                FNtb.Text = SelectedAccount.FirstName;
                LNtb.Text = SelectedAccount.FirstName;
                Balancetb.Text = SelectedAccount.FirstName;
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
    }
}
