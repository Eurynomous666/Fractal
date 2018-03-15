using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractal
{
    public partial class Fractal : Form
    {
       
      
        private const int MAX = 256;      // max iterations
        private const double SX = -2.025; // start value real
        private const double SY = -1.125; // start value imaginary
        private const double EX = 0.6;    // end value real
        private const double EY = 1.125;  // end value imaginary
        private static int x1, y1, xs, ys, xe, ye;
        private static double xstart, ystart, xende, yende, xzoom, yzoom;
        private static Boolean action, rectangle, finished;
        private static float xy;
        //private Image picture;
        private Graphics g1;
        private Graphics g;
        private Bitmap picture;
        private Cursor c1, c2;


        private Pen pen;
        private Rectangle rect;
        private HSB HSBcol;


        private void stop()
        {
            pictureBox1.Image = null;
            pictureBox1.Invalidate();

        }


        public void InitializeForm() // all instances will be prepared
        {
            InitializeComponent();
            HSBcol = new HSB();
            
            this.pictureBox1.Size = new System.Drawing.Size(640, 480); //setSize(640, 480);
            finished = false;
            //addMouseListener(this);
            //addMouseMotionListener(this);
            c1 = Cursors.WaitCursor;
            c2 = Cursors.Cross;
            x1 = pictureBox1.Width;//300 value
            y1 = pictureBox1.Height;//300 value
            xy = (float)x1 / (float)y1;
            picture = new Bitmap(x1,y1);
            g1 = Graphics.FromImage(picture);
            finished = true;

            start();
        }


      /*  public void destroy() // delete all instances 
        {
            if (finished)
            {
                //removeMouseListener(this);
                //removeMouseMotionListener(this);
                picture = null;
                g1 = null;
                c1 = null;
                c2 = null;
                //System.gc(); // garbage collection
            }
        }*/

        public void start()
        {
            action = false;
            rectangle = false;
            initvalues();
            xzoom = (xende - xstart) / (double)x1;
            yzoom = (yende - ystart) / (double)y1;
            mandelbrot();
        }




        public void paint(Graphics g)
        {
            Update(g);
        } 

        public void Update(Graphics g)
        {
            Image tempPic = Image.FromHbitmap(picture.GetHbitmap());
            Graphics g1 = Graphics.FromImage(tempPic);

            if (rectangle)
            {
                Pen pen = new Pen(Color.White);

                Rectangle rect;

                if (xs < xe)
                {

                    if (ys < ye)
                    {
                        rect = new Rectangle(xs, ys, (xe - xs), (ye - ys));
                    }
                    else
                    {
                        rect = new Rectangle
                            (xs, ye, (xe - xs), (ys - ye));
                    }
                }
                else
                {
                    if (ys < ye)
                    {
                        rect = new Rectangle
                            (xe, ys, (xs - xe), (ye - ys));
                    }
                    else
                    {
                        rect = new Rectangle
                            (xe, ye, (xs - xe), (ys - ye));
                    }
                }
            }
        } 

     /*   public void update(Graphics g)
        {
            Pen p1 = new Pen(Color.White);
            g.DrawRectangle(p1, 0, 0, 0, 0);


            if (rectangle)
            {

                g.GetNearestColor (Color.White);
                if (xs < xe)
                {

                    if (ys < ye) g.DrawRectangles(xs, ys, (xe - xs), (ye - ys));
                    else g.DrawRectangle(xs, ye, (xe - xs), (ys - ye));
                }
                else
                {
                    if (ys < ye) g.DrawRectangle(xe, ys, (xs - xe), (ye - ys));
                    else g.DrawRectangle(xe, ye, (xs - xe), (ys - ye));
                }
            }
        }*/


        private void mandelbrot() // calculate all points
        {
            int x, y;
            float h, b, alt = 0.0f;
            Pen pen = new Pen(Color.White);

            action = false;
            //this.Cursor = c1; // in java setCursor(c1)
            pictureBox1.Cursor = c2;

            //showStatus("Mandelbrot-Set will be produced - please wait..."); will do later
            for (x = 0; x < x1; x += 2)
            {
                for (y = 0; y < y1; y++)
                {
                    h = pointcolour(xstart + xzoom * (double)x, ystart + yzoom * (double)y); // hue value

                    if (h != alt)
                    {
                        b = 1.0f - h * h; // brightness

                        HSBcol.fromHSB(h, 0.8f, b); //convert hsb to rgb then make a Java Color
                        Color col = Color.FromArgb(Convert.ToByte(HSBcol.rChan), Convert.ToByte(HSBcol.gChan), Convert.ToByte(HSBcol.bChan));

                        pen = new Pen(col);

                        //djm end
                        //djm added to convert to RGB from HSB

                        alt = h;
                    }
                    g1.DrawLine(pen, new Point(x, y), new Point(x + 1, y)); // drawing pixel
                }
                //showStatus("Mandelbrot-Set ready - please select zoom area with pressed mouse.");
                Cursor.Current = c1;
                action = true;
            }

            pictureBox1.Image = picture;
        }

        private float pointcolour(double xwert, double ywert) // color value from 0.0 to 1.0 by iterations
        {
            double r = 0.0, i = 0.0, m = 0.0;
            int j = 0;

            while ((j < MAX) && (m < 4.0))
            {
                j++;
                m = r * r - i * i;
                i = 2.0 * r * i + ywert;
                r = m + xwert;
            }
            return (float)j / (float)MAX;
        }

        private void initvalues() // reset start values
        {
            xstart = SX;
            ystart = SY;
            xende = EX;
            yende = EY;
            if ((float)((xende - xstart) / (yende - ystart)) != xy)
                xstart = xende - (yende - ystart) * (double)xy;
        }

        /*public void mousePressed(MouseEvent e)
        {
            e.consume();
            if (action)
            {
                xs = e.X();
                ys = e.Y();
            }
        }

        public void mouseReleased(MouseEvent e)
        {
            int z, w;

            e.consume();
            if (action)
            {
                xe = e.getX();
                ye = e.getY();
                if (xs > xe)
                {
                    z = xs;
                    xs = xe;
                    xe = z;
                }
                if (ys > ye)
                {
                    z = ys;
                    ys = ye;
                    ye = z;
                }
                w = (xe - xs);
                z = (ye - ys);
                if ((w < 2) && (z < 2)) initvalues();
                else
                {
                    if (((float)w > (float)z * xy)) ye = (int)((float)ys + (float)w / xy);
                    else xe = (int)((float)xs + (float)z * xy);
                    xende = xstart + xzoom * (double)xe;
                    yende = ystart + yzoom * (double)ye;
                    xstart += xzoom * (double)xs;
                    ystart += yzoom * (double)ys;
                }
                xzoom = (xende - xstart) / (double)x1;
                yzoom = (yende - ystart) / (double)y1;
                mandelbrot();
                rectangle = false;
                repaint();
            }
        }

        public void mouseEntered(MouseEvent e)
        {
        }

        public void mouseExited(MouseEvent e)
        {
        }

        public void mouseClicked(MouseEvent e)
        {
        }

        public void mouseDragged(MouseEvent e)
        {
            e.consume();
            if (action)
            {
                xe = e.getX();
                ye = e.getY();
                rectangle = true;
                //repaint();
                Refresh();
            }
        }

        public void mouseMoved(MouseEvent e)
        {
        }

        
      public String getAppletInfo()
      {
        return "fractal.class - Mandelbrot Set a Java Applet by Eckhard Roessel 2000-2001";
      }
         

    */

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
             
    }
    }
}

