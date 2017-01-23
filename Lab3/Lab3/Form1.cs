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
namespace Lab3
{
    public partial class Form1 : Form
    {
        private ArrayList coordinates = new ArrayList();
        public Form1()
        {
            InitializeComponent();
            this.Text = "Lab 3";

        }



        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point_Info p = new Point_Info();
                p.X = e.X;
                p.Y = e.Y;
                p.color = true;
               coordinates.Add(p);
                this.Invalidate();
            }

            if (e.Button == MouseButtons.Right)
            {
                int length = coordinates.Count - 1;
                for (int x = length; x >= 0; x--)
                {
                    Point_Info check_p = (Point_Info)coordinates[x];

                    if ((Math.Abs(check_p.X - e.X)) <= 10 && (Math.Abs(check_p.Y - e.Y)) <= 10)
                    {
                        if (check_p.color == false)
                        {
                            coordinates.RemoveAt(x);
                            this.Invalidate();
                        }
                        if (check_p.color == true)
                        {
                            check_p.color = false;
                            this.Invalidate();

                        }

                    }

                }

                this.Invalidate();
            }

        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            const int WIDTH = 20;
            const int HEIGHT = 20;
            Graphics g = e.Graphics;

            foreach (Point_Info p in this.coordinates)
            {
                //string message = string.Format("{0}, {1}", p.X, p.Y);
                if (p.color == true)
                {
                    g.FillEllipse(Brushes.Black, p.X - WIDTH / 2, p.Y - WIDTH / 2, WIDTH, HEIGHT);
                }
                //g.DrawString(message, Font, Brushes.Black, p.X + WIDTH, p.Y - HEIGHT);
                if (p.color == false)
                {
                    g.FillEllipse(Brushes.Red, p.X - WIDTH / 2, p.Y - WIDTH / 2, WIDTH, HEIGHT);
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.coordinates.Clear();
            this.Invalidate();

        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.coordinates.Clear();
            this.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }


    public class Point_Info
    {
        private int x;
        private int y;
        private bool Color;
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public int Y
        {

            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public bool color
        {
            get
            {
                return Color;
            }
            set
            {
                Color = value;
            }
        }



    }
}

