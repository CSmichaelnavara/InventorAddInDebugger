using System.Windows.Forms;
using Inventor;
using MiNa.InventorAddInDebugger.Common;
using MiNa.InventorAddInDebugger.Properties;
using Application = Inventor.Application;

namespace MiNa.InventorAddInDebugger.Commands
{
    class DeactivateAddInCmd : Command
    {
        private readonly AddInLoader _addInLoader;

        public DeactivateAddInCmd(Application inventor, AddInLoader addInLoader) : base(inventor)
        {
            this._addInLoader = addInLoader;
        }

        protected override void ExecuteCommand(NameValueMap context)
        {
            try
            {
                _addInLoader.Deactivate();
                ThisApplication.BubbleTip(Resources.Msg_SuccessfullyDeactivated);
            }
            catch (System.Exception ex)
            {
                ThisApplication.PromptBox(Resources.Msg_DeactivationOfAddInFailed,
                    Resources.AddIn_DisplayName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    promptStrings: new[] { ex.ToString() });
            }
        }
    }
}