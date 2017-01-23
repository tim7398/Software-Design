using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {
        private const float clientSize = 100;
        private const float lineLength = 80;
        public const float block = lineLength / 3;
        private const float offset = 10;
        private const float delta = 5;
        int state = 0;

        
       
        private float scale; //current scale factor 
        public int[,] grid = new int[3, 3];
        public void reset() // resets the entire board
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    grid[x, y] = 0;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            ResizeRedraw = true;
            reset(); // initializes the board to blank
        }
        public GameEngine game = new GameEngine();
        
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            ApplyTransform(g);
            //draw board
            g.DrawLine(Pens.Black, block, 0, block, lineLength);
            g.DrawLine(Pens.Black, 2 * block, 0, 2 * block, lineLength);
            g.DrawLine(Pens.Black, 0, block, lineLength, block);
            g.DrawLine(Pens.Black, 0, 2 * block, lineLength, 2 * block);

            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (grid[i, j] == -1) // circle
                    {
                        DrawO(i, j, g);
                    }
                    else if (grid[i, j] == 1)// x
                    {
                        DrawX(i, j, g);
                    }
                }
            }

            if(game.tie == true && state == 0)
            {
                System.Windows.Forms.MessageBox.Show("It is a tie");
                state = 1;
            }

            if (game.win == true && state == 0) // displays message after drawing, 
            {
                System.Windows.Forms.MessageBox.Show("You have won");
                state = 1;
            }
            if(game.lose == true && state == 0)
            {
                System.Windows.Forms.MessageBox.Show("You have lost");
                state = 1;
            }
        }
        private void ApplyTransform(Graphics g)
        {
            scale = Math.Min(ClientRectangle.Width / clientSize,
            ClientRectangle.Height / clientSize);
            if (scale == 0f) return;
            g.ScaleTransform(scale, scale);
            g.TranslateTransform(offset, offset);
        }
        private void DrawX(int i, int j, Graphics g)
        {
            g.DrawLine(Pens.Black, i * block + delta, j * block + delta,(i * block) + block - delta, (j * block) + block - delta);
            g.DrawLine(Pens.Black, (i * block) + block - delta, j * block + delta, (i * block) + delta, (j * block) + block - delta);
        }
        private void DrawO(int i, int j, Graphics g)
        {
            g.DrawEllipse(Pens.Black, i * block + delta, j * block + delta, block - 2 * delta, block - 2 * delta);
        }

    
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            ApplyTransform(g);
            PointF[] p = { new Point(e.X, e.Y) };
            g.TransformPoints(CoordinateSpace.World,CoordinateSpace.Device, p);
          
            game.user_click(e,grid,p, block);
            if(!game.computer_start)
            {
                computerStartsToolStripMenuItem.Enabled = false;
            }
            this.Invalidate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reset(); // resets the grid
            game = new GameEngine();
            state = 0;
            computerStartsToolStripMenuItem.Enabled = true;
            this.Invalidate();
        }

        private void computerStartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (game.computer_start)
            {
                game.firstmove = true;
                game.make_move(grid);
                computerStartsToolStripMenuItem.Enabled = false;
                Invalidate();
            }
          
        }
    }
}



