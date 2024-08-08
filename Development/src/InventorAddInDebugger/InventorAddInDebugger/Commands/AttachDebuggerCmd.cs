using System.Diagnostics;
using Inventor;
using Application = Inventor.Application;

namespace MiNa.InventorAddInDebugger.Commands
{
    internal class AttachDebuggerCmd : Command
    {
        public AttachDebuggerCmd(Application inventor) : base(inventor)
        {
        }

        protected override void ExecuteCommand(NameValueMap context)
        {
            Debugger.Launch();
        }
    }
}