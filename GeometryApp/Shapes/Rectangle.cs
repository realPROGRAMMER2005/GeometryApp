using System;
using System.Drawing;

namespace GeometryApp.Shapes
{
    public class MyRectangle
    {
        private int x, y;
        private int width, height;
        private Color color;
        private bool filled;
        private Color borderColor;
        private int borderSize;
        private bool isBordered;

        public MyRectangle()
        {
            this.x = 0;
            this.y = 0;
            this.width = 100;
            this.height = 50;
            this.color = Color.Red;
            this.filled = false;
            this.borderColor = Color.Black;
            this.borderSize = 3;
            this.isBordered = false;
        }

        public void Create(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = Math.Abs(width);
            this.height = Math.Abs(height);
        }

        public int GetX() { return x; }
        public int GetY() { return y; }

        public int GetWidth() { return width; }
        public int GetHeight() { return height; }

        public Color GetBorderColor() { return borderColor; }
        public int GetBorderSize() { return borderSize; }
        public Color GetColor() { return color; }

        public bool IsFilled() { return filled; }

        public void SetWidth(int newWidth)
        {
            this.width = newWidth;
        }

        public void SetHeight(int newHeight)
        {
            this.height = newHeight;
        }

        public void Move(int newX, int newY)
        {
            this.x = newX;
            this.y = newY;
        }

        public void Rotate(int degrees)
        {
        }

        public void Fill()
        {
            this.filled = true;
        }

        public void SetBorder(int size, Color color)
        {
            this.borderSize = size;
            this.isBordered = true;
            this.borderColor = color;
        }

        public void Remove()
        {
        }

        public void Unfill()
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

        public void NoBorder()
        {
            this.borderSize = 0;
            this.isBordered = false;
            this.borderColor = Color.Transparent;
        }

        public bool hasBorder()
        {
            return isBordered;
        }

        public void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(color))
            {
                if (filled)
                {
                    g.FillRectangle(brush, x, y, width, height);
                }
            }

            using (Pen pen = new Pen(color, 2))
            {
                g.DrawRectangle(pen, x, y, width, height);
            }

            if (IsFilled() && isBordered && borderSize > 0 && borderColor != Color.Transparent)
            using (Pen pen = new Pen(borderColor, borderSize))
            {
                g.DrawRectangle(pen, x, y, width, height);
            }
        }
    }
}