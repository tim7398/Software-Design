using System;

public class Location
{

    private int x;
    private int y;
    private bool draw_q;
    private bool color;

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

    public bool Draw_Q
    {
        get
        {
            return draw_q;
        }
        set
        {
            draw_q = value;
        }
    }

    public bool Color
    {
        get
        {
            return color;
        }
        set
        {
            color = value;
        }
    }
}
