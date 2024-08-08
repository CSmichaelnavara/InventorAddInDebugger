using System.Windows.Forms;
using Inventor;
using MiNa.InventorAddInDebugger.Common;
using MiNa.InventorAddInDebugger.Properties;
using Application = Inventor.Application;

namespace MiNa.InventorAddInDebugger.Commands
{
    class ActivateAddInCmd : Command
    {
        private readonly AddInLoader _addInLoader;

        public ActivateAddInCmd(Application inventor, AddInLoader addInLoader) : base(inventor)
        {
            this._addInLoader = addInLoader;
        }

        protected override void ExecuteCommand(NameValueMap context)
        {
            try
            {
                _addInLoader.Activate();
                ThisApplication.BubbleTip(Resources.Msg_SuccessfullyActivated);
            }
            catch (System.Exception ex)
            {
                ThisApplication.PromptBox(Resources.Msg_ActivationOfAddInFailed,
                    Resources.AddIn_DisplayName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    promptStrings: new[] { ex.ToString() });
            }
        }
    }
}