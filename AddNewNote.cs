using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNotes
{
    public partial class AddNewNote : Form
    {
        public AddNewNote()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          if (string.IsNullOrEmpty(txtTitle.Text))
            {
                errorProvider1.SetError(txtTitle, "This field cannot be empty!");
                return;
            }
          if (string.IsNullOrEmpty(rtbNote.Text))
            {
                errorProvider1.SetError(rtbNote, "This field cannot be empty!");
                return;
            }
            MessageBox.Show("Note Save Successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void AddNewNote_Load(object sender, EventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            rtbNote.Text= string.Empty;
            txtTitle.Text=string.Empty;
            this.Close();
        }
        
        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void AddNewNote_FormClosing(object sender, FormClosingEventArgs e)
        {
           // Form1.SaveNotesToFile();
        }
    }
}
