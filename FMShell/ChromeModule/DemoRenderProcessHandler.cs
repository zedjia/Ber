﻿using System.Windows.Forms;
using Xilium.CefGlue.Demo;
using Xilium.CefGlue.Demo.Renderer.CallJs;

namespace FMShell
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xilium.CefGlue;
    using Xilium.CefGlue.Wrapper;

    internal class DemoRenderProcessHandler : CefRenderProcessHandler
    {
        internal static bool DumpProcessMessages { get; private set; }
        public CefV8Handler Cef;
        private IMainView mainView;


        public DemoRenderProcessHandler(IMainView view)
        {
            MessageRouter = new CefMessageRouterRendererSide(new CefMessageRouterConfig());
            mainView = view;
        }

        internal CefMessageRouterRendererSide MessageRouter { get; private set; }

        protected override void OnContextCreated(CefBrowser browser, CefFrame frame, CefV8Context context)
        {
            MessageRouter.OnContextCreated(browser, frame, context);
        }

        protected override void OnContextReleased(CefBrowser browser, CefFrame frame, CefV8Context context)
        {
            MessageRouter.OnContextReleased(browser, frame, context);
        }

        protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
        {
            if (DumpProcessMessages)
            {
                Console.WriteLine("Render::OnProcessMessageReceived: SourceProcess={0}", sourceProcess);
                Console.WriteLine("Message Name={0} IsValid={1} IsReadOnly={2}", message.Name, message.IsValid, message.IsReadOnly);
                var arguments = message.Arguments;
                for (var i = 0; i < arguments.Count; i++)
                {
                    var type = arguments.GetValueType(i);
                    object value;
                    switch (type)
                    {
                        case CefValueType.Null: value = null; break;
                        case CefValueType.String: value = arguments.GetString(i); break;
                        case CefValueType.Int: value = arguments.GetInt(i); break;
                        case CefValueType.Double: value = arguments.GetDouble(i); break;
                        case CefValueType.Bool: value = arguments.GetBool(i); break;
                        default: value = null; break;
                    }

                    Console.WriteLine("  [{0}] ({1}) = {2}", i, type, value);
                }
            }

            var handled = MessageRouter.OnProcessMessageReceived(browser, sourceProcess, message);
            if (handled) return true;

            if (message.Name == "myMessage2") return true;

            var message2 = CefProcessMessage.Create("myMessage2");
            var success = browser.SendProcessMessage(CefProcessId.Renderer, message2);
            Console.WriteLine("Sending myMessage2 to renderer process = {0}", success);

            var message3 = CefProcessMessage.Create("myMessage3");
            var success2 = browser.SendProcessMessage(CefProcessId.Browser, message3);
            Console.WriteLine("Sending myMessage3 to browser process = {0}", success);

            return false;
        }


        /// <summary>
        /// 通过反射机制 注册c#函数到JS
        /// </summary>
        public void RegisterJs()
        {

            JsEvent js = new JsEvent(mainView);

            Cef = new CefJsV8Handler(js);

            string javascriptCode = CefJavaScriptEx.CreateJsCodeByObject(js, "Cef");

            CefRuntime.RegisterExtension("Cef", javascriptCode, Cef);
        }


        protected override void OnWebKitInitialized()
        {
            // 注册JS函数
            RegisterJs();
        }
    }
}
