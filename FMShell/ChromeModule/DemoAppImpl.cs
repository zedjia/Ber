using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xilium.CefGlue;
using Xilium.CefGlue.Demo;
using MenuItem = Xilium.CefGlue.Demo.MenuItem;

namespace FMShell
{
    internal sealed class DemoAppImpl : DemoApp
    {
        protected override void PlatformInitialize()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        protected override void PlatformShutdown()
        {
        }

        protected override void PlatformRunMessageLoop()
        {
            if (!MultiThreadedMessageLoop)
            {
                Application.Idle += (s, e) => CefRuntime.DoMessageLoopWork();
            }

            Application.Run();
        }

        protected override void PlatformQuitMessageLoop()
        {
            Application.Exit();
        }

        protected override IMainView CreateMainView(MenuItem[] menuItems)
        {
            return new MainView(this, menuItems);//this, null
        }

        protected override void PlatformMessageBox(string message)
        {
            MessageBox.Show(message, "Shell Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
