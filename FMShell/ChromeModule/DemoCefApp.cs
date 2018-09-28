using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Xilium.CefGlue;
using Xilium.CefGlue.Demo;

namespace FMShell
{

    internal sealed class DemoCefApp : CefApp
    {
        private IMainView mainView;
        public DemoCefApp(IMainView view)
        {
            mainView = view;
            _renderProcessHandler = new DemoRenderProcessHandler(mainView);
        }

        private CefBrowserProcessHandler _browserProcessHandler = new DemoBrowserProcessHandler();
        private CefRenderProcessHandler _renderProcessHandler ;

        protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
        {
            Console.WriteLine("OnBeforeCommandLineProcessing: {0} {1}", processType, commandLine);

            // TODO: currently on linux platform location of locales and pack files are determined
            // incorrectly (relative to main module instead of libcef.so module).
            // Once issue http://code.google.com/p/chromiumembedded/issues/detail?id=668 will be resolved
            // this code can be removed.
            if (CefRuntime.Platform == CefRuntimePlatform.Linux)
            {
                var path = new Uri(Assembly.GetEntryAssembly().CodeBase).LocalPath;
                path = Path.GetDirectoryName(path);

                commandLine.AppendSwitch("resources-dir-path", path);
                commandLine.AppendSwitch("locales-dir-path", Path.Combine(path, "locales"));
                //commandLine.AppendSwitch("ppapi-flash-version", "21.0.0.197");
                //commandLine.AppendSwitch("ppapi-flash-path", "plugins/pepflashplayer.dll");
            }
            else
            {
                //commandLine.AppendSwitch("ppapi-flash-version", "21.0.0.197");
                commandLine.AppendSwitch("ppapi-flash-path", "plugins/pepflashplayer.dll");
            }
        }

        protected override CefBrowserProcessHandler GetBrowserProcessHandler()
        {
            return _browserProcessHandler;
        }

        protected override CefRenderProcessHandler GetRenderProcessHandler()
        {
            return _renderProcessHandler;
        }
    }
}
