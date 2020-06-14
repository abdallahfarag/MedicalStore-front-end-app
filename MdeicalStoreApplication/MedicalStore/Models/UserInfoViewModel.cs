using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalStore.Models
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }
}