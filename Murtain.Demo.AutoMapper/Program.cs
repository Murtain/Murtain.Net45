using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Configuration.Startup;
using Murtain.AutoMapper;
using AutoMapper;

namespace Murtain.Demo.AutoMapper
{
    class Program
    {

        static void Main(string[] args)
        {
            StartupConfig.RegisterDependency(config =>
            {
                config.UseAutoMapper();
            });


            var entity = new Entity
            {

                ClassNo = "1",
                Name = "小明"
            };


            var dto = entity.MapTo<Dto>();

            Console.WriteLine(dto.StudentName);

            Console.ReadKey();
        }
    }


    [AutoMap(typeof(Dto))]
    public class Entity : IAutoMaping
    {
        public string ClassNo { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity, Dto>()
                .ForMember(x => x.StudentName, opt => opt.MapFrom(m => m.Name));
        }
    }


    public class Dto
    {
        public string ClassNo { get; set; }
        public string StudentName { get; set; }
    }
}
