using System.Reflection;
using System.Windows.Forms;
using Inventor;
using MiNa.InventorAddInDebugger.Common;
using MiNa.InventorAddInDebugger.Properties;
using Application = Inventor.Application;

namespace MiNa.InventorAddInDebugger.Commands
{
    internal class AboutCmd:Command
    {
        public AboutCmd(Application inventor) : base(inventor)
        {
        }

        protected override void ExecuteCommand(NameValueMap context)
        {
            string link = "<a href=\"https://github.com/CSmichaelnavara/InventorAddInDebugger\">Project page</a>";
            string message=$@"<p>
<p>{Resources.AddIn_DisplayName}</p>
<p>Version: {Assembly.GetExecutingAssembly().GetName().Version}</p>
<p></p>
<p>See the %s for more information.</p>
</p>";
            ThisApplication.PromptBox(
                message, 
                Resources.AddIn_DisplayName, 
                MessageBoxButtons.OK,
                MessageBoxIcon.Information, 
                MessageBoxDefaultButton.Button1,
                PromptMessageRestrictionsEnum.kDontAllowNoMoreThisSession, 
                link);
        }
    }
}
