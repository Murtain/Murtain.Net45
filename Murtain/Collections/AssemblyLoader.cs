using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Murtain.Collections
{
    public class AssemblyLoader : IAssemblyLoader
    {
        private string assemblySkipLoaderParttern = "^System|^vshost32|^Nito.AsyncEx|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^NSubstitute|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Telerik|^Iesi|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease|^IdentityServer3";
        private string assemblyLoaderParttern;
        public AssemblyLoader()
        {

        }

        public AssemblyLoader(string assemblyLoaderParttern)
        {
            this.assemblyLoaderParttern = assemblyLoaderParttern;
        }

        public Assembly[] GetAssemblies()
        {
            var path = GetPhysicalPath(AppDomain.CurrentDomain.BaseDirectory);
            return FilterSystemAssembly(GetAssemblies(path)).ToArray();
        }
        public List<string> GetAllFiles(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly).ToList();
        }
        public List<Assembly> GetAssemblies(string directoryPath)
        {
            var filePaths = GetAllFiles(directoryPath).Where(t => t.EndsWith(".exe") || t.EndsWith(".dll"));
            return filePaths.Select(Assembly.LoadFrom).ToList();
        }
        public string GetPhysicalPath(string relativePath)
        {
            if (HttpContext.Current == null)
            {
                if (relativePath.StartsWith("~"))
                {
                    relativePath = relativePath.Remove(0, 2);
                }
                return Path.GetFullPath(relativePath);
            }
            if (relativePath.StartsWith("~"))
            {
                return HttpContext.Current.Server.MapPath(relativePath);
            }

            if (relativePath.StartsWith("/") || relativePath.StartsWith("\\"))
            {
                return HttpContext.Current.Server.MapPath("~" + relativePath);
            }
            if (HttpContext.Current != null)
            {
                return relativePath + "bin";
            }
            return HttpContext.Current.Server.MapPath("~/" + relativePath);
        }
        public Assembly[] FilterSystemAssembly(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .Where(assembly => string.IsNullOrEmpty(assemblyLoaderParttern)
                                ? !Regex.IsMatch(assembly.FullName, assemblySkipLoaderParttern, RegexOptions.IgnoreCase | RegexOptions.Compiled)
                                : Regex.IsMatch(assembly.FullName, assemblyLoaderParttern, RegexOptions.IgnoreCase | RegexOptions.Compiled)
                        )
                .ToArray();
        }
        public Type[] GetAllTypes(Func<Type, bool> predicate)
        {
            var allTypes = new List<Type>();

            foreach (var assembly in GetAssemblies())
            {
                try
                {
                    allTypes.AddRange(assembly.GetTypes().Where(type => type != null));
                }
                catch (ReflectionTypeLoadException e)
                {
                    throw e;
                }
            }

            return allTypes.Where(predicate).ToArray();
        }

        public Type[] GetAllTypes(List<Assembly> assemblies, Func<Type, bool> predicate)
        {
            var allTypes = new List<Type>();

            foreach (var assembly in assemblies)
            {
                try
                {
                    allTypes.AddRange(assembly.GetTypes().Where(type => type != null));
                }
                catch (ReflectionTypeLoadException e)
                {
                    throw e;
                }
            }

            return allTypes.Where(predicate).ToArray();
        }
    }
}