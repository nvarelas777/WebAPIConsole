using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo1.Models
{
    public class StudentViewModel
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }

        public AddressViewModel Address { get; set; }
        public StandardViewModel Standard { get; set; }
    }
}