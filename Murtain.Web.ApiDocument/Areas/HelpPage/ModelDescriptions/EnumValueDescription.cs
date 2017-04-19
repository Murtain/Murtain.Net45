using System.Collections.ObjectModel;

namespace Murtain.Web.ApiDocument.Areas.HelpPage.ModelDescriptions
{
    public class EnumValueDescription
    {
        public EnumValueDescription()
        {

            Aniotations = new Collection<ParameterAnnotation>();
        }
        public Collection<ParameterAnnotation> Aniotations { get; private set; }
        public string Documentation { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}