using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPexam
{
    public abstract class Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public DateTime InterestDate { get; set; }

        public abstract decimal CalculateInterest(); 
    }

    public class CurrentAccount : Account
    {
        public decimal InterestRate = 0.03m;

        public override decimal CalculateInterest()
        {
            
        }
    }
    
    public class SavingsAccount : Account
    {
        public decimal InterestRate = 0.06m;
    }
}
