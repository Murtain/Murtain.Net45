using Murtain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Localization.Dictionaries.Db
{
    public class LanguageTextChangedEvent : DomainEvent
    {

        public string LanguageTextValue { get; set; }


        public string LanguageTextName { get; set; }


        public string LanguageTextSourceName { get; set; }
    }
}
