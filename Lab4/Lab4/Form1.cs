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

namespace Lab4
{
    public partial class Form1 : Form
    {

        private ArrayList coordinates = new ArrayList(); // array to hold coordinates of rectangles
        private int num_queen = 0; // tracks number of Q's on the board
        private bool[,] grid_taken = new bool[8, 8]; // if rectangle on grid is valid
        private bool first_enter = true; // makes sure message display once for win
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void make_valid(int square_x, int square_y) // makes the squares associated to the removed queen valid
        {
            int length = 8;
            int y = square_y;
            int y2 = square_y;
            for (int x = 0; x < length; x++) // validate the row/column
            {
                grid_taken[x, square_y] = false;
                grid_taken[square_x, x] = false;
            }
            for (int x = square_x; x < length; x++) // diagonal right down/right up
            {
                if (8 > y) // right down diagonal
                {
                    grid_taken[x, y] = false;
                    y++;
                }
                if (-1 < y2) // right up diagonal
                {
                    grid_taken[x, y2] = false;
                    y2--;
                }

            }
            y = square_y;
            y2 = square_y;
            for (int x = square_x; x >= 0; x--) // left diagonal
            {
                if (8 > y) // left down diagonal
                {
                    grid_taken[x, y] = false;
                    y++;
                }
                if (-1 < y2) // left up diagonal
                {
                    grid_taken[x, y2] = false;
                    y2--;
                }
            }
        }
        private void invalidate_squares(int square_x, int square_y) // invalidates the squares associated with the queen
        {
            int length = 8;
            int y = square_y;
            int y2 = square_y;
            for (int x = 0; x < length; x++) // invalidate the row/column
            {
                grid_taken[x, square_y] = true;
                grid_taken[square_x, x] = true;
            }
            for (int x = square_x; x < length; x++) // diagonal right down/right up
            {
                if (8 > y) // right down diagonal
                {
                    grid_taken[x, y] = true;
                    y++;
                }
                if (-1 < y2) // right up diagonal
                {
                    grid_taken[x, y2] = true;
                    y2--;
                }

            }
            y = square_y;
            y2 = square_y;
            for (int x = square_x; x >= 0; x--) // left diagonal
            {
                if (8 > y) // left down diagonal
                {
                    grid_taken[x, y] = true;
                    y++;
                }
                if (-1 < y2) // left up diagonal
                {
                    grid_taken[x, y2] = true;
                    y2--;
                }
            }





        }
        private void Form1_Paint(object sender, PaintEventArgs e) // this draws the rectangles for the chess board
        {
            Graphics g = e.Graphics;
            string message = "You have ";
            message = message + num_queen.ToString() + " queens on the board."; // message to display # of queens on board
            g.DrawString(message, Font, Brushes.Black, 225, 45);

            g.PageUnit = GraphicsUnit.Pixel;
            int coord_x = 100, coord_y = 100;

            Pen pen = new Pen(Brushes.Black, 2f); // 1 inch borders

            for (int y = 0; y < 8; y++)// draws the black and white blocks
            {
                for (int x = 0; x < 8; x++)
                {
                    g.DrawRectangle(pen, coord_x, coord_y, 50, 50); // draws rectangle with border first
                    Location square = new Location(); // store coordinate of the current square into an object
                    square.X = coord_x;
                    square.Y = coord_y;
                    square.Draw_Q = false; // does not draw

                    // draw chess board
                    if (0 == (y % 2)) // pattern for even rows. 
                    {
                        if (0 == (x % 2))
                        {
                            g.FillRectangle(Brushes.White, coord_x, coord_y, 50, 50); //fills in the rectangle with color
                            square.Color = false; // false means color white
                        }
                        else
                        {
                            g.FillRectangle(Brushes.Black, coord_x, coord_y, 50, 50);
                            square.Color = true; // true means color black;      
                        }
                    }
                    else // pattern for odd rows
                    {
                        if (1 == (x % 2))
                        {
                            g.FillRectangle(Brushes.White, coord_x, coord_y, 50, 50);
                            square.Color = false;
                        }
                        else
                        {
                            g.FillRectangle(Brushes.Black, coord_x, coord_y, 50, 50);
                            square.Color = true;
                        }
                    }
                    // end chess board drawing
                    if ((true == grid_taken[x, y]) && (true == checkBox1.Checked)) // highlights taken rows red
                    {
                        g.FillRectangle(Brushes.Red, coord_x, coord_y, 50, 50);
                    }
                    coordinates.Add(square); // adds the object with the coordinate info into array
                    coord_x += 50;
                }
                coord_y += 50; // increase the y coordinate for next row
                coord_x = 100; // restart x coordinate from beginning

            }
            pen.Dispose();
            foreach (Location block in coordinates) // see if Q needs to be drawn
            {
                if (true == block.Draw_Q)
                {
                    Font myfont = new Font("arial", 30, FontStyle.Bold);
                    if ((block.Color == false) || (checkBox1.Checked)) // if box is checked or white square, black text
                    {
                        g.DrawString("Q", myfont, Brushes.Black, block.X, block.Y);
                    }
                    else
                    {
                        g.DrawString("Q", myfont, Brushes.White, block.X, block.Y);
                    }

                }
            }
            if (8 == num_queen && first_enter) // display message if 8 queens are put on board
            {
                first_enter = false;
                MessageBox.Show("Congradulations you did it!");
            }

        } // end paint function

