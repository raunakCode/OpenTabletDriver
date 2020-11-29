using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Forms;
using OpenTabletDriver.Desktop;
using OpenTabletDriver.Plugin.Output;
using OpenTabletDriver.Reflection;

namespace OpenTabletDriver.UX.Controls
{
    public class OutputModeSelector : ComboBox
    {
        public OutputModeSelector()
        {
            var outputModes = from type in AppInfo.PluginManager.GetChildTypes<IOutputMode>()
                where !type.IsAbstract
                where !type.IsInterface
                select AppInfo.PluginManager.GetPluginReference(type);

            OutputModes = new List<PluginReference>(outputModes);
            this.SelectedIndexChanged += (sender, e) =>
            {
                if (this.SelectedIndex >= 0)
                    SelectedMode = OutputModes[this.SelectedIndex];
            };
        }

        public event EventHandler<PluginReference> SelectedModeChanged;

        private List<PluginReference> _modes;
        public List<PluginReference> OutputModes
        {
            set
            {
                _modes = value;
                Items.Clear();
                foreach (var plugin in OutputModes)
                    Items.Add(plugin.ToString());
            }
            get => _modes;
        }

        private PluginReference _selectedMode;
        public PluginReference SelectedMode
        {
            private set
            {
                _selectedMode = value;
                SelectedModeChanged?.Invoke(this, SelectedMode);
            }
            get => _selectedMode;
        }
    }
}
