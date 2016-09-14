using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Murtain.OAuth2.Web.Models
{
    public class PassportSecurityViewModel
    {
        public string Telphone { get; set; }

        public string Email { get; set; }

        public IList<Questions> Questions { get; set; }
    }

    public class Questions
    {
        public string Question { get; set; }

        public string Ask { get; set; }
    }
}