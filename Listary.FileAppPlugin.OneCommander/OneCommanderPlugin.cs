using System;
using System.IO;
using System.Threading.Tasks;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using Microsoft.Extensions.Logging;

namespace Listary.FileAppPlugin.OneCommander
{
    public class OneCommanderPlugin : IFileAppPlugin
    {
        private IFileAppPluginHost _host;

        public bool IsOpenedFolderProvider => true;
        
        public bool IsQuickSwitchTarget => false;
        
        public bool IsSharedAcrossApplications => false;

        public SearchBarType SearchBarType => SearchBarType.Floating;
        
        public async Task<bool> Initialize(IFileAppPluginHost host)
        {
            _host = host;
            return true;
        }

        public IFileWindow BindFileWindow(IntPtr hWnd)
        {
            // Is it from OneCommander?
            string processName = Path.GetFileName(Win32Utils.GetProcessPathFromHwnd(hWnd));
            if (processName == "OneCommander.exe")
            {
                // Is it OneCommander's file window?
                try
                {
                    var automation = new UIA3Automation();
                    AutomationElement oneCommander = automation.FromHandle(hWnd);
                    if (oneCommander.Name == "OneCommander")
                    {
                        return new OneCommanderWindow(_host, hWnd, automation, oneCommander);
                    }
                }
                catch (TimeoutException e)
                {
                    _host.Logger.LogWarning($"UIA timeout: {e}");
                }
                catch (Exception e)
                {
                    _host.Logger.LogError($"Failed to bind window: {e}");
                }
            }
            return null;
        }
    }
}
