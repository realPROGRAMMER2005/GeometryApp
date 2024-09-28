public class MySquare
{
    private int x, y;
    private int side;
    private Color color;
    private bool filled;
    private Color borderColor;
    private int borderSize;
    private bool isBordered;

    public MySquare()
    {
        this.x = 0;
        this.y = 0;
        this.side = 100;
        this.color = Color.Red;
        this.filled = false;
        this.borderColor = Color.Black;
        this.borderSize = 3;
        this.isBordered = false;
    }

    public void Create(int x, int y, int side)
    {
        this.x = x;
        this.y = y;
        this.side = Math.Abs(side);
    }

    public void Move(int newX, int newY)
    {
        this.x = newX;
        this.y = newY;
    }

    public void Rotate(int degrees)
    {
    }

    public int GetX() { return x; }
    public int GetY() { return y; }

    public int GetWidth() { return side; }
    public int GetHeight() { return side; }

    public int GetSide() { return side; }

    public Color GetBorderColor() { return borderColor; }
    public int GetBorderSize() { return borderSize; }
    public Color GetColor() { return color; }

    public bool IsFilled() { return filled; }

    public void SetSide(int newSide)
    {
        this.side = newSide;
    }

    public void SetWidth(int newWidth)
    {
        this.side = newWidth;
    }

    public void SetHeight(int newHeight)
    {
        this.side = newHeight;
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
                g.FillRectangle(brush, x, y, side, side);
            }
        }

        using (Pen pen = new Pen(color, 2))
        {
            g.DrawRectangle(pen, x, y, side, side);
        }

        if (IsFilled() && isBordered && borderSize > 0 && borderColor != Color.Transparent)
        {
            using (Pen pen = new Pen(borderColor, borderSize))
            {
                g.DrawRectangle(pen, x, y, side, side);
            }
        }
    }
}
