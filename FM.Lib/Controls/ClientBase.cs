using System.Windows.Forms;

namespace WC.Lib.Controls
{
    public abstract class ClientBase:Form
    {

        public abstract void CallJsFunction(string jsFunction);

    }
}
