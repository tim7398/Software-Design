using System;
using System.Windows.Forms;
using System.Drawing;

public class GameEngine
{
    public bool is_turn = true;
    public bool done = false, win = false, lose = false, tie = false;
    public bool computer_start = true;
    public bool firstmove = false;
    private int[,] temp_grid = new int[3, 3];

    public bool is_tie(int[,] grid)
    {
        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                if(grid[x,y]==0)
                {
                    return false;
                }
            }
        }
        return true;
    }




    public bool is_lose(int[,] grid)
    {
        int counter = 0, counter2 = 0;
        int x = 0, y = 1, x2 = 0;
        while (x < 3) // goes along first row
        {
            if (grid[x, 0] == -1) // check if O
            {
                counter++;
                y = 1;
                counter2 = 0;
                while (y < 3) // check the column
                {
                    if (grid[x, y] == -1)
                    {
                        counter2++;
                    }
                    y++;
                }
                if (counter2 == 2)  // see if three x's found
                {
                    return true;
                }
                counter2 = 0;

                if (x == 0) // down right diagnol
                {
                    y = 1;
                    x2 = x + 1;
                    while (y < 3) // check diagonal
                    {
                        if (grid[x2, y] == -1)
                        {
                            counter2++;
                            x2++;
                            y++;
                            continue;
                        }
                        break;
                    }
                    if (counter2 == 2) // see if three x's found
                    {
                        return true;
                    }

                }

                if (x == 2) // down left diagnol
                {
                    y = 1;
                    x2 = x - 1;
                    while (y < 3) // check diagonal
                    {
                        if (grid[x2, y] == -1)
                        {
                            counter2++;
                            x2--;
                            y++;
                            continue;
                        }
                        break;
                    }
                    if (counter2 == 2) // see if three x's found
                    {
                        return true;
                    }
                }
                x++;
                continue;
            }


            y = 1;
            x++;
        }
        if (counter == 3)
        {
            return true;
        }
        counter = 0;

        for (int j = 1; j < 3; j++)// check columns to see if win
        {
            for (int z = 0; z < 3; z++)
            {
                if (grid[z, j] == -1)
                {

                    counter++;
                }
                if (counter == 3)
                {
                    return true;
                }
            }
            counter = 0;


        }

        return false;
    }
    public bool is_win(int[,] grid)
    {
        int counter = 0, counter2 = 0;
        int x = 0, y = 1, x2 = 0;
        while(x < 3) // goes along first row
        {
            if(grid[x,0] == 1) // check if x
            {
                counter++;
                y = 1;
                counter2 = 0;
                while(y<3) // check the column
                {
                    if(grid[x,y]==1)
                    {
                        counter2++;
                    }
                    y++;
                }
                if (counter2 == 2)  // see if three x's found
                {
                    return true;
                }
                counter2 = 0;

                if (x == 0) // down right diagnol
                {
                    y = 1;
                    x2 = x+1;
                    while (y < 3) // check diagonal
                    {
                        if(grid[x2,y]==1)
                        {
                            counter2++;
                            x2++;
                            y++;
                            continue;
                        }
                        break;
                    }
                    if (counter2 == 2) // see if three x's found
                    {
                        return true;
                    }
                    
                }

                if(x == 2) // down left diagnol
                {
                    y = 1;
                    x2 = x - 1;
                    while (y < 3) // check diagonal
                    {
                        if (grid[x2, y] == 1)
                        {
                            counter2++;
                            x2--;
                            y++;
                            continue;
                        }
                        break;
                    }
                    if (counter2 == 2) // see if three x's found
                    {
                        return true;
                    }
                }
                x++;
                continue;
            }
            
           
            y = 1;
            x++;
        }
        if (counter == 3)
        {
            return true;
        }
        counter = 0;
        
      for(int j = 1; j < 3; j++)// check columns to see if win
        {
            for( int z = 0; z < 3; z++)
            {
                if(grid[z,j]==1)
                {

                    counter++;
                }
                if (counter == 3)
                {
                    return true;
                }
            }
            counter = 0;
           
            
        }
 
        return false;
    }
    public void user_click(MouseEventArgs e, int[,]grid, PointF[] p, float block)
    {
       
        //only allow setting empty cells
        if (p[0].X < 0 || p[0].Y < 0)
        {
            return;
        }
        int i = (int)(p[0].X / block);
        int j = (int)(p[0].Y / block);
        if (i > 2 || j > 2)
        {
            return;
        }
        if (done)
        {
            System.Windows.Forms.MessageBox.Show("Game Over, Start A New Game");
            return;
        }
        if (grid[i,j] !=0)
        {
            System.Windows.Forms.MessageBox.Show("Invalid Move");
            return;
        }
        if (grid[i, j] == 0 && !done) // checks if theres a winner
        {
            computer_start = false;
            if (e.Button == MouseButtons.Left && is_turn)
            {
                grid[i, j] = 1; // puts x in that location
                is_turn = false;

            }
            
       
            if (is_win(grid)) // check win
            {
                done = true;
                win = true;
                return;
            }
            if (!is_turn) // computer goes
            {
                make_move(grid);
                is_turn = true;

            }
            if (is_lose(grid)) // check lose
            {
                done = true;
                lose = true;
                return;
            }

            if (is_tie(grid)) // check tie
            {
                done = true;
                tie = true;
                return;
            }

        }
    }


    public void make_move(int [,] grid) // computer makes a move
    {
        
        if (firstmove) // computer makes first move, choses random corner
        {
            Random random = new Random();
            int num = random.Next(0, 3);
            switch(num) // chooses random corner based on random number generator.
            {
                case 0: grid[0, 0] = -1;
                    break;
                case 1:
                    grid[0, 2] = -1;
                    break;
                case 2:
                    grid[2, 0] = -1;
                    break;
                case 3:
                    grid[2, 2] = -1;
                    break;
                default: grid[0, 0] = -1;
                    break;

            }
            firstmove = false;
            computer_start = false;
        }


        else // move based on user
        {
            temp_grid = grid; // for trying out different moves.
            for (int x = 0; x < 3; x++) // go through the possibilities
            {
                for (int y = 0; y < 3; y++)
                {
                    if (grid[x, y] == 0) // put a x in an empty box, see what happens
                    {
                        grid[x, y] = -1;
                        if (is_lose(grid))
                        {
                            done = true;
                            lose = true;
                            return;
                        }

                        grid[x, y] = 0;
                    }
                }
            }



            for (int x = 0; x < 3; x++) // go through the possibilities
            {
                for (int y = 0; y < 3; y++)
                {
                    if (grid[x, y] == 0) // put a x in an empty box, see what happens
                    {
                        grid[x, y] = 1;
                        
                        if (is_win(grid)) // if X in that position wins the game, put a O there.
                        {
                            grid[x, y] = -1;
                            return;
                        }
                        grid[x,y] = 0;
                    }
                }
            }

            for (int x = 0; x < 3; x++) // go through the possibilities
            {
                for (int y = 0; y < 3; y++)
                {
                    if (grid[x, y] == 0) // put a x in an empty box, see what happens
                    {
                        grid[x, y] = -1;
                        return;

                    }
                }
            }



        }
    }
    

}
