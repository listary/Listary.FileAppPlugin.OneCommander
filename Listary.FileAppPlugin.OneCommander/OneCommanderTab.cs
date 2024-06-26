﻿using FlaUI.Core.AutomationElements;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Listary.FileAppPlugin.OneCommander
{
    public class OneCommanderTab : IFileTab, IGetFolder, IOpenFolder
    {
        private readonly IFileAppPluginHost _host;
        private readonly TextBox _currentPathGet;
        private readonly TextBox _currentPathSet;

        public OneCommanderTab(IFileAppPluginHost host, AutomationElement oneCommander)
        {
            _host = host;

            _currentPathGet = oneCommander.FindFirstChild(cf => cf.ByAutomationId("CurrentPathGet"))?.AsTextBox();
            if (_currentPathGet == null)
            {
                _host.Logger.LogError("Failed to find CurrentPathGet");
                return;
            }

            _currentPathSet = oneCommander.FindFirstChild(cf => cf.ByAutomationId("CurrentPathSet"))?.AsTextBox();
            if (_currentPathSet == null)
            {
                _host.Logger.LogError("Failed to find CurrentPathSet");
                return;
            }
        }

        public async Task<string> GetCurrentFolder()
        {
            try
            {
                return _currentPathGet.Text;
            }
            catch (Exception e)
            {
                _host.Logger.LogError($"Failed to get current folder: {e}");
                return null;
            }
        }

        public async Task<bool> OpenFolder(string path)
        {
            try
            {
                _currentPathSet.Text = path;
                return true;
            }
            catch (Exception e)
            {
                _host.Logger.LogError($"Failed to set current folder: {e}");
                return false;
            }
        }
    }
}
