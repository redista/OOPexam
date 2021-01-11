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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Account Acc1 = new CurrentAccount() {AccNo = 1, FirstName = "John", LastName = "Doe", Balance = 10000, InterestDate = DateTime.Now};
            Account Acc2 = new SavingsAccount() {AccNo = 1, FirstName = "Jane", LastName = "Lyon", Balance = 20000};

            Accounts.Add(Acc1);
            Accounts.Add(Acc2);
        }

        private void Tcheck_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
