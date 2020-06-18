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

        [Required(ErrorMessage ="Please determine the quantity")]
        [Range(1 ,int.MaxValue ,ErrorMessage ="Quantity must be greater than 0")]
        public int Quantity { get; set; }

      
    }
}