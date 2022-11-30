using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.ImgHash;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;


namespace Brightness1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private Bitmap imgDefault = null;
        private Bitmap img = null;
        private Bitmap imgRed = null;
        private Bitmap imgGreen = null;
        private Bitmap imgBlue = null;

        private void OpenButton(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "bitmap (*.bmp)|*.bmp";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (imgDefault != null)
                        imgDefault.Dispose();
                    imgDefault = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName, false);
                }
                catch (Exception)
                {
                    //checkfilepicture
                    MessageBox.Show("CAN'T OPEN FILE", "FILE EROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            img = new Bitmap(imgDefault, Width, imgDefault.Height);
            for (int i = 0; i < imgDefault.Width; i++)
            {
                for (int j = 0; j < imgDefault.Height; j++)
                {
                    Color pixel = imgDefault.GetPixel(i,j);
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;
                    img.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }
            pictureBox1.Image = imgDefault;
            pictureBox2.Image = img;

            //call function
            redComponent();
            greenComponent();
            blueComponent();

        }

        // funtion red
        private void redComponent()
        {
            chart1.Series[0].Points.Clear();
            imgRed = new Bitmap(img.Width, img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;
                    imgRed.SetPixel(i, j, Color.FromArgb(r, 0, 0));
                }
            }
            pictureBox3.Image = imgRed;

            //show histogram
            int[] hist = new int[256];
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;
                    hist[r]++;

                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart1.ChartAreas[0].AxisX.Title = "gray Level";
                    chart1.ChartAreas[0].AxisY.Title = "No. of Pixels";
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.Series[0].Points.AddXY(r, hist[r]);
                }
            }
        }
        // funtion green
        private void greenComponent()
        {
            chart2.Series[0].Points.Clear();
            imgGreen = new Bitmap(img.Width, img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;
                    imgGreen.SetPixel(i,j, Color.FromArgb(0, g, 0));
                }
            }
            pictureBox4.Image = imgGreen;

            //show histogramgreen
            int[] hist = new int[256];
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;
                    hist[g]++;

                    chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart2.ChartAreas[0].AxisX.Title = "gray Level";
                    chart2.ChartAreas[0].AxisY.Title = "No. of Pixels";
                    chart2.ChartAreas[0].AxisX.Minimum = 0;
                    chart2.Series[0].Points.AddXY(g, hist[g]);
                }
            }
        }
        // funtion blue
        private void blueComponent()
        {
            chart3.Series[0].Points.Clear();
            imgBlue = new Bitmap(img.Width, img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;
                    imgBlue.SetPixel(i, j, Color.FromArgb(0, 0, b));
                }
            }
            pictureBox5.Image = imgBlue;

            //show histogramblue
            int[] hist = new int[256];
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;
                    hist[b]++;

                    chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart3.ChartAreas[0].AxisX.Title = "gray Level";
                    chart3.ChartAreas[0].AxisY.Title = "No. of Pixels";
                    chart3.ChartAreas[0].AxisX.Minimum = 0;
                    chart3.Series[0].Points.AddXY(b, hist[b]);
                }
            }
        }
        //input Truncate
        private int Truncate(int value)
        {
            if (value > 255)
            {
                return 255;
            }
            else if (value < 0)
            {
                return 0;
            }
            else
            {
                return value;
            }
        }
        //buttonBrightness
        private void BrightnessButton(object sender, EventArgs e)
        {
            img = new Bitmap(imgDefault.Width, imgDefault.Height);
            int luminosity = Convert.ToInt32(textBox1.Text);

            for (int i = 0; i < imgDefault.Width; i++)
            {
                for (int j = 0; j < imgDefault.Height; j++)
                {
                    Color pixel = imgDefault.GetPixel(i, j);
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;

                    r = Truncate(r + luminosity);
                    g = Truncate(g + luminosity);
                    b = Truncate(b + luminosity);

                    img.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }
            pictureBox2.Image = img;

            redComponent();
            greenComponent();
            blueComponent();
        }
     


        //button Negative
        private void negativeBtn(object sender, EventArgs e)
        {
            img = new Bitmap(imgDefault.Width, imgDefault.Height);
            for (int i = 0; i < imgDefault.Width; i++)
            {
                for (int j = 0; j < imgDefault.Height; j++)
                {
                    Color pixel = imgDefault.GetPixel(i, j);
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;

                    r = 255 - r;
                    g = 255 - g;
                    b = 255 - b;
                    img.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }
            pictureBox2.Image = img;

            redComponent();
            greenComponent();
            blueComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }


}
  

    
   


