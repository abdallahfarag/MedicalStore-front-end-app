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
        [Required]
        [DisplayName("Order status")]
        public Orderstatus OrderStatus { get; set; }
        [DisplayName("Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        [DisplayName("Order address*")]
        public string OrderAddress { get; set; }
        [Phone]
        [Required]
        [DisplayName("Phone*")]
        public string ContactPhone { get; set; }
        [DisplayName("FeedBack(Optional)*")]
        public string FeedBack { get; set; }
        [Required]
        public string UserId { get; set; }

    }
}