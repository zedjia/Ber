using System;
using System.Windows.Forms;

namespace WC.Lib.Controls
{
    public static class ControlsUtils
    {

        public static void SetControlText(this Label control, string text)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => control.Text = text));
            }
            else
            {
                control.Text = text;
            }
        }

        public static void SetControlItem(this ListBox control, string text)
        {
            
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => control.Items.Add(text)));
            }
            else
            {
                control.Items.Add(text);
            }
        }



    }

    public static class ControlExtensions
    {
        /// <summary>
        /// Executes the Action asynchronously on the UI thread, does not block execution on the calling thread.
        /// </summary>
        /// <param name="control">the control for which the update is required</param>
        /// <param name="action">action to be performed on the control</param>
        public static void InvokeOnUiThreadIfRequired(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }
    }
}
