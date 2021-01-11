using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPexam
{
    public abstract class Account
    {
        // Right now the AccNo is manually applied. Can be done automatically when constructors are added.
        public int AccNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public DateTime InterestDate { get; set; }

        public abstract bool CalculateInterest();

        // Displaying the class as a string. Used for the lisbox.
        public override string ToString()
        {
            return $"{AccNo} - {LastName}, {FirstName}"; 
        }
    }

    public class CurrentAccount : Account
    {
        // Interest rate of current account, 3%
        public decimal InterestRate = 0.03m;

        public override bool CalculateInterest()
        {
            // Works by removing a year from the current time. If the InterestDate is less than (Now - 1 year), clearly
            // then it's over a year ago.
            if (InterestDate < DateTime.Now.AddYears(-1))
            {
                // Set the InterestDate to now, add the interest to the balance.
                InterestDate = DateTime.Now;
                Balance = Balance + (Balance * InterestRate);
                // return successful
                return true;
            }
            return false;
        }
    }
    
    public class SavingsAccount : Account
    {
        // Interest rate of savings account, 6%
        public decimal InterestRate = 0.06m;

        public override bool CalculateInterest()
        {
            // Same as CurrentAccount
            if (InterestDate < DateTime.Now.AddYears(-1))
            {
                InterestDate = DateTime.Now;
                Balance = Balance + (Balance * InterestRate);
                return true;
            }
            return false;
        }
    }
}
