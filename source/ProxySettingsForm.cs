using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ProxyToggle
{
    public class ProxySettingsForm : Form
    {
        private TextBox proxyBox;
        private Button saveButton;

        public ProxySettingsForm()
        {
            Text = "Edit Proxy Settings";
            Width = 300;
            Height = 120;

            proxyBox = new TextBox
            {
                Text = ProxyManager.GetProxyServer(),
                Dock = DockStyle.Top
            };

            saveButton = new Button
            {
                Text = "Save",
                Dock = DockStyle.Bottom
            };
            saveButton.Click += (s, e) => {
                ProxyManager.SetProxyServer(proxyBox.Text);
                Close();
            };

            Controls.Add(proxyBox);
            Controls.Add(saveButton);
        }
    }
}
