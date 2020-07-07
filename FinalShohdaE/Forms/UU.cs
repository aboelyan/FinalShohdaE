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
    public partial class UU : Form
    {
        int ID = 0;
        SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=sh;Trusted_Connection=True;");

        public UU()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                conn.Open();
                string qry = "insert into Users (username, password ,namee ,email ,usertype)  Values " +
                    "( @username ,@password  ,@namee ,@email ,@usertype )";
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@username", txtname.Text);
                cmd.Parameters.AddWithValue("@password", txtkode.Text);
                cmd.Parameters.AddWithValue("@namee", txtreport.Text);
                cmd.Parameters.AddWithValue("@email", textBox1.Text);
                cmd.Parameters.AddWithValue("@usertype", txtwife.Text);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("تم الاضافة بنجاح");
                GetData();


            }
            catch (Exception)
            {

                MessageBox.Show("حاول مجددا");
            }
        }
        public void GetData()
        {


            string qry = "select * from  Users";
            SqlCommand cmd = new SqlCommand(qry, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "dt");
            conn.Close();

            this.dataGridView1.DataSource = ds.Tables[0];


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("delete  from  Users where id='" + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("تم الحذف بنجاح!");
                GetData();
            }
            catch (Exception)
            {

                MessageBox.Show(":) :) :) :)");
            }

        }
    }
}
