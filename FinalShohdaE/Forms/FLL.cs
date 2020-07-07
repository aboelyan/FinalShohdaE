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
    public partial class FLL : Form
    {
        public SqlConnection conn = new SqlConnection(@"Data Source = .\SQLEXPRESS;Initial Catalog = sh; Integrated Security = True");

        public FLL()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string userid = textBox1.Text;
            string password = textBox2.Text;
            SqlCommand cmd = new SqlCommand("select username,password from Users where username='" + textBox1.Text + "'and password='" + textBox2.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("تسجيل دخول ناجح ");
               Forms.MAIN_FRM frm = new MAIN_FRM();
                
              //  frm.textBox1.Text = this.textBox1.Text;

                frm.ShowDialog();

                Close();

            }
            else
            {
                MessageBox.Show("كلمة  مرور خاطئة");
            }
            conn.Close();
        }
    }
}
