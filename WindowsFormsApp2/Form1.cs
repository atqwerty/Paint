using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        bool startPaint = false;
        Graphics g;
        //nullable int for storing Null value
        int? initX = null;
        int? initY = null;
        bool drawSquare = false;
        bool drawRectangle = false;
        bool drawCircle = false;
        bool eraser = false;
        int mX, mY;
        private Rectangle rect;

        public Form1()
        {
            InitializeComponent();
            g = g = panel1.CreateGraphics();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clearing the graphics from the Panel(pnl_Draw)
            g.Clear(panel1.BackColor);
            //Setting the BackColor of pnl_draw and btn_CanvasColor to White on Clicking New under File Menu
            panel1.BackColor = Color.White;
            //btn_CanvasColor.BackColor = Color.White;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPaint && !eraser)
            {
                //Setting the Pen BackColor and line Width
                Pen p = new Pen(button1.BackColor, float.Parse(comboBox1.Text));
                //Drawing the line.
                g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                initX = e.X;
                initY = e.Y;
            }
            else if(startPaint && eraser)
            {
                //Setting the Pen BackColor and line Width
                Pen p = new Pen(Color.White, float.Parse(comboBox3.Text));
                //Drawing the line.
                g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                initX = e.X;
                initY = e.Y;
            }
            else if (drawRectangle)
            {
                if (mX < e.X)
                {
                    rect.X = mX;
                    rect.Width = e.X - mX;
                }
                else
                {
                    rect.X = e.X;
                    rect.Width = mX - e.X;
                }
                if (mY < e.Y)
                {
                    rect.Y = mY;
                    rect.Height = e.Y - mY;
                }
                else
                {
                    rect.Y = e.Y;
                    rect.Height = mY - e.Y;
                }
                Refresh();
                Pen p = new Pen(button1.BackColor, float.Parse(comboBox1.Text));
                g.DrawRectangle(p, rect);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Open Color Dialog and Set BackColor of btn_PenColor if user click on OK
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = c.Color;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            startPaint = false;
            initX = null;
            initY = null;
            drawRectangle = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            drawSquare = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            drawRectangle = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            drawCircle = true;
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            startPaint = true;
            if (drawSquare)
            {
                //Use Solid Brush for filling the graphic shapes
                SolidBrush sb = new SolidBrush(button1.BackColor);
                //setting the width and height same for creating square.
                //Getting the width and Heigt value from Textbox(txt_ShapeSize)
                g.FillRectangle(sb, e.X, e.Y, int.Parse(comboBox2.Text), int.Parse(comboBox2.Text));
                //setting startPaint and drawSquare value to false for creating one graphic on one click.
                startPaint = false;
                drawSquare = false;
            }
            if (drawRectangle)
            {
                mX = e.X; mY = e.Y;
                rect.Location = new Point(mX, mY);
                //int a = e.X;
                //int b = e.Y;
                //Pen p = new Pen(Color.White, 1);
                //SolidBrush sb = new SolidBrush(button1.BackColor);
                ////setting the width twice of the height
                //g.DrawRectangle(p, e.X, e.Y, e.X, e.Y);
                //g.FillRectangle(sb, e.X, e.Y, 2 * int.Parse(comboBox2.Text), int.Parse(comboBox2.Text));
                //startPaint = false;
                //drawRectangle = false;
            }
            if (drawCircle)
            {
                SolidBrush sb = new SolidBrush(button1.BackColor);
                g.FillEllipse(sb, e.X, e.Y, int.Parse(comboBox2.Text), int.Parse(comboBox2.Text));
                startPaint = false;
                drawCircle = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!eraser)
            {
                button5.Text = "Pen";
                eraser = true;
            }
            else
            {
                button5.Text = "Eraser";
                eraser = false;
            }
        }
    }
}
