using System.Drawing;
using System.Windows.Forms;
using Inventor;

namespace MiNa.InventorAddInDebugger.Common
{
    internal static class Extensions
    {
        /// <summary>
        /// Selects the icon by theme for inventor
        /// </summary>
        /// <param name="inventor">Inventor application</param>
        /// <param name="lightIcon">The light icon.</param>
        /// <param name="darkIcon">The dark icon.</param>
        /// <returns>When darkIcon is provided, active Inventor API is 25 or above and active theme is "DarkTheme", returns darkIcon. Otherwise returns lightIcon</returns>
        public static Icon IconByTheme(this Inventor.Application inventor, Icon lightIcon, Icon darkIcon = null)
        {
            if (darkIcon == null)
                return lightIcon;

            //Before Inventor 2021 (25)
            if (inventor.SoftwareVersion.Major < 25)
                return lightIcon;

            try
            {
                string themeName = inventor.ThemeManager.ActiveTheme.Name;
                return themeName == "DarkTheme" ? darkIcon : lightIcon;
            }
            catch
            {
                return lightIcon;
            }
        }

        /// <summary>Display Inventor native messagebox</summary>
        /// <param name="inventor">Application instance</param>
        /// <param name="text">Message text. %s is replaced by the elements from <paramref name="promptStrings" /> </param>
        /// <param name="caption">Messagebox title</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon"> One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="restrictions">&gt;Combination of the PromptMessageRestrictionsEnum</param>
        /// <param name="promptStrings">Strings for replace %s in text</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult PromptBox(this Inventor.Application inventor,
            string text,
            string caption = "",
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1,
            PromptMessageRestrictionsEnum restrictions = PromptMessageRestrictionsEnum.kNoRestrictions,
            params string[] promptStrings)
        {
            var pmPrompt = text;
            var pmButtons = (int)(buttons + (int)icon + (int)defaultButton);
            var pmTitle = string.IsNullOrEmpty(caption) ? null : (object)caption;
            object pmDefaultAnswer = null;
            var pmRestrictions = restrictions;
            var pmPromptStrings = promptStrings;
            return (DialogResult)inventor.CommandManager.PromptMessage(pmPrompt, pmButtons, pmTitle, pmDefaultAnswer,
                pmRestrictions, pmPromptStrings);
        }

        public static void BubbleTip(this Inventor.Application inventor, string message)
        {
            string balloonTipId = "{53BA95E9-32CF-4EDF-9D17-D264CFFEF70A}";
            BalloonTip balloonTip;

            try
            {
                balloonTip = inventor.UserInterfaceManager.BalloonTips[balloonTipId];
            }
            catch
            {
                balloonTip = inventor.UserInterfaceManager.BalloonTips.Add(balloonTipId, "AddIn loader", "");
            }

            balloonTip.Display(message);
        }

    }
}