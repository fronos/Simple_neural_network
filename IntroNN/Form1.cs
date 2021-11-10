using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace IntroNN
{
    public partial class Form1 : Form
    {
        Bitmap src;
        int[] redkeyp = new int[10];
        double[,] param = new double[10, 12];
        public Form1()
        {
            InitializeComponent();
            src = new Bitmap(System.IO.Directory.GetCurrentDirectory() + "/Numbers0-9.png");
            pictureBox1.Image = scaleimg(src, 20, 5, 10);
        }
        
       
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e) // Uploading weights for full classification
        {
            OpenFileDialog open = new OpenFileDialog();
            StreamReader ReadFile;
            listBox2.Items.Clear();
            if(open.ShowDialog() == DialogResult.OK) // Choose spreadsheet with weights
            {
                string path = open.FileName;
                ReadFile = new StreamReader(path);
                for(int count =0; count < 10; count++)
                {
                    string input = ReadFile.ReadLine();
                    listBox2.Items.Add(input);
                    string[] str = new string[11];
                    str = input.Split(' ');
                    for(int IOArr = 0; IOArr < 12; IOArr++)
                    {
                        if (str[IOArr] != null)
                        {
                            param[count, IOArr] = Convert.ToDouble(str[IOArr]);
                        }
                    }
                }
                ReadFile.Close();
            }

        }

        /// <summary>
        /// Scaling of image
        /// </summary>
        /// <param name="bmp"> Source image</param>
        /// <param name="width">Image's width</param>
        /// <param name="height"> Images's height</param>
        /// <param name="factor">Coefficient for scaling</param>
        /// <returns>Scaled image</returns>
        private Bitmap scaleimg( Bitmap bmp, int width, int height, int factor)
        {   Bitmap Nbmp;
            Nbmp = new Bitmap(width*factor,height*factor);
            Graphics NewImg = Graphics.FromImage(Nbmp);

             for(int x = 0; x < width; x++)
              {
                  for(int y = 0; y < height; y++)
                  {
                      if(bmp.GetPixel(x,y).R != 0)
                      {
                          NewImg.FillRectangle(new SolidBrush(Color.White), new Rectangle(x*factor, y*factor, factor,  factor));
                      }
                      else
                      {
                          NewImg.FillRectangle(new SolidBrush(Color.Black), new Rectangle(x*factor, y*factor, factor,  factor));
                      }
                  }
              }
            NewImg.Dispose();
            return Nbmp;
        }

        //Display one number from set {0 - 9}
        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int number = e.X / 10;
            switch (number)
            {
                case 0:
                    goto case 1;
                case 1:
                    display_number(0, 2);
                    break;
                case 2:
                    display_number(2, 4);
                    break;

                case 3:  goto case 2;
                case 4:
                    display_number(4, 6);
                    break;
                case 5:  goto case 4;
                case 6:
                    display_number(6, 8);
                    break;
                case 7:  goto case 6;
                case 8:
                    display_number(8, 10);
                    break;
                case 9: goto case 8;
                case 10:
                    display_number(10, 12);
                    break;
                case 11: goto case 10;
                case 12:
                    display_number(12, 14);
                    break;
                case 13: goto case 12;
                case 14:
                    display_number(14, 16);
                    break;
                case 15: goto case 14;
                case 16:
                    display_number(16, 18);
                    break;
                case 17: goto case 16;
                case 18:
                    display_number(18, 20);
                    break;
                case 19: goto case 18;
            }

        }

        /// <summary>
        /// Drawing single image from a set 0-9. All set equal 20px. Each number - 2 px.
        /// </summary>
        /// <param name="min">Start pixel of number</param>
        /// <param name="max">End pixel of number</param>
        void display_number(int min, int max)
        {
            listBox1.Items.Clear();
            int counter = 0;
            Bitmap Single;
            Single = new Bitmap(80, 200);
            Graphics SImm = Graphics.FromImage(Single);
            const int koefficient = 40;
            for (int y = 0; y < 5; y++) 
            {
                for (int x = min; x < max; x++)
                {
                    if (src.GetPixel(x, y).R != 0)
                    {
                        redkeyp[counter] = -1;
                        listBox1.Items.Add(-1);
                        SImm.FillRectangle(new SolidBrush(Color.White), new Rectangle((x - min) * koefficient, y * koefficient, koefficient, koefficient));
                    }
                    else
                    {
                        listBox1.Items.Add(1);
                        redkeyp[counter] = 1;
                        SImm.FillRectangle(new SolidBrush(Color.Black), new Rectangle((x - min) * koefficient, y * koefficient, koefficient, koefficient));
                    }
                    counter ++;
                }
            }
           
            pictureBox2.Image = Single;
            SImm.Dispose();
        }

        private void Button2_Click(object sender, EventArgs e)            ///Settings of weights
        {
            listBox2.Items.Clear();
            Random ran = new Random();
            StreamWriter sw = new StreamWriter("Params.csv");
            for(int row = 0; row < 10; row++)
            {
                for(int column = 0; column < 10; column++)
                {
                    param[row, column] = (double)(ran.Next(0, 10))/10.0;
                    sw.Write(param[row, column] +" " );
                }
                sw.WriteLine();
            }
            sw.Close();
            StreamReader sr = new StreamReader("Params.csv");
            for (int i =0; i < 10; i++)
            {
                listBox2.Items.Add(sr.ReadLine());
            }
            sr.Close();
        }

        private void Button3_Click(object sender, EventArgs e) // Forward pass of activated cells from image of number
        {
            double max_value = 0;
            int index = -1;
            listBox3.Items.Clear();

            for(int i = 0; i < 10; i++)//Multiplication of number's vector and weights' matrix
            {
                param[i, 10] = 0;
                for (int j =0; j < 10; j++)
                {
                    param[i, 10] += param[i, j] * (double)redkeyp[j];

                }
                listBox3.Items.Add(param[i, 10]); // Display value for each number

                if (param[i, 10] > max_value) // Find higher result and set as predicted number
                {
                    max_value = param[i, 10];
                    index = i;
                }
            }
            MessageBox.Show("Number: " + index);
        }

        private void Button4_Click(object sender, EventArgs e) //Sace weights from textbox
        {
            string filen = " ";
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                 filen = sfd.FileName;
            }
            StreamWriter newF = new StreamWriter(filen +".csv");
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 12; column++)
                {
                    newF.Write(param[row, column] + " ");
                }
                newF.WriteLine();
            }
            newF.Close();
        }
    }
}
