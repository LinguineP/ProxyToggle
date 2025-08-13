using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProxyToggle
{
    public class MainForm : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private Icon iconOn;
        private Icon iconOff;

        public MainForm()
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Visible = false;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            // DEBUG: List all embedded resources
            var resources = assembly.GetManifestResourceNames();
            foreach (var name in resources)
            {
                System.Diagnostics.Debug.WriteLine("Resource: " + name);
            }

            // Find resource names for icons
            string iconOnResource = null;
            string iconOffResource = null;
            foreach (var name in resources)
            {
                if (name.ToLower().EndsWith("proxy_on.ico")) iconOnResource = name;
                if (name.ToLower().EndsWith("proxy_off.ico")) iconOffResource = name;
            }

            try
            {
                if (iconOnResource != null)
                {
                    using (var stream = assembly.GetManifestResourceStream(iconOnResource))
                    {
                        iconOn = stream != null ? new Icon(stream) : SystemIcons.Application;
                    }
                }
                else
                {
                    iconOn = SystemIcons.Application;
                }
            }
            catch { iconOn = SystemIcons.Application; }

            try
            {
                if (iconOffResource != null)
                {
                    using (var stream = assembly.GetManifestResourceStream(iconOffResource))
                    {
                        iconOff = stream != null ? new Icon(stream) : SystemIcons.Shield;
                    }
                }
                else
                {
                    iconOff = SystemIcons.Shield;
                }
            }
            catch { iconOff = SystemIcons.Shield; }

            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Toggle Proxy", null, ToggleProxy);
            trayMenu.Items.Add("Edit Proxy Settings", null, EditProxySettings);
            trayMenu.Items.Add("Exit", null, (s, e) => Application.Exit());

            trayIcon = new NotifyIcon
            {
                Icon = ProxyManager.IsProxyEnabled() ? iconOn : iconOff,
                ContextMenuStrip = trayMenu,
                Visible = true,
                Text = "Proxy Toggle"
            };
            Toast.Init(trayIcon);

            trayIcon.DoubleClick += (s, e) => ToggleProxy(s, e);
            trayIcon.MouseUp += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                    trayMenu.Show(Cursor.Position);
            };
        }

        private void ToggleProxy(object sender, EventArgs e)
        {
            bool enabled = ProxyManager.ToggleProxy();
            trayIcon.Icon = enabled ? iconOn : iconOff;
        }

        private void EditProxySettings(object sender, EventArgs e)
        {
            var form = new ProxySettingsForm();
            form.ShowDialog();
            trayIcon.Icon = ProxyManager.IsProxyEnabled() ? iconOn : iconOff;
        }
    }
}
