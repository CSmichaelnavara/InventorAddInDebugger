using System.Diagnostics;
using Inventor;
using Application = Inventor.Application;

namespace MiNa.InventorAddInDebugger.Commands
{
    internal class OpenBuildFolderCmd : Command
    {
        private readonly AddInLoader _addInLoader;

        public OpenBuildFolderCmd(Application inventor, AddInLoader addInLoader) : base(inventor)
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