        public bool is_valid(int square_x, int square_y) // checks if square is taken
        {
            if (true == grid_taken[square_x, square_y])
            {
                return false;
            }

            return true;

        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int length = coordinates.Count, square_x = 0, square_y = 0, beep = 0;
            if (e.Button == MouseButtons.Left)
            {
                if ((e.X >= 100) && (e.Y >= 100) && (e.X <= 500) && (e.Y <= 500)) // makes sure the clicks are on the board
                {
                    for (int x = 0; x < length; x++)
                    {
                        Location click_square = (Location)coordinates[x];
                        Point p = new Point(click_square.X, click_square.Y);
                        Size area = new Size(50, 50);
                        Rectangle box = new Rectangle(p, area);
                        if (box.Contains(e.X, e.Y))// see if current coordinates are within current rectangle
                        {
                            //This block used to find which square in the grid
                            square_x = click_square.X - 100; // subtract offset of 100,100
                            square_y = click_square.Y - 100;
                            square_x = square_x / 50; // since multiple 50's, this will find X location of block
                            square_y = square_y / 50; // y location of block
                                                      // end finding location of block

                            if (true == is_valid(square_x, square_y)) // check to see if square is valid for Q
                            {
                                beep = 1;
                                click_square.Draw_Q = true;
                                num_queen++;
                                invalidate_squares(square_x, square_y); // invalidates the squares associated to where Q will be drawn
                                //FUNCTION TO NULLIFY THE SQUARES
                                this.Invalidate();
                            }


                        }
                    }
                    if (0 == beep)//only beeps after Q is drawn. without condition, loop will make any click beep
                    {
                        System.Media.SystemSounds.Beep.Play();
                    }

                    beep = 0;
                }

            }
            if (e.Button == MouseButtons.Right)
            {
                if ((e.X >= 100) && (e.Y >= 100) && (e.X <= 500) && (e.Y <= 500)) // makes sure the clicks are on the board
                {
                    for (int x = 0; x < length; x++)
                    {
                        Location click_square = (Location)coordinates[x];
                        Point p = new Point(click_square.X, click_square.Y);
                        Size area = new Size(50, 50);
                        Rectangle box = new Rectangle(p, area);
                        if (box.Contains(e.X, e.Y))//check if rectangle contains clicked coordinates
                        {
                            //This block used to find which square in the grid
                            square_x = click_square.X - 100; // subtract offset of 100,100
                            square_y = click_square.Y - 100;
                            square_x = square_x / 50; // since multiple 50's, this will find X location of block
                            square_y = square_y / 50; // y location of block
                                                      // end finding location of block

                            if (click_square.Draw_Q) // check to see if square is contains a Q
                            {
                                click_square.Draw_Q = false;// erase Q
                                num_queen--;
                                first_enter = true;
                                make_valid(square_x, square_y); // makes the squares valid again
                                foreach (Location Q in coordinates)
                                {
                                    if (Q.Draw_Q) // invalidate all the existing Q's again so the overlapping grids do not get erased
                                    {
                                        square_x = Q.X - 100;
                                        square_y = Q.Y - 100;
                                        square_x = square_x / 50;
                                        square_y = square_y / 50;
                                        invalidate_squares(square_x, square_y);
                                        this.Invalidate();
                                    }
                                }
                                this.Invalidate();
                            }
                        }
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e) // clears the board of everything
        {
            foreach (Location block in coordinates)
            {
                block.Draw_Q = false;
            }
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    grid_taken[x, y] = false;
                }
            }
            num_queen = 0;
            first_enter = true;
            this.Invalidate();

        }
    }
}
