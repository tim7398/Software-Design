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

//********** change solid border to dotted

namespace Lab5
{
    public partial class Form1 : Form
    {
        private ArrayList Drawlist = new ArrayList();
        private bool first_click = true, outline = false, fill = false;
        private Point first_location, second_location;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) // initialize the list options to the first one
        {
            radioButton1.Checked = true;
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;
            listBox3.SelectedIndex = 0;
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e) // sets up the UI
        {
            Graphics g = e.Graphics;
            Color c = Color.FromArgb(192, 192, 192);
            this.BackColor = Color.White;
            g.FillRectangle(new SolidBrush(c), 0, 0, Width, 275);
            radioButton2.BackColor = Color.FromArgb(192, 192, 192);
            radioButton1.BackColor = Color.FromArgb(192, 192, 192);
            radioButton3.BackColor = Color.FromArgb(192, 192, 192);
            radioButton4.BackColor = Color.FromArgb(192, 192, 192);
            checkBox1.BackColor = Color.FromArgb(192, 192, 192);
            checkBox2.BackColor = Color.FromArgb(192, 192, 192);
            label1.BackColor = Color.FromArgb(192, 192, 192);
            label2.BackColor = Color.FromArgb(192, 192, 192);
            label3.BackColor = Color.FromArgb(192, 192, 192);
            label4.BackColor = Color.FromArgb(192, 192, 192);
            panel2.Size = new Size(Width, Height);
            Pen rec = new Pen(Brushes.LightGray);
            g.DrawRectangle(rec, 30, 57, 217, 189);
            

        }

      

        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Drawlist.Clear(); // clears all items from list
            panel2.Invalidate(); // invalidate to actually refresh panel2paint handler to clear.
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close(); // closes program
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e) // removes last element drawn
        {
            if (Drawlist.Count > 0) // makes sure the arraylist is not empty
            {
                Drawlist.RemoveAt(Drawlist.Count - 1);
            }
            panel2.Invalidate();
        }

   

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Shape shape in Drawlist) // loops through the shape object arraylist; polymorphism makes this contain the child classes. 
            {
                shape.Draw_Shape(g);
            }
        
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if(first_click) // first click only saves location
            {
                first_click = false; // makes sure to go to second state
                first_location = e.Location; // saves first location
                return; // restarts
            }
            Brush pen_brush = null, fill_brush=null; // declare brush variable to store color
            first_click = true; // makes sure next click is first click again
            second_location = e.Location; // stores second variable
            outline = false;
            fill = false;
            Pen pen = null;
            switch (listBox1.SelectedIndex) // choose pen color
            {
                case 0: pen_brush = Brushes.Black;
                    break;
                case 1: pen_brush = Brushes.Red;
                    break;
                case 2: pen_brush = Brushes.Blue;
                    break;
                case 3: pen_brush = Brushes.Green;
                    break;
                default: pen_brush = null;
                    break;
            }
            if(radioButton4.Checked && (pen_brush != null) && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                text textbox = new text(pen_brush, first_location, second_location, textBox1.Text);
                Drawlist.Add(textbox);
                panel2.Invalidate();
            }
            
            if(radioButton1.Checked && (pen_brush!=null)) // see if line is checked, and makes sure pen_brush has a value before making the pen object
            {
                pen = new Pen(pen_brush, listBox3.SelectedIndex);
                
                Line line = new Line(pen, first_location, second_location); // creates a new line object with the calculated attributes
                Drawlist.Add(line); // adds the line object to the array list
                panel2.Invalidate(); // invalidates so it will draw

            }
            if(checkBox2.Checked) // check to see if there is an outline
            {
                outline = true;
            }

         
            
            if (checkBox1.Checked) // see if fill is checked
            {
                switch(listBox2.SelectedIndex) // set fill color to brush variable
                {
                    case 0: fill_brush = Brushes.White;
                        break;
                    case 1: fill_brush = Brushes.Black;
                        break;
                    case 2: fill_brush = Brushes.Red;
                        break;
                    case 3: fill_brush = Brushes.Blue;
                        break;
                    case 4: fill_brush = Brushes.Green;
                        break;
                    default: fill_brush= null;
                        break;
                }

                fill = true;

            }

            if (radioButton2.Checked && (fill || outline)&& (pen_brush != null))// rectangle is checked along with outline or fill. Makes sure not null
            {
                pen = new Pen(pen_brush, listBox3.SelectedIndex);
                Rec_shape rectangle = new Rec_shape(pen, fill_brush, first_location, second_location, outline, fill);
                Drawlist.Add(rectangle);
                panel2.Invalidate();
            }

            if (radioButton3.Checked && (fill || outline) && (pen_brush != null))// rectangle is checked along with outline or fill. Makes sure not null
            {
                pen = new Pen(pen_brush, listBox3.SelectedIndex);
                Ellipse ellipse = new Ellipse(pen, fill_brush, first_location, second_location, outline, fill);
                Drawlist.Add(ellipse);
                panel2.Invalidate();
            }


        }
    }
}

