using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPexam
{
    public abstract class Account
    {
        public int AccNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public DateTime InterestDate { get; set; }

        public abstract decimal CalculateInterest();

        public override string ToString()
        {
            return $"{AccNo} - {LastName}, {FirstName}"; 
        }
    }

    public class CurrentAccount : Account
    {
        public decimal InterestRate = 0.03m;

        public override decimal CalculateInterest()
        {
            return Balance * InterestRate;
        }
    }
    
    public class SavingsAccount : Account
    {
        public decimal InterestRate = 0.06m;

        public override decimal CalculateInterest()
        {
            return Balance * InterestRate;
        }
    }
}
