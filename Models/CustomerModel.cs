using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Polymer3D_APIs.Models
{
    public class CustomerModel
    {
        public string customerId { get; set; }
        public string name{ get; set; }
        public string email{ get; set; }
        public string phone{ get; set; }
        public string company{ get; set; }
        public string moreinfo{ get; set; }
        public string customerType { get; set;  }
        public string servicesList { get; set; }
    }
}