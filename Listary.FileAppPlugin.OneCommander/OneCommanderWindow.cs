using System;
using System.Threading.Tasks;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

namespace Listary.FileAppPlugin.OneCommander
{
    public class OneCommanderWindow : IFileWindow, IDisposable
    {
        private readonly IFileAppPluginHost _host;
        private readonly UIA3Automation _automation;
        private readonly AutomationElement _oneCommander;

        public IntPtr Handle { get; }

        public OneCommanderWindow(IFileAppPluginHost host, IntPtr hWnd, UIA3Automation automation, AutomationElement oneCommander)
        {
            _host = host;
            Handle = hWnd;
            _automation = automation;
            _oneCommander = oneCommander;
        }

        public void Dispose()
        {
            _automation.Dispose();
        }

        public async Task<IFileTab> GetCurrentTab()
        {
            return new OneCommanderTab(_host, _oneCommander);
        }
    }
}
