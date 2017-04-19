using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Murtain.Web.ApiDocument.Areas.HelpPage.ModelDescriptions
{
    public class ComplexTypeModelDescription : ModelDescription
    {
        public ComplexTypeModelDescription()
        {
            Properties = new Collection<ParameterDescription>();
            Aniotations = new Collection<ParameterAnnotation>();
        }
        public Collection<ParameterAnnotation> Aniotations { get; private set; }
        public Collection<ParameterDescription> Properties { get; private set; }
    }
}