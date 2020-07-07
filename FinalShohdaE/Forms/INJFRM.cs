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
    public partial class INJFRM : Form
    {
        FLL frm = new FLL();
        int ID = 0;
        SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=sh;Trusted_Connection=True;");
        SqlDataAdapter da;
        DataTable dcenter = new DataTable();
        DataTable dvillege = new DataTable();
        public INJFRM()
        {
            InitializeComponent();
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM MX";
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //cmbcentr.Items.Add(dr["center"]);
                cmbSC.Items.Add(dr["sotialstate"]);
                cmpworkk.Items.Add(dr["workk"]);
                cmbincom.Items.Add(dr["incom"]);
                cmblearning.Items.Add(dr["learning"]);

            }
            conn.Close();

            da = new SqlDataAdapter("select * from Center", conn);
            da.Fill(dcenter);
            cmbcentr.DataSource = dcenter;
            cmbcentr.ValueMember = "id";
            cmbcentr.DisplayMember = "cname";
            conn.Close();
        }
        public byte[] savephoto1()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            try
            {
                conn.Open();
                string qry = "insert into BENF_TBL (benfname,ssn,relation,learning,address,phone,homedesc,job,incom,noots,iddimage ,sex)  Values " +
                "(@benfname,@ssn,@relation,@learning,@address,@phone,@homedesc,@job,@incom,@noots,@iddimage ,@sex)";
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@benfname", txtname.Text);
                cmd.Parameters.AddWithValue("@ssn", txtidd.Text);
                cmd.Parameters.AddWithValue("@relation", txtname.Text);
                cmd.Parameters.AddWithValue("@learning", cmblearning.Text);
                cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                cmd.Parameters.AddWithValue("@phone", txtphone1.Text);
                cmd.Parameters.AddWithValue("@homedesc", txthome.Text);
                cmd.Parameters.AddWithValue("@job", cmpworkk.Text);
                cmd.Parameters.AddWithValue("@incom", cmbincom.Text);
                cmd.Parameters.AddWithValue("@noots", tx.Text);
                cmd.Parameters.AddWithValue("@iddimage", savephoto1());
                cmd.Parameters.AddWithValue("@ingid", this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd.Parameters.AddWithValue("@sex", comboBox2.Text);
                cmd.Parameters.AddWithValue("@re", lre.Text);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("تم الاضافة بنجاح");
                GetData();
                //clear();

            }
            catch (Exception)
            {

                MessageBox.Show("حاول مجددا");
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string qry = "insert into Injurd_TBL (enjname,injured_num ,snn ,report_num ,workk ,sotial_state ,sons_num,center, adress ,phone ,indecate ,needs,agz,villege ,iddimage,sex,re)  Values " +
                        "(@enjname , @injured_num , @snn , @report_num , @workk ,@sotial_state , @sons_num , @center, @adress ,@phone ,@indecate , @needs ,@agz , @villege ,@iddimage ,@sex,@re)";
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@enjname", txtname.Text);
                cmd.Parameters.AddWithValue("@injured_num", txtcode.Text);
                cmd.Parameters.AddWithValue("@snn", txtidd.Text);
                cmd.Parameters.AddWithValue("@report_num", txtreport.Text);
                cmd.Parameters.AddWithValue("@workk", cmpworkk.Text);
                cmd.Parameters.AddWithValue("@sotial_state", cmbSC.Text);
                cmd.Parameters.AddWithValue("@sons_num", txtsunnum.Text);
                cmd.Parameters.AddWithValue("@center", cmbcentr.Text);
                cmd.Parameters.AddWithValue("@adress", txtaddress.Text);
                cmd.Parameters.AddWithValue("@phone", txtphone1.Text);
                cmd.Parameters.AddWithValue("@indecate", txtwakaa.Text);
                cmd.Parameters.AddWithValue("@needs", txtwants.Text);
                cmd.Parameters.AddWithValue("@agz", txtAgz.Text);
                cmd.Parameters.AddWithValue("@villege", comboBox1.Text);
                cmd.Parameters.AddWithValue("@iddimage", savephoto1());
                cmd.Parameters.AddWithValue("@sex", comboBox2.Text);
                cmd.Parameters.AddWithValue("@re", frm.textBox1.Text);
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string qry = "update   Injurd_TBL SET enjname = @enjname ,injured_num = @injured_num ,snn = @snn ,report_num = @report_num ,workk = @workk ,sotial_state = @sotial_state ,sons_num = @sons_num, center = @center, adress = @adress ,phone = @phone ,indecate = @indecate ,needs = @needs ,agz = @agz ,villege = @villege ,iddimage=@iddimage ,sex=@sex  where  injuredid='" + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@enjname", txtname.Text);
                cmd.Parameters.AddWithValue("@injured_num", txtcode.Text);
                cmd.Parameters.AddWithValue("@snn", txtidd.Text);
                cmd.Parameters.AddWithValue("@report_num", txtreport.Text);
                cmd.Parameters.AddWithValue("@workk", cmpworkk.Text);
                cmd.Parameters.AddWithValue("@sotial_state", cmbSC.Text);
                cmd.Parameters.AddWithValue("@sons_num", txtsunnum.Text);
                cmd.Parameters.AddWithValue("@center", cmbcentr.Text);
                cmd.Parameters.AddWithValue("@adress", txtaddress.Text);
                cmd.Parameters.AddWithValue("@phone", txtphone1.Text);
                cmd.Parameters.AddWithValue("@indecate", txtwakaa.Text);
                cmd.Parameters.AddWithValue("@needs", txtwants.Text);
                cmd.Parameters.AddWithValue("@agz", txtAgz.Text);
                cmd.Parameters.AddWithValue("@villege", comboBox1.Text);
                cmd.Parameters.AddWithValue("@iddimage", savephoto1());
                cmd.Parameters.AddWithValue("@sex", comboBox2.Text);
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
                SqlCommand cmd = new SqlCommand("delete  from  Injurd_TBL where injuredid='" + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", conn);
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
        public void GetData()
        {
            string qry = "select  *  Injurd_TBL";
            SqlCommand cmd = new SqlCommand(qry, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "dt");
            conn.Close();

            this.dataGridView1.DataSource = ds.Tables[0];
        }
        void clear()
        {
            txtname.Text = "";
            txtphone1.Text = "";
            txtidd.Text = "";
            txtAgz.Text = "";
            txtaddress.Text = "";
            txtreport.Text = "";
            txtwakaa.Text = "";
            txtwants.Text = "";
            txtcode.Text = "";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            cmbincom.Visible = true;
            txthome.Visible = true;
            tx.Visible = true;
            button5.Visible = true;
            label19.Visible = true;
            label20.Visible = true;
            label21.Visible = true;
            cmblearning.Visible = true;
            label22.Visible = true;
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
        private Image Getphoto(byte[] value)
        {
            MemoryStream ms = new MemoryStream(value);
            return Image.FromStream(ms);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtname.Text = row.Cells[1].Value.ToString();
                txtcode.Text = row.Cells[2].Value.ToString();
                txtidd.Text = row.Cells[3].Value.ToString();
                txtreport.Text = row.Cells[4].Value.ToString();
                cmpworkk.Text = row.Cells[5].Value.ToString();
                cmbSC.Text = row.Cells[6].Value.ToString();
                txtsunnum.Text = row.Cells[7].Value.ToString();
                cmbcentr.Text = row.Cells[8].Value.ToString();
                txtaddress.Text = row.Cells[9].Value.ToString();
                txtphone1.Text = row.Cells[10].Value.ToString();
                txtwakaa.Text = row.Cells[11].Value.ToString();
                txtwants.Text = row.Cells[12].Value.ToString();
                txtAgz.Text = row.Cells[13].Value.ToString();
                comboBox1.Text = row.Cells[14].Value.ToString();
                pictureBox1.Image = (row.Cells[15].Value is DBNull)?Resources.download :Getphoto ((byte[])row.Cells[15].Value);
                comboBox2.Text = row.Cells[16].Value.ToString();
            }
        }

        private void txtidd_Validated(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select snn from Injurd_TBL where injured_num='" + txtcode.Text + "'", conn);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                MessageBox.Show("هذا الكود موجود لدي مصاب اخر");
                txtcode.Focus();
            }
        }
    }
    }

