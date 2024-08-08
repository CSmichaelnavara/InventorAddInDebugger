using System;
using System.Windows.Forms;
using Inventor;
using MiNa.InventorAddInDebugger.Common;
using MiNa.InventorAddInDebugger.Properties;
using Application = Inventor.Application;

namespace MiNa.InventorAddInDebugger.Commands
{
    internal abstract class Command
    {
        /// <summary>The same as Application</summary>
        protected readonly Application ThisApplication;

        /// <summary>Creates new addin command</summary>
        /// <param name="inventor"></param>
        public Command(Application inventor)
        {
            ThisApplication = inventor;
        }


        /// <summary>Command button</summary>
        public ButtonDefinition ButtonDefinition { get; private set; }

        /// <summary>
        ///     Returns wrapper for display form as child window of Inventor
        /// </summary>
        public IWin32Window InventorMainWindow => new WindowWrapper((IntPtr)ThisApplication.MainFrameHWND);

        /// <summary>Creates new command button</summary>
        /// <param name="displayName">Display name of the button</param>
        /// <param name="internalName">Internal ID of the button</param>
        /// <param name="commandType">Flags of command type</param>
        /// <param name="clientId">Addin GUID</param>
        /// <param name="description">Description text of the button</param>
        /// <param name="toolTip">Tool tip text of the button</param>
        /// <param name="standardIcon">Small button icon</param>
        /// <param name="largeIcon">Large button icon</param>
        /// <param name="autoAddToGui">Automaticaly add button to AddIns tab</param>
        /// <param name="buttonDisplayType">Display icon, text or both</param>
        public void CreateButton(
            string displayName,
            string description = "",
            string toolTip = "",
            CommandTypesEnum commandType = CommandTypesEnum.kQueryOnlyCmdType,
            string clientId = null,
            CommandIcon icon = null,
            bool autoAddToGui = false,
            ButtonDisplayEnum buttonDisplayType = ButtonDisplayEnum.kDisplayTextInLearningMode)
        {
            string internalName = GetType().FullName;
            ButtonDefinition = ThisApplication.CommandManager.ControlDefinitions.AddButtonDefinition(displayName,
                internalName, commandType, clientId, description, toolTip, icon?.SmallRibboIcon, icon?.LargeRibboIcon,
                buttonDisplayType);

            ButtonDefinition.OnExecute += ButtonDefinition_OnExecute;

            if (autoAddToGui) ButtonDefinition.AutoAddToGUI();
        }

        /// <summary>Removes command button</summary>
        public void RemoveButton()
        {
            try
            {
                ButtonDefinition.OnExecute -= ButtonDefinition_OnExecute;
                ButtonDefinition.Delete();
            }
            finally
            {
                ButtonDefinition = null;
            }
        }

        /// <summary>Button press handler</summary>
        /// <param name="context"></param>
        protected virtual void ButtonDefinition_OnExecute(NameValueMap context)
        {
            ButtonDefinition.Pressed = true;
            try
            {
                ExecuteCommand(context);
            }
            catch (Exception ex)
            {
                ThisApplication.PromptBox(Resources.Msg_CommandFailedGeneral, Resources.AddIn_DisplayName,
                    promptStrings: new[] { ButtonDefinition.DisplayName, ex.Message });
            }
            finally
            {
                ButtonDefinition.Pressed = false;
            }
        }

        /// <summary>Method called when button pressed</summary>
        /// <param name="context"></param>
        protected abstract void ExecuteCommand(NameValueMap context);
    }
}