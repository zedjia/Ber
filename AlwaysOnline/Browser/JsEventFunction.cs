using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace AlwaysOnline.Browser
{
    public class JsEventFunction
    {
        public MainForm MainForm;
        public JsEventFunction(MainForm form)
        {
            MainForm = form;
        }

        public void CloseForm()
        {
            Application.ExitThread();
            Application.Exit();
            Process.GetCurrentProcess().Kill();
            System.Environment.Exit(0);
        }

        /// <summary>
        /// 是否支持jQuery
        /// </summary>
        /// <param name="isSupportjQuery"></param>
        public void callbackjQueryChecker(bool isSupportjQuery)
        {
            MainForm.OnCheckjQueryCallBack(isSupportjQuery);
        }

        /// <summary>
        /// 把前端返回的用","分割的字符串，改为字符串列表
        /// </summary>
        /// <param name="links"></param>
        /// <returns></returns>
        public void callbackGetAllLinks(string links)
        {
            MainForm.OnCaptureLinksCallBack(links.Split(',').Distinct().ToList());
        }


    }
}
