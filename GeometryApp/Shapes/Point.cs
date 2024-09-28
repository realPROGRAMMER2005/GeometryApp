public class MyPoint
{
    private int x;
    private int y;
    private Color color;
    private bool filled;
    private Color borderColor;
    private int borderSize;

    // Конструктор
    public MyPoint()
    {
        // Установим дефолтные параметры
        this.x = 0;
        this.y = 0;
        this.color = Color.Black;
        this.filled = false;
        this.borderColor = Color.Transparent;
        this.borderSize = 0;
    }

    // Метод создания точки
    public void Create(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    // Метод перемещения точки
    public void Move(int newX, int newY)
    {
        this.x = newX;
        this.y = newY;
    }

    // Метод вращения точки (для точки этот метод не имеет смысла, но должен быть)
    public void Rotate(int degrees)
    {
        // Поскольку точка не имеет размеров, вращение не изменяет её
    }

    // Метод закрашивания
    public void Fill()
    {
        this.filled = true;
    }

    // Метод создания обводки
    public void Border(int size, int r, int g, int b)
    {
        this.borderSize = size;
        this.borderColor = Color.FromArgb(r, g, b);
    }

    // Метод удаления фигуры
    public void Remove()
    {
        // Здесь можно добавить логику для удаления точки (если нужно)
    }



    // Метод изменения цвета
    public void ChangeColor(Color newColor)
    {
        this.color = newColor;
    }

    // Метод изменения цвета обводки
    public void ChangeBorderColor(Color newBorderColor)
    {
        this.borderColor = newBorderColor;
    }

    // Метод удаления обводки
    public void NoBorder()
    {
        this.borderSize = 0;
        this.borderColor = Color.Transparent;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    // Метод отрисовки точки
    public void Draw(Graphics g)
    {
        // Рисуем точку
        Brush brush = new SolidBrush(color);
        g.FillEllipse(brush, x - 2, y - 2, 4, 4); // Рисуем точку как маленький круг

        if (borderSize > 0 && borderColor != Color.Transparent)
        {
            Pen pen = new Pen(borderColor, borderSize);
            g.DrawEllipse(pen, x - 2, y - 2, 4, 4); // Обводка вокруг точки
        }
    }
}
