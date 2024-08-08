using System.Windows.Forms;
using Inventor;
using MiNa.InventorAddInDebugger.Common;
using MiNa.InventorAddInDebugger.Properties;
using Application = Inventor.Application;

namespace MiNa.InventorAddInDebugger.Commands
{
    class ReloadAddInCmd : Command
    {
        private readonly AddInLoader _addInLoader;

        public ReloadAddInCmd(Application inventor, AddInLoader addInLoader) : base(inventor)
        {
            this._addInLoader = addInLoader;
        }

        protected override void ExecuteCommand(NameValueMap context)
        {
            try
            {
                _addInLoader.Deactivate();
                _addInLoader.Activate();
                ThisApplication.BubbleTip(Resources.Msg_SuccessfullyReloaded);
            }
            catch (System.Exception ex)
            {
                ThisApplication.PromptBox(Resources.Msg_ReloadOfAddInfailed,
                    Resources.AddIn_DisplayName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    promptStrings: new[] { ex.ToString() });
            }
        }
    }
}