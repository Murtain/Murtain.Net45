using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Murtain.OAuth2.Web.Models
{
    public class PassportUserViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SubjectId { get; set; }

        public string HeadImageUrl { get; set; }
    }
}