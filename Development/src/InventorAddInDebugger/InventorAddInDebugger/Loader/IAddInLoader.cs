using System.IO;

namespace MiNa.InventorAddInDebugger.Loader;

internal interface IAddInLoader
{
    /// <summary>
    /// Gets the full file name of the original build of the AddIn
    /// </summary>
    string AddInAssemblyFile { get; }

    /// <summary>
    /// Gets the ClientId of the original AddIn
    /// </summary>
    string AddInClientId { get; }

    /// <summary>
    /// Gets the full file name of the last available AddIn version
    /// </summary>
    string LastVersionFile { get; }

    /// <summary>
    /// Activates the AddIn if it is not activated before
    /// </summary>
    void Activate();

    /// <summary>
    /// Deactivates the AddIn if it is not deactivated before
    /// </summary>
    void Deactivate();

    /// <summary>
    /// Gets the information if the newer version of AddIn is available
    /// </summary>
    /// <param name="buildDir">Original build directory</param>
    /// <param name="dllFileName">AddIn file name</param>
    /// <param name="versionDllFullFileName">Returns the full file name of the last version build.</param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    bool IsNewVersionAvailable(string buildDir, string dllFileName, out string versionDllFullFileName);
}