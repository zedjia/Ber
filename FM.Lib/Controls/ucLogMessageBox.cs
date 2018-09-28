using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WC.Lib.Entity;

namespace WC.Lib.Controls
{
    public partial class ucLogMessageBox : RichTextBox
    {
        public ucLogMessageBox()
        {
            InitializeComponent();
            this.DetectUrls = false;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            //this.Location = new System.Drawing.Point(3, 3);
            //this.Name = "txtLogContainer";
            this.ReadOnly = true;
            this.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ShortcutsEnabled = false;
            //this.Size = new System.Drawing.Size(1056, 514);
            this.TabIndex = 0;
            this.Text = "通用组件.By Zed";
        }

        public void PrintShowLogMessage(string logmgs, MessageType type = MessageType.Info, Priority priority = Priority.Normal)
        {
            Color c;
            switch (type)
            {
                case MessageType.Info:
                    c = Color.Black;
                    break;
                case MessageType.Warnning:
                    c = Color.Chocolate;
                    break;
                case MessageType.Debug:
                    c = Color.DarkGray;
                    break;
                default:
                    c = Color.Red;
                    break;
            }
            this.InvokeOnUiThreadIfRequired(() =>
            {
                this.InsertTextColorful($"{DateTime.Now}:{logmgs}", c);
            });
        }

    }
}
