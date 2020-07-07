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
    public partial class AIDSFRM : Form
    {
        MAIN_FRM frm = new MAIN_FRM();
        int ID = 0;
        SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=sh;Trusted_Connection=True;");
        public AIDSFRM()
        {
            InitializeComponent();
            DataTable daid = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from BENF_TBL", conn);
            da.Fill(daid);
            comboBox1.DataSource = daid;
            comboBox1.ValueMember = "Benfid";
            comboBox1.DisplayMember = "benfname";

            SqlCommand cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM MX";
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox2.Items.Add(dr["aidkind"]);


            }
            conn.Close();
        }
        void clear()
        {

        }
        public void GetData()
        {


            string qry = "SELECT *FROM [dbo].[AID_TBL] ";
            SqlCommand cmd = new SqlCommand(qry, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "dt");
            conn.Close();

            this.dataGridView1.DataSource = ds.Tables[0];


        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string qry = "insert into AID_TBL (aidkind, qty ,uaid ,nots ,benn,datedes)  Values ( @aidkind ,@qty  ,@uaid ,@nots ,@benn,@datedes )";
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@aidkind", comboBox2.Text);
                cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(txtqty.Text));
                cmd.Parameters.AddWithValue("@uaid", txtaidq.Text);
                cmd.Parameters.AddWithValue("@nots", txtnots.Text);
                cmd.Parameters.AddWithValue("@benn", Convert.ToInt32(comboBox1.SelectedValue));
                cmd.Parameters.AddWithValue("@datedes", dateTimePicker1.Value);


                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("تم الاضافة بنجاح");
                GetData();
                clear();

            }
            catch (Exception)
            {

                MessageBox.Show("حاول مجددا");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string qry = "update   AID_TBL  SET  aidkind = @aidkind,qty= @qty ,uaid= @uaid ,nots= @nots,benn = @benn , datedes = @datedes   where  aidid='" + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@aidkind", comboBox2.Text);
                cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(txtqty.Text));
                cmd.Parameters.AddWithValue("@uaid", txtaidq.Text);
                cmd.Parameters.AddWithValue("@nots", txtnots.Text);
                cmd.Parameters.AddWithValue("@benn", Convert.ToInt32(comboBox1.SelectedValue));
                cmd.Parameters.AddWithValue("@datedes", dateTimePicker1.Value);
                cmd.ExecuteNonQuery();

                MessageBox.Show("تم التحديث بنجاح");

                GetData();
                conn.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("حاول مجددا");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("delete  from  AID_TBL where aidid='" + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", conn);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
                string.Format(" uaid LIKE '%{0}%'OR  aidkind LIKE '%{0}%' ", textBox1.Text);
            conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                comboBox2.Text = row.Cells[1].Value.ToString();
                txtqty.Text = row.Cells[2].Value.ToString();
                comboBox1.Text = row.Cells[3].Value.ToString();
                txtnots.Text = row.Cells[4].Value.ToString();
                comboBox1.Text = row.Cells[5].Value.ToString();
                dateTimePicker1.Text = row.Cells[6].Value.ToString();
                button4.Enabled = false;
            }
        }
    }
}
