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
using System.Security.Cryptography;


namespace Lab7
{
  
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //no intial border highlights
            button1.TabStop = false;
            textBox1.TabStop = false;
            textBox2.TabStop = false;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private bool is_encryptError() // check if theres any errors with encryption
        {
            
            if(textBox2.Text=="") // first check if there is a key.
            {
                MessageBox.Show("Please enter a Key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return true;
            }

            if((textBox1.Text == "")) // check if path to file is empty
            {
                MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }

        private bool is_decryptError() // error with decryption
        {
            if (textBox2.Text == "") // first check if there is a key.
            {
                MessageBox.Show("Please enter a Key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            if ((textBox1.Text == "") || Path.GetExtension(textBox1.Text) != ".des") // check if path is empty, or if the extension is .des or not.
            {
                MessageBox.Show("Not a .des file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }
        private void EncryptData(String inName, String outName, byte[] desKey, byte[] desIV) //encrypts the file
        {
            // first create file streams for input and output files
            FileStream fileIn = null;
            FileStream fileOut = null;
            //check for error in opening/creating files
            try
            {
                fileIn = new FileStream(inName, FileMode.Open, FileAccess.Read); //input file
                fileOut = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write); //output file
                fileOut.SetLength(0);
            }
            catch
            {
                MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if(fileIn != null) // make sure its not null before closing
                {
                   fileIn.Close();
                }
                if(fileOut != null)
                {
                   fileOut.Close();
                }
                
                return;
            }
            byte[] tempStorage = new byte[100]; //This is intermediate storage for the encryption.
            long readLength = 0;              //This is the total number of bytes written.
            long totalLength = fileIn.Length;    //This is the total length of the input file.
            int length;                     //This is the number of bytes to be written at a time.

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fileOut, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);

            while(readLength < totalLength) // reads from input file, then encrypts and writes to output file.
            {
                length = fileIn.Read(tempStorage, 0, 100);
                encStream.Write(tempStorage, 0, length);
                readLength = readLength + length;
            }
            encStream.Close();
            fileOut.Close();
            fileIn.Close();

        }

        private void DecryptData(String inName, String outName, byte[] desKey, byte[] desIV) //decrypts the data
        {
            bool delete = false;
            // first create file streams for input and output files
            FileStream fileIn = null;
            FileStream fileOut = null;
            //check for error in opening/creating files
            try
            {
                fileIn = new FileStream(inName, FileMode.Open, FileAccess.Read); //input file
                fileOut = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write); //output file
                fileOut.SetLength(0);
            }
            catch
            {
                MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (fileIn != null) // make sure its not null before closing
                {
                    fileIn.Close();
                }
                if (fileOut != null)
                {
                    fileOut.Close();
                }

                return;
            }
            byte[] tempStorage = new byte[100]; //This is intermediate storage for the encryption.
            long readLength = 0;              //This is the total number of bytes written.
            long totalLength = fileIn.Length;    //This is the total length of the input file.
            int length;                     //This is the number of bytes to be written at a time.

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fileOut, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);
            try
            {
                while (readLength < totalLength) // reads from input file, then encrypts and writes to output file.
                {
                    length = fileIn.Read(tempStorage, 0, 16);
                    encStream.Write(tempStorage, 0, length);
                    readLength = readLength + length;
                }
                encStream.Close();
            }
            catch
            {
                MessageBox.Show("Bad key or file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                delete = true;
            }
            
            fileOut.Close();
            fileIn.Close();
            if(delete) // cannot delete file while it is open with stream
            {
                File.Delete(outName);
            }
        }
        private byte[] setKey() // sets the key
        {
            byte[] key = new byte[8];
            int temp = 0;
            for (int x = 0; x < textBox2.Text.Length; x++) // generating the key 
            {
                key[temp] = (byte)(key[temp] + (byte)textBox2.Text[x]);
                temp = (temp + 1) % 8; // if more than 8 characters, wraps from the beginning of the array.
            }
            return key; // return the key
        }

        private void button1_Click(object sender, EventArgs e) // for file location
        {
            //the dialog for looking for files
            OpenFileDialog fileDialog = new OpenFileDialog();

            
            if (fileDialog.ShowDialog() == DialogResult.OK)// only change content of textbox if open is pressed, otherwise the content is deleted.
            {
                // set file text box as file name
                this.textBox1.Text = fileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e) // encrypt
        {
            if(is_encryptError())
            {
                return;
            }
            else
            {
                
                string inName = textBox1.Text;
                string outName = textBox1.Text + ".des";
                
                if(File.Exists(outName)) // check if output file exists
                {
                    if (MessageBox.Show("Output file exists. Overwrite?", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) // if no, do not do anything
                    {
                        return;
                    }
                }
                byte[] key = new byte[8];
                key = setKey(); // key is set
                EncryptData(inName, outName, key, key);
            }
        }

        private void button3_Click(object sender, EventArgs e)// decrypt
        {
            if (is_decryptError())
            {
                return;
            }
            else
            {
                string inName = textBox1.Text;
                string outName = textBox1.Text.Substring(0,textBox1.Text.Length-3); // takes out the .des extension
                
                if (File.Exists(outName)) // check if output file exists
                {
                    if (MessageBox.Show("Output file exists. Overwrite?", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) // if no, do not do anything
                    {
                        return;
                    }
                }
                byte[] key = new byte[8];
                key = setKey(); // key is set
                DecryptData(inName, outName, key, key);
            }

        }
    }
}
