using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalShohdaE.Forms
{
    public partial class MAIN_FRM : Form
    {
        public MAIN_FRM()
        {
            InitializeComponent();
        }

        private void تسجبلالدخولToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UU frm = new UU();
            frm.ShowDialog();
        }

        private void عملنسخةاحتياطيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_BuckUp frm = new Form_BuckUp();
            frm.ShowDialog();
        }

        private void استخراجنسخةمحفوظةToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FORM_RESTOR frm = new FORM_RESTOR();
            frm.ShowDialog();
        }

        private void المدربينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            INJFRM frm = new INJFRM();
            frm.ShowDialog();
        }

        private void المشروعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SHOFRM frm = new SHOFRM();
            frm.ShowDialog();
        }

        private void المستفيدينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BNFFRM frm = new BNFFRM();
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AIDSFRM frm = new AIDSFRM();
            frm.ShowDialog();
        }

        private void الانشطةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activity_FRM frm = new Activity_FRM();
            frm.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SUBMINU frm = new SUBMINU();
            frm.ShowDialog();
        }
    }
}
