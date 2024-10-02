using System.Collections.Generic;

namespace MiNa.InventorAddInDebugger
{

    /// <summary>
    /// AddIn loader configuration
    /// </summary>
    public class AddInLoaderConfig
    {
        /// <summary>
        /// Gets and sets the full file name of the original build of the AddIn
        /// </summary>
        public string AddInAssemblyFile { get; set; }

        /// <summary>
        /// Gets and sets the ClientId of the AddIn
        /// </summary>
        public string AddInClientId { get; set; }

        /// <summary>
        /// Gets and sets the type FullName of the AddIn
        /// </summary>
        public string AddInFullName { get; set; }

        /// <summary>
        /// Loads debugged AddIn when inventor starts
        /// </summary>
        public bool LoadOnStart { get; set; }

        public List<AddInInfo> MruAddInInfos { get; set; } = new List<AddInInfo>();
    }
}