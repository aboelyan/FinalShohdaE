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
    public partial class SUBMINU : Form
    {
        int ID = 0;
        SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=sh;Trusted_Connection=True;");
        public SUBMINU()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string qry = "insert into MX (learning ,incom ,sotialstate ,activitykind  ,aidkind,workk, sq )  Values " +
                    "( @learning , @incom , @sotialstate , @activitykind , @aidkind , @workk, @sq )";
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@learning", textBox4.Text);
                cmd.Parameters.AddWithValue("@incom", textBox5.Text);
                cmd.Parameters.AddWithValue("@sotialstate", textBox3.Text);
                cmd.Parameters.AddWithValue("@activitykind", textBox7.Text);
                cmd.Parameters.AddWithValue("@aidkind", textBox8.Text);
                cmd.Parameters.AddWithValue("@workk", textBox2.Text);
                cmd.Parameters.AddWithValue("@sq", textBox10.Text);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("تم الاضافة بنجاح");
                //GetData();
                //clear();

            }
            catch (Exception)
            {
                MessageBox.Show("حاول مجددا");
            }
        }
    }
}
