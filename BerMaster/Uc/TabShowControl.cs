using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BerMaster.DB;

namespace BerMaster.Uc
{
    public partial class TabShowControl : BaseUserControl
    {
        
        private TableConfig _table { get; set; }

        public TabShowControl(TableConfig table)
        {
            InitializeComponent();
            _table = table;
            lblAlise.Text = string.Format("{0}({1})", table.name, table.alias);
            dateTimePicker2.Value = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DapperDbContext context = new DapperDbContext(CurrentConfig.ConnectionString);
            var list = context.QueryBData(_table.alias,dateTimePicker1.Value,dateTimePicker2.Value);
            dataGridView1.DataSource = list;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime d;
            d = dateTimePicker1.Value;
            dateTimePicker1.Value = dateTimePicker2.Value;
            dateTimePicker2.Value = d;
        }

        public void SetLastUpdateTimeLabel(DateTime dt)
        {
            this.Invoke((EventHandler)delegate { this.lbllastUpdate.Text = dt.ToString("yyyy-MM-dd HH:mm:ss"); });
        }
    }
}
