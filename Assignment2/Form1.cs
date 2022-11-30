using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class Form1 : Form
    {
        private Bitmap f_image = null; 
        public Bitmap g_image; 
        private const int MinDepthDistance = 850;
        private const int MaxDepthDistanceOffset = 3150;
        public Form1()
        {
            InitializeComponent();
        }
        private void OpenBtn(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "bitmap (*.bmp)|*.bmp|(*.jpg)|*.jpg";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (f_image != null)
                        f_image.Dispose();
                    f_image = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName, false);
                }
                catch (Exception)
                {
                    MessageBox.Show("Can not open file", "File Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            g_image = new Bitmap(f_image.Width, f_image.Height);
            for (int i = 0; i < f_image.Width; i++)
            {
                for (int j = 0; j < f_image.Height; j++)
                {
                    Color PixelColor = f_image.GetPixel(i, j);
                    int C_gray = (int)(PixelColor.R + PixelColor.G + PixelColor.B) / 3;
                    g_image.SetPixel(i, j, Color.FromArgb(C_gray, C_gray, C_gray));
                }
            }
            //color and gray level
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = g_image;
            pictureBox3.BackColor = Color.FromArgb(207, 200, 235);
            pictureBox4.BackColor = Color.FromArgb(204, 229, 255);
            pictureBox5.BackColor = Color.FromArgb(0, 94, 255);
            pictureBox6.BackColor = Color.FromArgb(130, 255, 91);
            pictureBox7.BackColor = Color.FromArgb(91, 251, 255);
            pictureBox8.BackColor = Color.FromArgb(253, 116, 48);
            //Histogram
            chart1.Series[0].Points.Clear();
            int[] hist1 = new int[256];
            for (int i = 0; i < f_image.Width; i++)
            {
                for (int j = 0; j < f_image.Height; j++)
                {
                    Color PixelColor = f_image.GetPixel(i, j);
                    int C_gray = (int)(PixelColor.G);
                    hist1[C_gray]++;
                    chart1.Series[0].ChartType =
                    System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart1.ChartAreas[0].AxisX.Title = "Gray Level";
                    chart1.ChartAreas[0].AxisY.Title = "No. of Pixels";
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.Series[0].Points.AddXY(C_gray, hist1[C_gray]);
                }
            }
        }


        //intensity
        private void IntensityBtn(object sender, EventArgs e)
        {
            Bitmap intensitySlicing = new Bitmap(f_image.Width, f_image.Height);
            for (int i = 0; i < f_image.Width; i++)
            {
                for (int j = 0; j < f_image.Height; j++)
                {
                    Color PixelColor = f_image.GetPixel(i, j);
                    int C_gray = (int)(PixelColor.R + PixelColor.G + PixelColor.B) / 3;
                    if (C_gray <= Convert.ToInt32(textBox1.Text))
                        intensitySlicing.SetPixel(i, j, Color.FromArgb(207, 200, 235));
                    else if (C_gray <= Convert.ToInt32(textBox2.Text))
                        intensitySlicing.SetPixel(i, j, Color.FromArgb(204, 229, 255));
                    else if (C_gray <= Convert.ToInt32(textBox3.Text))
                        intensitySlicing.SetPixel(i, j, Color.FromArgb(0, 94, 255));
                    else if (C_gray <= Convert.ToInt32(textBox4.Text))
                        intensitySlicing.SetPixel(i, j, Color.FromArgb(130, 255, 91));
                    else if (C_gray <= Convert.ToInt32(textBox5.Text))
                        intensitySlicing.SetPixel(i, j, Color.FromArgb(91, 251, 255));
                    else if (C_gray <= Convert.ToInt32(textBox6.Text))
                        intensitySlicing.SetPixel(i, j, Color.FromArgb(253, 116, 48));
                }
            }
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = intensitySlicing;
            //output image intensitySlicing

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

