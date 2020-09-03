using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationGameForms
{
    public partial class Form1 : Form
    {

        private Graphics graphics;
        private int resolution;
        private Engine engine;

        private void StartG()
        {
            
            if (timer1.Enabled)
                return;

            resolution = (int)NumUpDwResolution.Value;

            engine = new Engine(rows: pictureBox1.Height / resolution, cols: pictureBox1.Width / resolution, density: (int)numDensity.Value);

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);

            timer1.Start();
            Text = $"Generation {engine.CurrentGen}";

        }

        public Form1()
        {
            InitializeComponent();

        }


        private void DrawGeneration()
        {
            graphics.Clear(Color.Black);

            var cells = engine.GetCells();
             
            for (int x = 0; x < cells.GetLength(0); x++)
                for (int y = 0; y < cells.GetLength(1); y++)
                    if (cells[x,y])
                    graphics.FillRectangle(Brushes.Blue, x * resolution, y * resolution, resolution-1, resolution-1);


            Text = $"Generation {engine.CurrentGen}";
            pictureBox1.Refresh();
        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
            DrawGeneration();
        }

        private void BStart_Click(object sender, EventArgs e)
        {
            StartG();
        }

        private void BStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
                return;

            if (e.Button == MouseButtons.Left)
                engine.AddCell(e.Location.X / resolution, e.Location.Y / resolution);



            //   && (MouseValidator(e.Location.X / resolution, e.Location.Y / resolution)))
            //    field[] = true;

            if (e.Button == MouseButtons.Right)
                engine.RemoveCell(e.Location.X / resolution, e.Location.Y / resolution);
                
             //   && (MouseValidator(e.Location.X / resolution, e.Location.Y / resolution)))
            //    field[e.Location.X / resolution, e.Location.Y / resolution] = false;
        }

    }
}
