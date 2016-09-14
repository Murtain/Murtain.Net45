using Castle.Core.Logging;
using Murtain.Localization.Dictionaries;
using Murtain.Localization.Dictionaries.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Localization.Sources.Db
{
    public class DbLocalizationSource : DictionaryBasedLocalizationSource
    {
        public DbLocalizationSource(string name, DbLocalizationDictionaryProvider dictionaryProvider) 
            : base(name, dictionaryProvider)
        {
        }
    }
}
