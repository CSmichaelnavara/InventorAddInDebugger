using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Inventor;
using MiNa.InventorAddInDebugger.Commands;
using MiNa.InventorAddInDebugger.Common;
using MiNa.InventorAddInDebugger.Properties;
using Command = MiNa.InventorAddInDebugger.Commands.Command;

namespace MiNa.InventorAddInDebugger
{
    /// <summary>
    ///     This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    ///     that all Inventor AddIns are required to implement. The communication between Inventor and
    ///     the AddIn is via the methods on this interface.
    /// </summary>
    [Guid("cb79d7e6-3ca7-439b-8c71-ff78c2922042")]
    public class StandardAddInServer : ApplicationAddInServer
    {
        /// <summary>
        /// Gets the ClientId of the AddIn
        /// </summary>
        public const string ClientId = "{cb79d7e6-3ca7-439b-8c71-ff78c2922042}";

        private AboutCmd _aboutCmd;

        private ActivateAddInCmd _activateAddInCmd;

        private AddInLoader _addInLoader;
        private AddInLoaderConfigManager _configMgr;

        // Inventor application object.
        private Application _inventor;
        private AttachDebuggerCmd _attachDebuggerCmd;
        private DeactivateAddInCmd _deactivateAddInCmd;
        private List<Command> _commands = new List<Command>();

        private List<RibbonPanel> _ribbonPanels = new List<RibbonPanel>();
        private OpenBuildFolderCmd _openBuildFolderCmd;
        private ReloadAddInCmd _reloadAddInCmd;
        private SettingsCmd _settingsCmd;

        internal static AddInLoaderConfig Config { get; private set; }

        private void AddButtonsToPanel(RibbonPanel panel)
        {
            panel.CommandControls.AddButton(_activateAddInCmd.ButtonDefinition, true);
            panel.CommandControls.AddButton(_deactivateAddInCmd.ButtonDefinition, true);
            panel.CommandControls.AddButton(_reloadAddInCmd.ButtonDefinition, true);
            panel.CommandControls.AddButton(_openBuildFolderCmd.ButtonDefinition, false);
            panel.CommandControls.AddButton(_attachDebuggerCmd.ButtonDefinition, false);
            panel.CommandControls.AddButton(_settingsCmd.ButtonDefinition, false);
            panel.SlideoutControls.AddButton(_aboutCmd.ButtonDefinition, false);
        }

        private void AddButtonsToRibbon()
        {
            const string intNamePrefix = "CsMichaelNavara.InventorAddInDebugger";

            var zeroDocRibbonTab = _inventor.UserInterfaceManager.Ribbons["ZeroDoc"].RibbonTabs["id_TabTools"];
            var partRibbonTab = _inventor.UserInterfaceManager.Ribbons["Part"].RibbonTabs["id_AddInsTab"];
            var assemblyRibbonTab = _inventor.UserInterfaceManager.Ribbons["Assembly"].RibbonTabs["id_AddInsTab"];
            var drawingRibbonTab = _inventor.UserInterfaceManager.Ribbons["Drawing"].RibbonTabs["id_AddInsTab"];

            var zeroDocRibbonPanel = zeroDocRibbonTab.RibbonPanels.Add(
                Resources.AddIn_DisplayName,
                $"{intNamePrefix}.ZeroDocPanel",
                ClientId);

            var partRibbonPanel = partRibbonTab.RibbonPanels.Add(
                Resources.AddIn_DisplayName,
                $"{intNamePrefix}.PartPanel",
                ClientId);

            var assemblyRibbonPanel = assemblyRibbonTab.RibbonPanels.Add(
                Resources.AddIn_DisplayName,
                $"{intNamePrefix}.AssemblyPanel",
                ClientId);

            var drawingRibbonPanel = drawingRibbonTab.RibbonPanels.Add(
                Resources.AddIn_DisplayName,
                $"{intNamePrefix}.DrawingPanel",
                ClientId);

            _ribbonPanels = new List<RibbonPanel>
            {
                zeroDocRibbonPanel,
                partRibbonPanel,
                assemblyRibbonPanel,
                drawingRibbonPanel
            };

            AddButtonsToPanel(zeroDocRibbonPanel);
            AddButtonsToPanel(partRibbonPanel);
            AddButtonsToPanel(assemblyRibbonPanel);
            AddButtonsToPanel(drawingRibbonPanel);
        }

