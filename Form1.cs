namespace paint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
        }

        private bool mouse_pinch = false;

        private class array_point 
        {
            private int index = 0;
            private Point[] Points;

            public array_point(int size) 
            {
                if (size <= 0) size = 2;
                Points = new Point[size];
            }
            public void setPoint(int x, int y) 
            {
                if(index >= Points.Length) index = 0;
                Points[index] = new Point(x, y);
                index++;
            }
            public void resetPoint() 
            {
                index = 0;
            }
            public int getCountPoints() { return index; }

            public Point[] getPoints() { return Points; }
        }

        private array_point arrayPoints = new array_point(2);

        Bitmap map = new Bitmap(100, 100);

        Graphics graphics;

        Pen pen = new Pen(Color.Black, 3f);

        private void SetSize() 
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_pinch= true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_pinch= false;
            arrayPoints.resetPoint();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouse_pinch) { return; }
             
            arrayPoints.setPoint(e.X, e.Y);
            if (arrayPoints.getCountPoints() >= 2) {
                graphics.DrawLines(pen,arrayPoints.getPoints());
                pictureBox1.Image= map;
                arrayPoints.setPoint(e.X, e.Y);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK) 
            {
                pen.Color= colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = map;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                if (pictureBox1 != null) 
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                }
            }

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }
    }
}