using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Runtime.Validation
{

    public interface ICustomValidate : IValidate
    {
        void AddValidationErrors(List<ValidationResult> results);
    }
}
