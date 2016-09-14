using Murtain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Localization.Language.Db
{
    public class LanguageChangedEvent : DomainEvent
    {

        public string LanguageValue { get; set; }


        public string LanguageName { get; set; }


        public string LanguageSourceName { get; set; }
    }
}
