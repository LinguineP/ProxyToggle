using Microsoft.Win32;

namespace ProxyToggle
{
    public static class ProxyManager
    {
        private const string KeyPath = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";

        public static bool IsProxyEnabled()
        {
            using var key = Registry.CurrentUser.OpenSubKey(KeyPath);
            return key?.GetValue("ProxyEnable")?.ToString() == "1";
        }

        public static bool ToggleProxy()
        {
            bool enable = !IsProxyEnabled();
            using var key = Registry.CurrentUser.OpenSubKey(KeyPath, true);
            key?.SetValue("ProxyEnable", enable ? 1 : 0);
            return enable;
        }

        public static string GetProxyServer()
        {
            using var key = Registry.CurrentUser.OpenSubKey(KeyPath);
            return key?.GetValue("ProxyServer")?.ToString() ?? "";
        }

        public static void SetProxyServer(string server)
        {
            using var key = Registry.CurrentUser.OpenSubKey(KeyPath, true);
            key?.SetValue("ProxyServer", server);
        }
    }
}
