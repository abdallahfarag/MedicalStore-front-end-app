using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalStore.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is mandatory")]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description Name is mandatory")]
        public string Description { get; set; }

        [DisplayName("Quantity in stock")]
        [Range(0, 100000)]
        public int QuantityInStock { get; set; }

        [Required]
        public string Image { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}