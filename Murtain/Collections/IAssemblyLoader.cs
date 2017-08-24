using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Collections
{
    public interface IAssemblyLoader
    {
        Assembly[] GetAssemblies();

        List<string> GetAllFiles(string directoryPath);

        List<Assembly> GetAssemblies(string directoryPath);

        string GetPhysicalPath(string relativePath);

        Assembly[] FilterSystemAssembly(IEnumerable<Assembly> assemblies);

        Type[] GetAllTypes(Func<Type, bool> predicate);
    }
}
