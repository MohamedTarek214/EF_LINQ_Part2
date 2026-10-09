using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    internal class Order
    {
        
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customers { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
