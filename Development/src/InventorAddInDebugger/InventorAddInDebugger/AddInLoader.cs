using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Inventor;
using MiNa.InventorAddInDebugger.Common;
using MiNa.InventorAddInDebugger.Properties;
using File = System.IO.File;
using Path = System.IO.Path;

namespace MiNa.InventorAddInDebugger
{
    /// <summary>
    /// Provides functionality for loading AddIns
    /// </summary>
    internal class AddInLoader
    {
        private readonly ApplicationAddInSite _addInSiteObject;
        private readonly ReferencesLoader _referencesLoader;
        private Application _inventor;

        private ApplicationAddInServer _addInServer;
        private bool _firstTime = true;
        private bool _isActivated;

        /// <summary>
        /// Creates new instance of AddInLoader
        /// </summary>
        /// <param name="addInSiteObject"></param>
        public AddInLoader(ApplicationAddInSite addInSiteObject)
        {
            _addInSiteObject = addInSiteObject;
            _inventor = addInSiteObject.Application;
            _referencesLoader = new ReferencesLoader();
        }

        /// <summary>
        /// Gets the full file name of the original build of the AddIn
        /// </summary>
        public string AddInAssemblyFile => Config.AddInAssemblyFile;

        /// <summary>
        /// Gets the ClientId of the original AddIn
        /// </summary>
        public string AddInClientId => Config.AddInClientId;

        /// <summary>
        /// Gets the full file name of the last available AddIn version
        /// </summary>
        public string LastVersionFile { get; private set; }

        private AddInLoaderConfig Config => StandardAddInServer.Config;

        /// <summary>
        /// Activates the AddIn if it is not activated before
        /// </summary>
        public void Activate()
        {
            if (_isActivated)
            {
                _inventor.BubbleTip(Resources.Msg_AlreadyActivated);
                return;
            }

            if (_addInServer == null)
                _addInServer = GetStandardAddInServer();

            if (_addInServer == null)
            {
                _inventor.BubbleTip(Resources.Msg_AddInNotFound);
                return;
            }

            _addInServer.Activate(_addInSiteObject, _firstTime);
            _isActivated = true;
        }

        /// <summary>
        /// Deactivates the AddIn if it is not deactivated before
        /// </summary>
        public void Deactivate()
        {
            if (!_isActivated)
            {
                _inventor.BubbleTip(Resources.Msg_AddInNotActivated);
                return;
            }

            if (_addInServer == null)
                return;

            _addInServer.Deactivate();
            _addInServer = null;

            _isActivated = false;
        }

        /// <summary>
        /// Gets the information if the newer version of AddIn is available
        /// </summary>
        /// <param name="buildDir">Original build directory</param>
        /// <param name="dllFileName">AddIn file name</param>
        /// <param name="versionDllFullFileName">Returns the full file name of the last version build.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public bool IsNewVersionAvailable(string buildDir, string dllFileName, out string versionDllFullFileName)
        {
            var dllFullFileName = Path.Combine(buildDir, dllFileName);
            if (!File.Exists(dllFullFileName))
                throw new FileNotFoundException("Add-in assembly file not found.", dllFullFileName);

            var versionInfo = FileVersionInfo.GetVersionInfo(dllFullFileName);

            var versionDir = Path.Combine($"{buildDir}Versions", versionInfo.FileVersion);
            versionDllFullFileName = Path.Combine(versionDir, dllFileName);

            return !File.Exists(versionDllFullFileName);
        }


        private void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            // Create folder structure
            foreach (var dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));

            // Copy files
            foreach (var fileName in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                File.Copy(fileName, fileName.Replace(sourcePath, targetPath), true);
        }


        private List<AddInInfo> GetAddInsFromAssembly(string lastBuild)
        {
            var addIns = new List<AddInInfo>();
            var addInAssembly = Assembly.LoadFile(lastBuild);
            foreach (var definedType in addInAssembly.DefinedTypes)
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

                    var clientId = attribute.ConstructorArguments[0].Value?.ToString().ToLowerInvariant();
                    addIns.Add(new AddInInfo(definedType.FullName, clientId));
                }
            }

            return addIns;
        }


        /// <summary>
        ///     Returns new instance of Inventor.ApplicationAddInServer defined by
        ///     AddInAssemblyFile and AddInClientId
        /// </summary>
        /// <returns></returns>
        private ApplicationAddInServer GetStandardAddInServer()
        {
            if (string.IsNullOrWhiteSpace(AddInAssemblyFile))
                return null;

            var buildDir = Path.GetDirectoryName(AddInAssemblyFile);
            var lastBuild = SearchLastBuild(buildDir,
                Path.GetFileName(AddInAssemblyFile));

            _referencesLoader.LastBuildFolder = Path.GetDirectoryName(lastBuild);

            var firstOrDefault = GetAddInsFromAssembly(lastBuild).FirstOrDefault(x => x.ClientId.Equals(AddInClientId, StringComparison.InvariantCultureIgnoreCase));
            if (firstOrDefault == null)
                return null;

            var addInAssembly = Assembly.LoadFile(lastBuild);
            var applicationAddInServer =
                addInAssembly.CreateInstance(firstOrDefault.FullName) as ApplicationAddInServer;

            LastVersionFile = lastBuild;
            return applicationAddInServer;
        }

        private string SearchLastBuild(string buildDir, string dllFileName)
        {
            if (!IsNewVersionAvailable(buildDir, dllFileName, out var versionDllFullFileName))
                return versionDllFullFileName;

            // Create directory with new build
            var lastVersionDir = Path.GetDirectoryName(versionDllFullFileName);
            Directory.CreateDirectory(lastVersionDir);
            CopyFilesRecursively(buildDir, lastVersionDir);
            return versionDllFullFileName;
        }
    }
}