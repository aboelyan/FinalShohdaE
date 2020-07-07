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
    public partial class Activity_FRM : Form
    {
        MAIN_FRM frm = new MAIN_FRM();


        int ID = 0;
        SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=sh;Trusted_Connection=True;");
        public Activity_FRM()
        {
            InitializeComponent();
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM MX";
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox1.Items.Add(dr["activitykind"]);


            }
            conn.Close();
        }
        void clear()
        {
            textBox7.Text = "";

            textBox3.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            textBox4.Text = "";

            textBox5.Text = "";
            textBox6.Text = "";
        }
        private void button9_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            conn.Open();
            String sql = "SELECT act_name,act_kined,sdate,edate,adress,maile,fmail,mange,aid,notes FROM  Activites  WHERE edate BETWEEN @sdate and @edate ";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.SelectCommand.Parameters.AddWithValue("@sdate", dateTimePicker4.Value);
            da.SelectCommand.Parameters.AddWithValue("@edate", dateTimePicker3.Value);
            da.Fill(dt);
            conn.Close();
            dataGridView2.DataSource = dt;

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                (dataGridView2.DataSource as DataTable).DefaultView.RowFilter =
                    string.Format("act_name LIKE '%{0}%' OR act_kined LIKE '%{0}%' OR aid LIKE '%{0}%' OR mange LIKE '%{0}%' ", textBox9.Text);
                conn.Close();
            }
            catch (Exception)
            {

                return;
            }
        }
        public void GetData()
        {


            string qry = "select * from  Activites";
            SqlCommand cmd = new SqlCommand(qry, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "dt");
            conn.Close();

            this.dataGridView2.DataSource = ds.Tables[0];


        }
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                conn.Open();
            string qry = "insert into Activites (act_name,act_kined,sdate,edate,adress,maile,fmail,mange,aid,notes,re)  Values " +
                "(@act_name,@act_kined,@sdate,@edate,@adress,@maile,@fmail,@mange,@aid,@notes)";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@act_name", textBox1.Text);
            cmd.Parameters.AddWithValue("@act_kined", comboBox1.Text);
            cmd.Parameters.AddWithValue("@sdate", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@edate", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@adress", textBox2.Text);
            cmd.Parameters.AddWithValue("@maile", Convert.ToInt32(textBox3.Text));
            cmd.Parameters.AddWithValue("@fmail", Convert.ToInt32(textBox4.Text));
            cmd.Parameters.AddWithValue("@mange", textBox5.Text);
            cmd.Parameters.AddWithValue("@aid", textBox6.Text);
            cmd.Parameters.AddWithValue("@notes", textBox7.Text);
       
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                textBox1.Text = row.Cells[1].Value.ToString();
                comboBox1.Text = row.Cells[2].Value.ToString();
                dateTimePicker1.Text = row.Cells[3].Value.ToString();
                dateTimePicker2.Text = row.Cells[4].Value.ToString();
                textBox2.Text = row.Cells[5].Value.ToString();
                textBox3.Text = row.Cells[6].Value.ToString();
                textBox4.Text = row.Cells[7].Value.ToString();
                textBox5.Text = row.Cells[8].Value.ToString();
                textBox6.Text = row.Cells[9].Value.ToString();
                textBox7.Text = row.Cells[10].Value.ToString();
            }
        }
    }
}
