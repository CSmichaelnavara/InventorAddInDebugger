using System.Reflection;

namespace SampleAddIn
{
    public class Info
    {
        public static string GetMessage => "SampleAddInServer\t" + GetVersion;
        static string GetVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
}
