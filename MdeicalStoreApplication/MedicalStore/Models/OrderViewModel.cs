using MedicalStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalStore.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [DisplayName("Total price")]
        [Required]
        public decimal TotalPrice { get; set; }
        [DisplayName("Date added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
        [DisplayName("Delivered")]
        [Required]
        public Orderstatus OrderStatus { get; set; }
        [Required]
        public string OrderAddress { get; set; }
        [Phone]
        [Required]
        public string ContactPhone { get; set; }
        [DisplayName("Feedback")]
        public string FeedBack { get; set; }
        [Required]
        public string UserId { get; set; }

    }
}