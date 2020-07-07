using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalShohdaE.Forms
{
    public partial class FORM_RESTOR : Form
    {
        SqlConnection con = new SqlConnection(@"Server =.\SQLEXPRESS;Database = Master; Integrated Security = True");
        SqlCommand cmd;
        public FORM_RESTOR()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string stq = "ALTER Database sh SET OFFLINE WITH ROLLBACK IMMEDIATE Restore Database sh From Disk ='" + textBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(stq, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("تم استعادة النسخة بنجاح", "نسخة احتياطية", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
