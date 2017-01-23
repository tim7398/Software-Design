using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Lab8
{
    public partial class Form1 : Form
    {

        string openfilename;
        public Form1()
        {
            InitializeComponent();
        }
        ModalWindow displayform = new ModalWindow();

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "5"; //default value
        }

        private void button1_Click(object sender, EventArgs e) // add button
        {
            // opens a windows dialog to choose files
            OpenFileDialog FilesDialog = new OpenFileDialog();
            FilesDialog.Filter = "*.jpg;*.gif;*.png;*.bmp|*.jpg;*.gif;*.png;*.bmp |*.*(All Files)|*.*"; // two filtering options for types of files
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
            

            for(int x = length-1; x>=0; x--) // prevent out of bound errors
            {
                listBox1.Items.Remove(listBox1.SelectedItems[x]);
            }
           
        }

        private void button3_Click(object sender, EventArgs e) // show button
        {
            if(this.listBox1.Items.Count == 0) // check to make sure there is something to display
            {
                MessageBox.Show("No images to show.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            //puts time into modal form
            //transfers image array to modal form
            string[] temp_array = new string[listBox1.Items.Count];
            this.listBox1.Items.CopyTo(temp_array,0);
            displayform.set_image(temp_array); 

            try // tries to set the interval from user input
            {
                displayform.interval = Convert.ToInt32(this.textBox1.Text);
                if(displayform.interval <=0)  // make sure interval is valid
                {
                    throw new Exception();
                }
                displayform.ShowDialog();

            }
            catch
            {
                MessageBox.Show("Please enter an integer time interval > 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); // makes sure its a valid integer
            }



        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) //closes the program
        {
            this.Close();
        }

        private void openCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog files = new OpenFileDialog();
            files.Filter = "*.pix | *.pix"; 
            files.FilterIndex = 1; // chose which filtering option to use

            if (files.ShowDialog() == DialogResult.OK)// user pressed okay
            {
                this.listBox1.Items.Clear(); // empty out the current list box
                this.openfilename = files.FileName;
                StreamReader read = new StreamReader(files.OpenFile()); // open read stream

                while (true)
                {
                    string line = read.ReadLine(); // read path from file 

                    if (line == null)
                    {
                        break;
                    }
                    listBox1.Items.Add(line); // add path to listbox view
                }
                read.Close();
            }

        }

        private void saveCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog files = new SaveFileDialog();
            files.Filter = " *.pix | *.pix"; // default save type, and display other files of that type
            files.DefaultExt = "pix";
            files.AddExtension = true;

            if (this.listBox1.Items.Count == 0) // if there is nothing to save
            {
                MessageBox.Show("No files name to save.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if(this.openfilename == null) // see if there is a valid file name
            {
                files.FileName = null;
            }
            else
            {
                files.FileName = this.openfilename;
            }

            if(files.ShowDialog() == DialogResult.OK)// if ok is pressed
            {
                StreamWriter write = new StreamWriter(files.OpenFile()); // open write stream

                foreach(string file in listBox1.Items)
                {
                    write.WriteLine(file); // writes the path of image to file
                }

                write.Close();
            }
        }
    }
}
