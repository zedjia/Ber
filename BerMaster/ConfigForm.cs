using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BerMaster.DB;
using Z.Lib.Controls;

namespace BerMaster
{
    public partial class ConfigForm : ClientBaseForm
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            refreshList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TableConfig item = new TableConfig
            {
                name = textBox1.Text.Trim(),
                alias = textBox2.Text.Trim(),
                url = textBox3.Text.Trim(),
                Bid = textBox4.Text.Trim(),
                sourcesite= comboBox1.SelectedItem.ToString().Trim()
            };
            try
            {


                DapperDbContext context = new DapperDbContext(CurrentConfig.ConnectionString);
                if (!context.InsertTableConfigs(item))
                {
                    MessageBox.Show("新增保存失败.");
                    return;

                }
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
                return;
            }
            refreshList();
        }


        void refreshList()
        {
            DapperDbContext context = new DapperDbContext(CurrentConfig.ConnectionString);
            var list= context.QueryTableConfigs();
            dataGridView1.DataSource = list;
            
        }
    }
}
