using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.AutoMapper
{

    public interface IAutoMaping
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
