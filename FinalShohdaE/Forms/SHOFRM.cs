using FinalShohdaE.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalShohdaE.Forms
{
    public partial class SHOFRM : Form
    {
        MAIN_FRM frm = new MAIN_FRM();
        SqlDataAdapter da;
        DataTable dcenter = new DataTable();
        DataTable dvillege = new DataTable();
        int ID = 0;
        SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=sh;Trusted_Connection=True;");
        public SHOFRM()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtsuns_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
                string.Format("center LIKE '%{0}%' OR shohds_name LIKE '%{0}%' OR report_num LIKE '%{0}%' OR shnum LIKE '%{0}%' OR villege LIKE '%{0}%' OR sex LIKE '%{0}%' ", textBox1.Text);
            conn.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            clear();
            txtname.Focus();
            btnsave.Enabled = true;
        }
        void clear()
        {
            txtname.Text = "";
            txtreport.Text = "";
            txtsuns.Text = "";
            txtkode.Text = "";
            txtwife.Text = "";
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string qry = "insert into SH_TBL (shohds_name,shnum,incident_date,report_num,workk,suns_num,wife_name,center,incident,villege,iddimage,sex,re)  Values " +
                    "(@shohds_name,@shnum,@incident_date,@report_num,@workk,@suns_num,@wife_name,@center,@incident,@villege,@iddimage,@sex,@re)";
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@shohds_name", txtname.Text);
                cmd.Parameters.AddWithValue("@shnum", txtkode.Text);
                cmd.Parameters.AddWithValue("@incident_date", dtp1.Value);
                cmd.Parameters.AddWithValue("@report_num", txtreport.Text);
                cmd.Parameters.AddWithValue("@workk", cmpwork.Text);
                cmd.Parameters.AddWithValue("@suns_num", txtsuns.Text);
                cmd.Parameters.AddWithValue("@wife_name", txtwife.Text);
                cmd.Parameters.AddWithValue("@center", cmbcenter.Text);
                cmd.Parameters.AddWithValue("@incident", rtb1.Text);
                cmd.Parameters.AddWithValue("@villege", comboBox1.Text);
                cmd.Parameters.AddWithValue("@iddimage", savephoto1());
                cmd.Parameters.AddWithValue("@sex", comboBox2.Text);
                //cmd.Parameters.AddWithValue("@re", frm.textBox1.Text);
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
        public byte[] savephoto1()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }
        public void GetData()
        {
            string qry = "select  * from SH_TBL";
            SqlCommand cmd = new SqlCommand(qry, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "dt");
            conn.Close();
            this.dataGridView1.DataSource = ds.Tables[0];
        }
        private Image Getphoto(byte[] value)
        {
            MemoryStream ms = new MemoryStream(value);
            return Image.FromStream(ms);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();

            ofd1.Title = "select ";
            ofd1.Filter = "Image File (*.png;*jpeg;*jog;*bmp)|*.png;*jpeg;*jog;*bmp";
            ofd1.ShowDialog();
            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd1.FileName);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string qry = "Update  SH_TBL SET shohds_name = @shohds_name,shnum = @shnum,incident_date = @incident_date,report_num = @report_num,workk = @workk,suns_num = @suns_num" +
                    ",wife_name = @wife_name,center = @center,incident = @incident,villege = @villege ,iddimage=@iddimage,sex=@sex  where shid ='" + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@shohds_name", txtname.Text);
                cmd.Parameters.AddWithValue("@shnum", txtkode.Text);
                cmd.Parameters.AddWithValue("@incident_date", dtp1.Value);
                cmd.Parameters.AddWithValue("@report_num", txtreport.Text);
                cmd.Parameters.AddWithValue("@workk", cmpwork.Text);
                cmd.Parameters.AddWithValue("@suns_num", txtsuns.Text);
                cmd.Parameters.AddWithValue("@wife_name", txtwife.Text);
                cmd.Parameters.AddWithValue("@center", cmbcenter.Text);
                cmd.Parameters.AddWithValue("@incident", rtb1.Text);
                cmd.Parameters.AddWithValue("@villege", comboBox1.Text);
                cmd.Parameters.AddWithValue("@iddimage", savephoto1());
                cmd.Parameters.AddWithValue("@sex", comboBox2.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("تم التعديل بنجاح");
                GetData();
                clear();
            }
            catch (Exception)
            {
                MessageBox.Show("try agane");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("delete  from  SH_TBL where shid='" + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", conn);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtname.Text = row.Cells[1].Value.ToString();
                txtkode.Text = row.Cells[2].Value.ToString();
                dtp1.Text = row.Cells[3].Value.ToString();
                txtreport.Text = row.Cells[4].Value.ToString();
                cmpwork.Text = row.Cells[5].Value.ToString();
                txtsuns.Text = row.Cells[6].Value.ToString();
                txtwife.Text = row.Cells[7].Value.ToString();
                cmbcenter.Text = row.Cells[8].Value.ToString();
                rtb1.Text = row.Cells[9].Value.ToString();
                comboBox1.Text = row.Cells[10].Value.ToString();
                pictureBox1.Image = (row.Cells[11].Value is DBNull) ? Resources.download : Getphoto((byte[])row.Cells[11].Value);
                comboBox2.Text = row.Cells[12].Value.ToString();
                btnsave.Enabled = false;

            }
        }
    }
}
