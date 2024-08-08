using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleAddInModule
{
    public class Info
    {
        public static string GetMessage => "SampleAddInModuleNet8\t" + GetVersion;
        static string GetVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
}
