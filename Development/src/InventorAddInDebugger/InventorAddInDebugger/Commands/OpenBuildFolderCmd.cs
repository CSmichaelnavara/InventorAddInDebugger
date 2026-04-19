using System.Diagnostics;
using Inventor;
using MiNa.InventorAddInDebugger.Loader;
using Application = Inventor.Application;

namespace MiNa.InventorAddInDebugger.Commands
{
    internal class OpenBuildFolderCmd : Command
    {
        private readonly IAddInLoader _addInLoader;

        public OpenBuildFolderCmd(Application inventor, IAddInLoader addInLoader) : base(inventor)
        {
            _addInLoader = addInLoader;
        }

        protected override void ExecuteCommand(NameValueMap context)
        {
            var startInfo = new ProcessStartInfo("explorer.exe", $"/select,\"{_addInLoader.LastVersionFile}\"")
                { UseShellExecute = true };
            Process.Start(startInfo);
        }
    }
}