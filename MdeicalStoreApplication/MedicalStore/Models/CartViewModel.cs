using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalStore.Models
{
    public class CartViewModel
    {
        [Required]
        public string UserId { get; set; }

        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

      
    }
}