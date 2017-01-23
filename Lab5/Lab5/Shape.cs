using System;
using System.Drawing;
public class Shape
{
    public virtual void Draw_Shape(Graphics g)
    {

    }
}

public class Line : Shape
{
  
    private Point first_location, second_location;
    private Pen pen;
    public Line(Pen pen,Point first_location, Point second_location)
    {
        this.first_location = first_location;
        this.second_location = second_location;
        this.pen = pen;

    }

   public override void Draw_Shape(Graphics g)
    {
        g.DrawLine(pen, first_location, second_location);
    }
}

public class Rec_shape :Shape
{
    private Point first_location, second_location;
    private Pen pen;
    Brush fill_brush;
    private bool outline, fill;
    private int x, y,start_x, start_y;
    public Rec_shape(Pen pen,Brush fill_brush, Point first_location, Point second_location, bool outline,bool fill )
    {
        this.first_location = first_location;
        this.second_location = second_location;
        this.pen = pen; // for outline
        this.fill_brush = fill_brush; // for fill
        this.fill = fill;
        this.outline = outline;
    }

    public override void Draw_Shape(Graphics g)
    {
        start_x = first_location.X;
        start_y = first_location.Y;
       
        if(first_location.X-second_location.X>0) // makes sure it always starts drawing at the top left corner
        {
            start_x = second_location.X;
            
        }
        if(first_location.Y - second_location.Y > 0) // makes sure to start drawing at top left corner
        {
            start_y = second_location.Y;
        }
        x = Math.Abs(first_location.X - second_location.X); // width of the rectangle
        y = Math.Abs(first_location.Y - second_location.Y); // height of rectangle

        if(fill) // check if need to fill
        {
            g.FillRectangle(fill_brush, start_x, start_y, x, y);
        }
        if(outline)// check if need to outline
        {
            g.DrawRectangle(pen, start_x, start_y, x, y);
        }
    }


    
}

public class Ellipse:Shape
{
    private Point first_location, second_location;
    private Pen pen;
    Brush fill_brush;
    private bool outline, fill;
    private int x, y, start_x, start_y;

    public Ellipse(Pen pen, Brush fill_brush, Point first_location, Point second_location, bool outline, bool fill)
    {
        this.first_location = first_location;
        this.second_location = second_location;
        this.pen = pen; // for outline
        this.fill_brush = fill_brush; // for fill
        this.fill = fill;
        this.outline = outline;
    }

    public override void Draw_Shape(Graphics g)
    {
        start_x = first_location.X;
        start_y = first_location.Y;

        if (first_location.X - second_location.X > 0) // makes sure it always starts drawing at the top left corner
        {
            start_x = second_location.X;

        }
        if (first_location.Y - second_location.Y > 0) // makes sure to start drawing at top left corner
        {
            start_y = second_location.Y;
        }
        x = Math.Abs(first_location.X - second_location.X); // width of the rectangle
        y = Math.Abs(first_location.Y - second_location.Y); // height of rectangle

        if (fill) // check if need to fill
        {
            g.FillEllipse(fill_brush, start_x, start_y, x, y);
        }
        if (outline)// check if need to outline
        {
            g.DrawEllipse(pen, start_x, start_y, x, y);
        }
    }
}

public class text:Shape
{
    private Brush brush;
    private Point first_location, second_location;
    private int start_x, start_y,x,y;
    private string message;
    public text(Brush brush, Point first_location, Point second_location, string message)
    {
        this.brush = brush;
        this.first_location = first_location;
        this.second_location = second_location;
        this.message = message;
    }

    public override void Draw_Shape(Graphics g)
    {
        start_x = first_location.X;
        start_y = first_location.Y;
        Font myfont = new Font("arial",9);

        if (first_location.X - second_location.X > 0) // makes sure it always starts drawing at the top left corner
        {
            start_x = second_location.X;

        }
        if (first_location.Y - second_location.Y > 0) // makes sure to start drawing at top left corner
        {
            start_y = second_location.Y;
        }
        x = Math.Abs(first_location.X - second_location.X); // width of the rectangle
        y = Math.Abs(first_location.Y - second_location.Y); // height of rectangle

        RectangleF box= new RectangleF(start_x, start_y, x, y);
        g.DrawString(message, myfont, brush, box);
    }
}