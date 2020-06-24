using MedicalStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalStore.Models
{
    public class AdminOrderViewModel
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public Orderstatus OrderStatus { get; set; }

        public UserInfoViewModel User { get; set; }
    }
}