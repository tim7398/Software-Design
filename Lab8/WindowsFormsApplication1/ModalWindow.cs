using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
//display full screen without title bar: form border style none
namespace Lab8
{
    public partial class ModalWindow : Form
    {
        private int counter = 0;
        private string[] images;
        public int interval = 0;
        public ModalWindow()
        {
            InitializeComponent();
        }

        public void set_image(string[] images) // set images to use in this form
        {
            this.images = images; // sets the array to images path
        }

        private void ModalWindow_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e) // comes here when image changes 
        {
            counter++; // image number
            if(counter == images.Count()) // once the images have all been seen, end slide show.
            {
                timer1.Enabled = false; // stop the timer
                DialogResult = DialogResult.OK;
                return;
            }
            this.Invalidate();
        }

        private void ModalWindow_Activated(object sender, EventArgs e)
        {
            counter = 0;
            timer1.Interval = 1000 * interval; // converts from mili to seconds.
            timer1.Enabled = true; // makes sure the timer starts counting.
        }

        private void ModalWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            try // make sure its a valid image file
            {

                Image img = Image.FromFile(images[counter]);
                int width = img.Width;
                int height = img.Height;
                SizeF clientSize = base.ClientSize;
                float scale = Math.Min(clientSize.Height / (float)height, clientSize.Width / (float)width); // makes sure there is proper scaling.

                // draw image to center of form
                g.DrawImage(img, (clientSize.Width - (float)width * scale) / 2f, (clientSize.Height - (float)height * scale) / 2f, (float)width * scale, (float)height * scale);

            }
            catch // if not valid image, display message
            {
                g.DrawString("Not an image file!", new Font("Arial", 30), Brushes.Red,0,0);
            }
        }

        private void ModalWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape) // if esc key pressed, dialog
            {
                DialogResult = DialogResult.OK;
                return;
            }
        }
    }
}
