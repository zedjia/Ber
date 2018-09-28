using System;
using System.Diagnostics;
using System.Windows.Forms;
using Xilium.CefGlue.Demo;

namespace FMShell
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class JsEvent
    {
        private MainView mainView;
        public JsEvent(IMainView view)
        {
            mainView = view as MainView;
        }
        public Object MyParam { get; set; }

        //public Object GetMyParam()
        //{
        //    if (MyParam.GetType().IsArray)
        //    {
        //        String s = "[";
        //        Object[] o = (Object[])MyParam;
        //        for (int i = 0; i < o.Length; i++)
        //        {
        //            s += "'" + o[i].ToString() + "'";
        //            if (i < (o.Length - 1))
        //            {
        //                s += ",";
        //            }
        //        }
        //        s += "]";
        //        return s;
        //    }
        //    return MyParam;
        //}

        public void openMyPc(String dir)
        {
            if(dir == null){// 打开我的电脑
                dir = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            }
            Process.Start("explorer.exe", dir);
        }

        public void ExitApp()
        {
            //mainView.
            //MessageBox.Show("exit");
            MainView.MainForm.ExitApplication();
            //System.Environment.Exit(0);
            //Application.Exit();
        }

        public void TestApp()
        {
            //mainView.
            MainView.MainForm.TestApp();
            //System.Environment.Exit(0);
            //Application.Exit();
        }
        
        //public void Login(string json)
        //{
        //    var dto = Util.Json.ToObject<UserDto>(json);
        //}

        //public void op



    }
}
