public class MyCircle
    {
        private int x, y;
        private int radius;
        private Color color;
        private bool filled;
        private Color borderColor;
        private int borderSize;
        private bool isBordered;

        public MyCircle()
        {
            this.x = 0;
            this.y = 0;
            this.radius = 50;
            this.color = Color.Red;
            this.filled = false;
            this.borderColor = Color.Transparent;
            this.borderSize = 0;
            this.isBordered = false;
        }

        public void Create(int x, int y, int radius)
        {
            this.x = x;
            this.y = y;
            this.radius = Math.Abs(radius);
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

        public bool hasBorder()
        {
            return isBordered;
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

        public bool IsFilled() { return filled; }

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

        public Color GetBorderColor()     { return borderColor; }
        public int GetBorderSize()      { return borderSize; }
        public Color GetColor()         { return color; }

        public int GetX() { return x; }
        public int GetY() { return y; }
        public int GetRadius() { return radius; }

        public void SetRadius(int radius) { this.radius = radius; }

        public void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(color))
            {
                if (filled)
                {
                    g.FillEllipse(brush, x - radius, y - radius, radius * 2, radius * 2);
                }
            }

            using (Pen pen = new Pen(color, 2))
            {
                g.DrawEllipse(pen, x - radius, y - radius, radius * 2, radius * 2);
            }

            if (IsFilled() && isBordered && borderSize > 0 && borderColor != Color.Transparent)
            {
                using (Pen pen = new Pen(borderColor, borderSize))
                {
                    g.DrawEllipse(pen, x - radius, y - radius, radius * 2, radius * 2);
                }
            }
        }
    }