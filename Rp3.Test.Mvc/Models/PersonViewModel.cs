using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rp3.Test.Mvc.Models
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}