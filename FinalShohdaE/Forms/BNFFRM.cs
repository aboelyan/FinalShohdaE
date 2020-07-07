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
    public partial class BNFFRM : Form
    {
        MAIN_FRM frm = new MAIN_FRM();
        SqlDataAdapter da;
        DataTable dcenter = new DataTable();
        DataTable dsh = new DataTable();
        DataTable din = new DataTable();
        int ID = 0;
        SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=sh;Trusted_Connection=True;");

        public BNFFRM()
        {
            InitializeComponent();
            da = new SqlDataAdapter("select * from Center", conn);
            da.Fill(dcenter);
            cmdcenter.DataSource = dcenter;
            cmdcenter.ValueMember = "id";
            cmdcenter.DisplayMember = "cname";

            SqlCommand cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM MX";
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                cmbincom.Items.Add(dr["incom"]);
                cmblearning.Items.Add(dr["learning"]);
                cmbsk.Items.Add(dr["sq"]);
            }
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            da = new SqlDataAdapter("select * from SH_TBL", conn);
            da.Fill(dsh);
            comboBox2.DataSource = dsh;
            comboBox2.ValueMember = "shid";
            comboBox2.DisplayMember = "shohds_name";
            comboBox3.Enabled = false;
            comboBox2.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("select * from Injurd_TBL", conn);
            da.Fill(din);
            comboBox3.DataSource = din;
            comboBox3.ValueMember = "injuredid";
            comboBox3.DisplayMember = "enjname";
            comboBox2.Enabled = false;
            comboBox3.Enabled = true;
            checkBox1.Checked = false;
        }
        public byte[] savephoto1()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
                string.Format("benfname LIKE '%{0}%' OR ssn LIKE '%{0}%'  OR sex LIKE '%{0}%'OR learning LIKE '%{0}%' OR relation LIKE '%{0}%' OR incom LIKE '%{0}%'OR noots LIKE '%{0}%'", textBox4.Text);
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            clear();
            txtname.Focus();
        }
        void clear()
        {
            txtname.Text = "";
            txtphone1.Text = "";
            txtidd.Text = "";
            txtadress.Text = "";
            txtjob.Text = "";
            txtnots.Text = "";
            txthome.Text = "";

        }
        public void GetData()
        {
            string qry = "select benfname,ssn,relation,learning,address,phone,homedesc,job,incom,noots,iddimage,ingid,sex,shohds_name,enjname inner join SH_TBL on shid=shid  inner join Injurd_TBL on ingid=injuredid from BENF_TBL";
            SqlCommand cmd = new SqlCommand(qry, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "dt");
            conn.Close();

            this.dataGridView1.DataSource = ds.Tables[0];


        }
        public Image Getphoto(byte[] value)
        {
            MemoryStream ms = new MemoryStream(value);
            return Image.FromStream(ms);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                try
                {
                    conn.Open();
                    string qry = "insert into BENF_TBL (benfname,ssn,relation,learning,address,phone,homedesc,job,incom,noots,iddimage,shid,sex,re)  Values " +
                    "(@benfname,@ssn,@relation,@learning,@address,@phone,@homedesc,@job,@incom,@noots,@iddimage,@shid,@sex ,@re)";
                    SqlCommand cmd = new SqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@benfname", txtname.Text);
                    cmd.Parameters.AddWithValue("@ssn", txtidd.Text);
                    cmd.Parameters.AddWithValue("@relation", cmbsk.Text);
                    cmd.Parameters.AddWithValue("@learning", cmblearning.Text);
                    cmd.Parameters.AddWithValue("@address", txtadress.Text);
                    cmd.Parameters.AddWithValue("@phone", txtphone1.Text);
                    cmd.Parameters.AddWithValue("@homedesc", txthome.Text);
                    cmd.Parameters.AddWithValue("@job", txtjob.Text);
                    cmd.Parameters.AddWithValue("@incom", cmbincom.Text);
                    cmd.Parameters.AddWithValue("@noots", txtnots.Text);
                    cmd.Parameters.AddWithValue("@iddimage", savephoto1());
                    cmd.Parameters.AddWithValue("@shid", comboBox2.SelectedValue);
                    cmd.Parameters.AddWithValue("@sex", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@re", lre.Text);
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
            else
            {
                try
                {
                    conn.Open();
                    string qry = "insert into BENF_TBL (benfname,ssn,relation,learning,address,phone,homedesc,job,incom,noots,iddimage,ingid,sex)  Values " +
                     "(@benfname,@ssn,@relation,@learning,@address,@phone,@homedesc,@job,@incom,@noots,@iddimage,@ingid,@sex)";
                    SqlCommand cmd = new SqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@benfname", txtname.Text);
                    cmd.Parameters.AddWithValue("@ssn", txtidd.Text);
                    cmd.Parameters.AddWithValue("@relation", cmbsk.Text);
                    cmd.Parameters.AddWithValue("@learning", cmblearning.Text);
                    cmd.Parameters.AddWithValue("@address", txtadress.Text);
                    cmd.Parameters.AddWithValue("@phone", txtphone1.Text);
                    cmd.Parameters.AddWithValue("@homedesc", txthome.Text);
                    cmd.Parameters.AddWithValue("@job", txtjob.Text);
                    cmd.Parameters.AddWithValue("@incom", cmbincom.Text);
                    cmd.Parameters.AddWithValue("@noots", txtnots.Text);
                    cmd.Parameters.AddWithValue("@iddimage", savephoto1());
                    cmd.Parameters.AddWithValue("@ingid", Convert.ToInt32(comboBox3.SelectedValue));
                    cmd.Parameters.AddWithValue("@sex", comboBox1.Text);

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                try
                {
                    conn.Open();
                    string qry = "update   BENF_TBL SET benfname = @benfname ,ssn = @ssn ,relation = @relation ,agestate = @agestate ,learning = @learning ,address = @address ,phone = @phone ,homedesc = @homedesc ," +
                        "job = @job ,incom = @incom ,noots = @noots ,iddimage = @iddimage ,shid=@shid,sex=@sex  where  Benfid='" + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";//inner join SH_TBL on shid=shid
                    SqlCommand cmd = new SqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@benfname", txtname.Text);
                    cmd.Parameters.AddWithValue("@ssn", txtidd.Text);
                    cmd.Parameters.AddWithValue("@relation", cmbsk.Text);
                    cmd.Parameters.AddWithValue("@learning", cmblearning.Text);
                    cmd.Parameters.AddWithValue("@address", txtadress.Text);
                    cmd.Parameters.AddWithValue("@phone", txtphone1.Text);
                    cmd.Parameters.AddWithValue("@homedesc", txthome.Text);
                    cmd.Parameters.AddWithValue("@job", txtjob.Text);
                    cmd.Parameters.AddWithValue("@incom", cmbincom.Text);
                    cmd.Parameters.AddWithValue("@noots", txtnots.Text);
                    cmd.Parameters.AddWithValue("@iddimage", savephoto1());
                    cmd.Parameters.AddWithValue("@shid", Convert.ToInt32(comboBox2.SelectedValue));
                    cmd.Parameters.AddWithValue("@sex", comboBox1.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();



                    MessageBox.Show("تم التحديث بنجاح");

                    GetData();
                    conn.Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("حاول مجددا");
                }
            }
            else
            {

                try
                {
                    conn.Open();
                    string qry = "update   BENF_TBL SET benfname = @benfname ,ssn = @ssn ,relation = @relation ,learning = @learning ,address = @address ,phone = @phone, homedesc = @homedesc ," +
                        "job = @job ,incom = @incom ,noots = @noots,iddimage = @iddimage ,sex=@sex   where  Benfid='" + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";//inner join Injurd_TBL on ingid=injuredid
                    SqlCommand cmd = new SqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@benfname", txtname.Text);
                    cmd.Parameters.AddWithValue("@ssn", txtidd.Text);
                    cmd.Parameters.AddWithValue("@relation", cmbsk.Text);
                    cmd.Parameters.AddWithValue("@learning", cmblearning.Text);
                    cmd.Parameters.AddWithValue("@address", txtadress.Text);
                    cmd.Parameters.AddWithValue("@phone", txtphone1.Text);
                    cmd.Parameters.AddWithValue("@homedesc", txthome.Text);
                    cmd.Parameters.AddWithValue("@job", txtjob.Text);
                    cmd.Parameters.AddWithValue("@incom", cmbincom.Text);
                    cmd.Parameters.AddWithValue("@noots", txtnots.Text);
                    cmd.Parameters.AddWithValue("@iddimage", savephoto1());
                    cmd.Parameters.AddWithValue("@sex", comboBox1.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("تم التحديث بنجاح");
                    GetData();
                    conn.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("حاول مجددا");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("delete  from  BENF_TBL where Benfid ='" + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", conn);
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
            try
            {


                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    txtname.Text = row.Cells[1].Value.ToString();
                    txtidd.Text = row.Cells[2].Value.ToString();
                    cmbsk.Text = row.Cells[3].Value.ToString();
                    cmblearning.Text = row.Cells[4].Value.ToString();
                    txtadress.Text = row.Cells[5].Value.ToString();
                    txtphone1.Text = row.Cells[6].Value.ToString();
                    txthome.Text = row.Cells[7].Value.ToString();
                    txtjob.Text = row.Cells[8].Value.ToString();
                    cmbincom.Text = row.Cells[9].Value.ToString();
                    txtnots.Text = row.Cells[10].Value.ToString();
                     pictureBox1.Image = (row.Cells[11].Value is DBNull) ? Resources.download : Getphoto((byte[])row.Cells[12].Value);
                    comboBox2.Text = row.Cells[12].Value.ToString();
                    comboBox3.Text = row.Cells[13].Value.ToString();
                    comboBox1.Text = row.Cells[14].Value.ToString();
                    Save.Enabled = false;

                }
            }
            catch (Exception)
            {

                return;
            }
        }
    }
}
