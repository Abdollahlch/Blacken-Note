using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNotes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static  List <string> Notes = new List <string>();
        public static List<string> Titles = new List<string>();
        int NumberOfNotes = (Notes.Count==0) ? 0: Notes.Count-1;
        ushort CurrentNote;
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool isPictureBoxVisible = false;

        void AddNoteBtn(string Text)
        {
            Guna2Button btn = new Guna2Button();
            btn.Location = new Point(50, 92 + (75 * NumberOfNotes));//50, 136
            // btn.Size = new Size(397, 79);

            btn.Size = new Size(300, 66);
            btn.Text = Text;
            btn.Tag = NumberOfNotes.ToString();
            btn.Click += new EventHandler(BtnNote_Click);
            btn.Cursor = Cursors.Hand;
            btn.AutoRoundedCorners = true;
            btn.ContextMenuStrip = cmNotes;
            btn.FillColor = Color.CornflowerBlue;
            btn.ForeColor = Color.MidnightBlue;
            btn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            btn.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            //btn.BackColor = Color.CornflowerBlue;


            //btn.FlatStyle = FlatStyle.Flat;
            //btn.FlatAppearance.MouseOverBackColor = Color.Silver;
            //btn.FlatAppearance.BorderColor = Color.Black;
            //btn.FlatAppearance.BorderSize = 2;
            //btn.FlatAppearance.MouseDownBackColor = Color.Gray;
            splitContainer1.Panel1.Controls.Add(btn);
            NumberOfNotes++;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddNewNote addNewNote = new AddNewNote();
            addNewNote.ShowDialog();
            if (!string.IsNullOrEmpty(addNewNote.txtTitle.Text) && !string.IsNullOrEmpty(addNewNote.rtbNote.Text)) 
            {
                AddNewNote(addNewNote.txtTitle.Text, addNewNote.rtbNote.Text,true);
                SaveNotesToFile();
                guna2CirclePictureBox1.Visible = false;
                guna2HtmlLabel1.Visible = false;
                lblNoNotes.Visible = false;
            }
           
        }
        void AddNewNote(string Title,string NoteContent, bool IsNewNote=false)
        {
            AddNoteBtn(Title);
            richTextBox1.Text = NoteContent;
            if (IsNewNote)
            {
                Notes.Add(NoteContent);
                Titles.Add(Title);
            }
            SaveNotesToFile();
        }

        private void BtnNote_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = Notes[Convert.ToUInt16(((Guna2Button)sender).Tag)];
            CurrentNote = Convert.ToUInt16(((Guna2Button)sender).Tag);
        }
        void LoadFileContentToTheNote(string Path)
        {
            string FileContent = File.ReadAllText(Path);
            richTextBox1.Text = FileContent;
            Notes.Add(FileContent);
        }

        

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "text files (*.txt)|*.txt";
            openFileDialog.DefaultExt = "txt";
            openFileDialog.Title = "Open file";
            if (openFileDialog.ShowDialog()== DialogResult.OK ) 
            {
                LoadFileContentToTheNote(openFileDialog.FileName);
                AddNoteBtn(richTextBox1.Lines[0]+"...");
                Titles.Add(richTextBox1.Lines[0] + "...");
                guna2CirclePictureBox1.Visible = false;
                guna2HtmlLabel1.Visible = false;
                NumberOfNotes++;
                lblNoNotes.Visible = false;
            }
            
        }

       public static void SaveNotesToFile()
        {
            File.WriteAllText("Notes.txt","");
            File.WriteAllText("Titles.txt", "");
            foreach (string Note in  Notes)
            {
                if (Note==Notes.Last())
                {
                    File.AppendAllText("Notes.txt", Note);
                    break;
                }
                File.AppendAllText("Notes.txt", Note+"#//#");
            }
            foreach (string Title in Titles)
            {
                if (Title == Titles.Last())
                {
                    File.AppendAllText("Titles.txt", Title);
                    break;
                }
                File.AppendAllText("Titles.txt", Title + "#//#");
            }
        }

        public void LoadNotesFromFileToList()
        {
            char[] chars = new char[4];
            chars[0] = '#';
            chars[1] = '/';
            chars[2] = '/';
            chars[3] = '#';
            string FileContent = File.ReadAllText("Notes.txt");
            string FileContentTiltes = File.ReadAllText("Titles.txt");
            //string[]  str = FileContent.Split(chars, );
            Notes = FileContent.Split(chars, StringSplitOptions.RemoveEmptyEntries).ToList();
            Titles = FileContentTiltes.Split(chars, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public void AddNotesFromListToApp()
        {
            for (int i=0;i<Titles.Count;i++)
            {
                AddNewNote(Titles[i], Notes[i],false);
            }
        }

        public void swrDarkMode_CheckedChanged(object sender, EventArgs e)
        {
            if (swrDarkMode.Checked)
            {
                this.BackColor = Color.Black;
                richTextBox1.BackColor = Color.Black;
                richTextBox1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                splitContainer1.Panel1.BackColor = Color.Gray;
                groupBox1.BackColor = Color.Gray;
                menuStrip1.BackColor = Color.Gray;
                guna2HtmlLabel1.ForeColor= Color.White;
                lblNoNotes.ForeColor=Color.White;
                

            }
            else
            {
                this.BackColor = Color.White;
                richTextBox1.BackColor = Color.White;
                richTextBox1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
               splitContainer1.Panel1.BackColor = Color.CornflowerBlue;
              
                groupBox1.BackColor=Color.WhiteSmoke;
                menuStrip1.BackColor= Color.WhiteSmoke;
                guna2HtmlLabel1.ForeColor = Color.Black;
                groupBox1.BackColor = Color.CornflowerBlue;
                menuStrip1.BackColor = Color.CornflowerBlue;
                lblNoNotes.ForeColor = Color.Black;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //if (NumberOfNotes>0)
            //{
            //    guna2CirclePictureBox1.Visible = false;
            //    guna2HtmlLabel1.Visible = false;
            //}
            //else
            //{
            //    guna2CirclePictureBox1.Visible = true;
            //    guna2HtmlLabel1.Visible = true;
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadNotesFromFileToList();
            AddNotesFromListToApp();
            timer1.Start();
            
            timer2.Start();
            if(NoNotesExist())
            {
                lblNoNotes.Visible = true;
            }
            else
            {
                lblNoNotes.Visible= false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveNotesToFile();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
           
            guna2Panel1.Controls.Clear();
            guna2Panel1.Visible= false;
            pbDelete.Visible = true;

        }

        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ProgressBar.Increment(20);

            if (ProgressBar.Value >= ProgressBar.Maximum)
            {
                timer2.Stop();
            }
            
        }

        private void richTextBox1_MouseEnter(object sender, EventArgs e)
        {
            pbEdit.Visible = true;
        }

        private async void richTextBox1_MouseLeave(object sender, EventArgs e)
        {
            await Task.Delay(100); // Adjust the delay duration as needed

            if (!isPictureBoxVisible)
            {
                pbEdit.Visible = false;
            }
        }

      

        private void pbEdit_MouseEnter_1(object sender, EventArgs e)
        {
            isPictureBoxVisible = true;
        }

        private void pbEdit_MouseLeave(object sender, EventArgs e)
        {
            isPictureBoxVisible = false;
        }

        private void pbEdit_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = false;
            pbSave.Visible = true;
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.SelectionLength = 0;
            richTextBox1.Focus();
            
        }

        private void pbSave_Click(object sender, EventArgs e)
        {
            Notes[CurrentNote] = richTextBox1.Text;
            richTextBox1.ReadOnly = true;
            MessageBox.Show("Note Saved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SaveNotesToFile();
            pbSave.Visible = false;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NumberOfNotes == 0) return;
            if (MessageBox.Show("Are you sure to delete this note? ", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                Notes.RemoveAt(CurrentNote);
                Titles.RemoveAt(CurrentNote);
                SaveNotesToFile();
                NumberOfNotes--;
                
                Application.Restart();
            }
        }
        void DeleteButton()
        {
            foreach (Control control in splitContainer1.Panel1.Controls)
            {
                if (control is Guna2Button)
                {
                    splitContainer1.Panel1.Controls.Remove(control);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (NumberOfNotes == 0) return;
            if (MessageBox.Show("Are you sure to delete this note? ","Confirm",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning)==DialogResult.OK)
            {
                Notes.RemoveAt(CurrentNote);
                Titles.RemoveAt(CurrentNote);
                SaveNotesToFile();
                NumberOfNotes--;
                
                Application.Restart();
            }
        }
        bool NoNotesExist()
        {
            return (NumberOfNotes == 0);
        }
        private void splitContainer1_Panel1_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (NoNotesExist())
            {
                lblNoNotes.Visible = true;
            }
            else
                lblNoNotes.Visible = false;
        }

        private void cmNotes_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
