using System;
namespace FBMS2.Core.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public string Description { get; set ;}

        public DateTime ExpiryDate { get; set; }

        public int Quantity { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
