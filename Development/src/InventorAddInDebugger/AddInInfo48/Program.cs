using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MiNa.AddInInfo48
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set output encoding
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Check the arguments
            if (args == null || args.Length == 0)
                return;

            // Get assembly file from arguments
            string assemblyFile = args[0];
            if (!File.Exists(assemblyFile))
            {
                Environment.Exit((int)ExitCodes.FileNotFound);
            }
            

            try
            {
                var assembly = Assembly.LoadFrom(args[0]);
                foreach (var definedType in assembly.DefinedTypes)
                {
                    // Type must implement Inventor.ApplicationAddInServer
                    var implementedInterfaces = definedType.ImplementedInterfaces;

                    // FullName comparison is necessary.
                    string applicationAddInServerFullName = "Inventor.ApplicationAddInServer";
                    if (implementedInterfaces.All(x => x.FullName != applicationAddInServerFullName))
                        continue;

                    // Type must contain GuidAttribute with value equal to AddInClientId
                    foreach (var attribute in definedType.CustomAttributes)
                    {
                        if (attribute.AttributeType != typeof(GuidAttribute)) continue;
                        if (attribute.ConstructorArguments.Count <= 0) continue;

                        string clientId = attribute.ConstructorArguments[0].Value?.ToString().ToLowerInvariant();
                        Console.WriteLine("{0};{1}", clientId, definedType.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit((int)ExitCodes.LoadFileFailed);
            }

        }

        public enum ExitCodes
        {
            Success = 0,
            FileNotFound = 1,
            LoadFileFailed = 2

        }
    }


}
