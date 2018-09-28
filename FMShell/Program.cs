using System;
using System.Windows.Forms;

namespace Ber
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //using (var application = new DemoAppImpl())
            //{
            //    //args = new string[args.Length + 1];
            //    //args[args.Length-1] = @"-ppapi-out-of-process –register-pepper-plugins=""F:\SVN projects\FireMonitor\Code\Shell\FMShell\FMShell\bin\Debug\Plugins\pepflashplayer.dll;application/x-shockwave-flash""";
            //    return application.Run(args);
            //}

            //CefRuntime.Load();
            //var mainArgs = new CefMainArgs(new string[] { });
            //var exitCode = CefRuntime.ExecuteProcess(mainArgs, null);
            //if (exitCode != -1)
            //    return;
            //var settings = new CefSettings
            //{
            //    SingleProcess = false,
            //    MultiThreadedMessageLoop = true,
            //    LogSeverity = CefLogSeverity.Disable,
            //    Locale = "zh-CN"
            //};
            //CefRuntime.Initialize(mainArgs, settings, null);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //if (!settings.MultiThreadedMessageLoop)
            //{
            //    Application.Idle += (sender, e) => { CefRuntime.DoMessageLoopWork(); };
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            //Application.Run(new CefBrowser());
            //CefRuntime.Shutdown();

        }
    }
}
