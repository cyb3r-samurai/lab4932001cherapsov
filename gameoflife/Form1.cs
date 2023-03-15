using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameoflife
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        public Form1()
        {
            InitializeComponent();
        }


        private bool [,] currentField;
        const int k = 20;
        int rows, cols;
        int density;
        Random rand = new Random();
        private void Initlife()
        {
            rows = pictureBox1.Height / k;
            cols = pictureBox1.Width / k;
            currentField = new bool[rows,cols];
            density = (int)numericUpDown1.Value;
            for(int i= 0;i  < rows; i++) {
                for(int j = 0;j < cols;j++)
                {
                    if (rand.Next(100) < density)
                    {
                        currentField[i,j] = true;     
                    }
                    else
                        currentField[i,j]= false;
                }  
            }
            pictureBox1.Image = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
        }

        private int countNeigbours(int row, int col)
        {
            int count = 0;
            for(int i = row -1; i<row+2;i++)
            {
                for(int j = col-1;j<col+2;j++)
                {
                    if( (i >=0)&&(j>=0) && (i< rows) && (j<cols) && (i!=row || j!= col))
                    {
                        if (currentField[i, j]) count++;
                    }
                }
            }

            return count; 
        }

        private void Gen()
        {
            graphics.Clear(Color.Black);
            var nextField = new bool[rows,cols];
            for(int i = 0; i < rows;i++) 
            {   
                for(int j = 0; j< cols;j++)
                {

                    var neighboursCount = countNeigbours(i,j);
                    if (currentField[i, j] && (neighboursCount <2 || neighboursCount>3))
                    {
                        nextField[i,j] = false  ;
                    }
                    else if (neighboursCount == 3 && !currentField[i,j]) {
                        nextField[i,j] = true;
                    }

                    else
                    {
                        nextField[i,j] = currentField[i,j];
                    }
                    if (currentField[i, j])
                    {
                        graphics.FillRectangle(Brushes.Crimson, k * j, k * i, k, k);
                    }
                }
            }
            currentField = nextField;
            pictureBox1.Refresh();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Gen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Initlife();

        }
    }
}
