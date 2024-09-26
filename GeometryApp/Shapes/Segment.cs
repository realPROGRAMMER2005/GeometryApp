using System;
using System.Drawing;

namespace GeometryApp.Shapes
{
    public class Segment
    {
        private int x1, y1;
        private int x2, y2;
        private Color color;
        private bool filled;
        private Color borderColor;
        private int borderSize;

        private int width;

        public Segment()
        {
            this.x1 = 0;
            this.y1 = 0;
            this.x2 = 0;
            this.y2 = 0;
            this.color = Color.Black;
            this.filled = false;
            this.borderColor = Color.Transparent;
            this.borderSize = 0;
            this.width = 2;
        }

        public void Create(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        public void SetWidth(int newWidth)
        {
            this.width = newWidth;
        }

        public int GetWidth() {
            return this.width;
        }

        public void Move(int newX1, int newY1, int newX2, int newY2)
        {
            this.x1 = newX1;
            this.y1 = newY1;
            this.x2 = newX2;
            this.y2 = newY2;
        }

        public void Rotate(int degrees)
        {
            // Rotation logic can be implemented here
        }

        public void Fill()
        {
            this.filled = true;
        }

        public void Border(int size, int r, int g, int b)
        {
            this.borderSize = size;
            this.borderColor = Color.FromArgb(r, g, b);
        }

        public void Remove()
        {
            // Logic for removing the segment (if necessary)
        }

        public void NoFill()
        {
            this.filled = false;
            this.borderColor = Color.Transparent;
        }

        public void ChangeColor(Color newColor)
        {
            this.color = newColor;
        }

        public void ChangeBorderColor(Color newBorderColor)
        {
            this.borderColor = newBorderColor;
        }

        public Color GetColor() { return color; }

        public void NoBorder()
        {
            this.borderSize = 0;
            this.borderColor = Color.Transparent;
        }

        public void Draw(Graphics g)
        {
            Pen pen = new Pen(color, this.width);
            g.DrawLine(pen, x1, y1, x2, y2);
        }

        public int GetX1() { return x1; }
        public int GetY1() { return y1; }
        public int GetX2() { return x2; }
        public int GetY2() { return y2; }
    }
}