using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    internal class Borrower
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public DateOnly MembershipDate { get; set; }
        public List<Loan> Loans { get; set; }


    }
}
