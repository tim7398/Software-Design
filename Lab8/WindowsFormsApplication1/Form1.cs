using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // add button
        {
            // opens a windows dialog to choose files
            OpenFileDialog FilesDialog = new OpenFileDialog();
            FilesDialog.Multiselect = true; // multiple selection possible

            // if user does not press ok, nothing happens
            if (FilesDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            // add the selected files to listbox
            for (int x = 0; x < (int)FilesDialog.FileNames.Length; x++)
            {
                this.listBox1.Items.Add(FilesDialog.FileNames[x]);
            }

        }

        private void button2_Click(object sender, EventArgs e) // delete button
        {
            int length = listBox1.SelectedItems.Count;
            

            for(int x = length-1; x >=0; x--)
            {
                listBox1.Items.Remove(listBox1.SelectedItems[x]);
            }
           
        }

        private void button3_Click(object sender, EventArgs e) // show button
        {

        }
    }
}
