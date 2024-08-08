using System.Reflection;

namespace SampleAddIn2025
{
    public class Info
    {
        public static string GetMessage => "SampleAddInServer2025\t" + GetVersion;
        static string GetVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
}
