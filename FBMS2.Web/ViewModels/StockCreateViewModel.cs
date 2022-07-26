using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FBMS2.Web.ViewModels
{
    public class StockCreateViewModel
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Description { get; set;}

        [Required]
        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }
    }

}