        private void CreateCommands()
        {
            bool autoAddToGui = false;
            _commands = new List<Command>();

            _activateAddInCmd = new ActivateAddInCmd(_inventor, _addInLoader);
            _activateAddInCmd.CreateButton(
                Resources.ActivateAddInCmd_DisplayName,
                Resources.ActivateAddInCmd_Description,
                Resources.ActivateAddInCmd_ToolTip,
                CommandTypesEnum.kQueryOnlyCmdType,
                ClientId,
                new CommandIcon(_inventor.IconByTheme(
                    Resources.ActivateCmd_Light,
                    Resources.ActivateCmd_Dark)),
                autoAddToGui
            );
            _commands.Add(_activateAddInCmd);

            _deactivateAddInCmd = new DeactivateAddInCmd(_inventor, _addInLoader);
            _deactivateAddInCmd.CreateButton(
                Resources.DeactivateAddInCmd_DisplayName,
                Resources.DeactivateAddInCmd_Description,
                Resources.DeactivateAddInCmd_ToolTip,
                CommandTypesEnum.kQueryOnlyCmdType,
                ClientId,
                new CommandIcon(_inventor.IconByTheme(
                    Resources.DeactivateCmd_Light,
                    Resources.DeactivateCmd_Dark)),
                autoAddToGui
            );
            _commands.Add(_deactivateAddInCmd);

            _reloadAddInCmd = new ReloadAddInCmd(_inventor, _addInLoader);
            _reloadAddInCmd.CreateButton(
                Resources.ReloadAddInCmd_DisplayName,
                Resources.ReloadAddInCmd_Description,
                Resources.ReloadAddInCmd_ToolTip,
                CommandTypesEnum.kQueryOnlyCmdType,
                ClientId,
                new CommandIcon(_inventor.IconByTheme(
                    Resources.RestartCmd_Light,
                    Resources.RestartCmd_Dark)),
                autoAddToGui
            );
            _commands.Add(_reloadAddInCmd);

            _openBuildFolderCmd = new OpenBuildFolderCmd(_inventor, _addInLoader);
            _openBuildFolderCmd.CreateButton(
                Resources.OpenBuildFolderCmd_DisplayName,
                Resources.OpenBuildFolderCmd_Description,
                Resources.OpenBuildFolderCmd_ToolTip,
                CommandTypesEnum.kQueryOnlyCmdType,
                ClientId,
                new CommandIcon(_inventor.IconByTheme(
                    Resources.OpenBuildFolderCmd_Light,
                    Resources.OpenBuildFolderCmd_Dark)),
                autoAddToGui
            );
            _commands.Add(_openBuildFolderCmd);

            _attachDebuggerCmd = new AttachDebuggerCmd(_inventor);
            _attachDebuggerCmd.CreateButton(
                Resources.AttachDebuggerCmd_DisplayName,
                Resources.AttachDebuggerCmd_Description,
                Resources.AttachDebuggerCmd_ToolTip,
                CommandTypesEnum.kQueryOnlyCmdType,
                ClientId,
                new CommandIcon(_inventor.IconByTheme(
                    Resources.AttachDebuggerCmd_Light,
                    Resources.AttachDebuggerCmd_Dark)),
                autoAddToGui
            );
            _commands.Add(_attachDebuggerCmd);

            _settingsCmd = new SettingsCmd(_inventor, _configMgr);
            _settingsCmd.CreateButton(
                Resources.SettingsCmd_DisplayName,
                Resources.SettingsCmd_Description,
                Resources.SettingsCmd_ToolTip,
                CommandTypesEnum.kQueryOnlyCmdType,
                ClientId,
                new CommandIcon(_inventor.IconByTheme(
                    Resources.SettingsCmd_Light,
                    Resources.SettingsCmd_Dark)),
                autoAddToGui
            );
            _commands.Add(_settingsCmd);

            _aboutCmd = new AboutCmd(_inventor);
            _aboutCmd.CreateButton(
                Resources.AboutCmd_DisplayName,
                Resources.AboutCmd_Description,
                Resources.AboutCmd_ToolTip,
                CommandTypesEnum.kQueryOnlyCmdType,
                ClientId,
                new CommandIcon(_inventor.IconByTheme(
                    Resources.AboutCmd_Ligh,
                    Resources.AboutCmd_Dark)),
                autoAddToGui
            );
            _commands.Add(_aboutCmd);
        }


        #region ApplicationAddInServer Members

        public void Activate(ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // Initialize AddIn members.
            _inventor = addInSiteObject.Application;

            // TODO: Add ApplicationAddInServer.Activate implementation.
            // e.g. event initialization, command creation etc.
            Config = new AddInLoaderConfig();
            _configMgr = new AddInLoaderConfigManager();
            _configMgr.Load(Config);

            _addInLoader = new AddInLoader(addInSiteObject);


            CreateCommands();
            AddButtonsToRibbon();

            //Loads the add-in when requested
            if (Config.LoadOnStart)
                _addInLoader.Activate();
        }

        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Remove commands
            foreach (var command in _commands)
            {
                try
                {
                    command.RemoveButton();
                }
                catch
                {
                    // Ignore all exceptions
                }
            }

            // Remove ribbon panels
            foreach (var ribbonPanel in _ribbonPanels)
            {
                try
                {
                    ribbonPanel.Delete();
                }
                catch
                {
                    // Ignore all exceptions
                }
            }


            // Release objects.
            _inventor = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandId)
        {
            // Note:this method is now obsolete, you should use the 
            // ControlDefinition functionality for implementing commands.
        }

        public object Automation =>
            // This property is provided to allow the AddIn to expose an API 
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning 
            // that class object through this property.
            // TODO: Add ApplicationAddInServer.Automation getter implementation
            null;

        #endregion
    }
}