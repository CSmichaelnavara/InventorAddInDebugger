using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MiNa.InventorAddInDebugger
{
    /// <summary>
    /// Provides functionality for loading referenced assemblies
    /// </summary>
    public class ReferencesLoader
    {
        private Dictionary<string, string> _listOfAssemblies;
        private string _lastBuildFolder;

        /// <summary>
        /// Creates new instance of ReferencesLoader
        /// </summary>
        public ReferencesLoader()
        {
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        /// <summary>
        /// Gets and sets the directory with the last build of AddIn
        /// </summary>
        public string LastBuildFolder
        {
            get => _lastBuildFolder;
            set
            {
                _lastBuildFolder = value;
                LoadAssembliesFromDir();
            }
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (_listOfAssemblies == null) LoadAssembliesFromDir();

            try
            {
                var file = _listOfAssemblies[args.Name];
                var assembly = Assembly.LoadFile(file);
                return assembly;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void LoadAssembliesFromDir()
        {
            _listOfAssemblies = new Dictionary<string, string>();

            var files = Directory.EnumerateFiles(LastBuildFolder, "*.dll", SearchOption.AllDirectories).ToArray();
            foreach (var file in files)
            {
                var reflectionAssy = Assembly.LoadFile(file);
                var fullName = reflectionAssy.FullName;
                if (!_listOfAssemblies.ContainsKey(fullName))
                    _listOfAssemblies.Add(fullName, file);
            }
        }
    }
}