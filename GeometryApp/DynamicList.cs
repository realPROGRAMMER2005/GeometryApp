using System;

public class MyPoint
{
    public void Move()
    {
        Console.WriteLine("Точка перемещена.");
    }
}

public class Segment
{
    public void Move()
    {
        Console.WriteLine("Сегмент перемещен.");
    }
}

public class MyRectangle
{
    public void Move()
    {
        Console.WriteLine("Прямоугольник перемещен.");
    }
}

public class MyEllipse
{
    public void Move()
    {
        Console.WriteLine("Эллипс перемещен.");
    }
}

public class MyCircle
{
    public void Move()
    {
        Console.WriteLine("Круг перемещен.");
    }
}

public class MyDynamicList
{
    private dynamic[] _items;
    private int _size;

    public MyDynamicList()
    {
        _items = new dynamic[4];
        _size = 0;
    }

    public int Count
    {
        get { return _size; }
    }

    public dynamic this[int index]
    {
        get
        {
            if (index < 0 || index >= _size)
                throw new ArgumentOutOfRangeException(nameof(index));
            return _items[index];
        }
        set
        {
            if (index < 0 || index >= _size)
                throw new ArgumentOutOfRangeException(nameof(index));
            _items[index] = value;
        }
    }

    public void Add(dynamic item)
    {
        if (_size == _items.Length)
        {
            int newCapacity = _items.Length * 2;
            dynamic[] newItems = new dynamic[newCapacity];
            Array.Copy(_items, newItems, _items.Length);
            _items = newItems;
        }
        _items[_size] = item;
        _size++;
    }

    public dynamic RemoveLast()
    {
        if (_size == 0)
            throw new InvalidOperationException("Список пуст.");
        _size--;
        dynamic item = _items[_size];
        _items[_size] = null;
        return item;
    }

    public void Clear()
    {
        for (int i = 0; i < _size; i++)
        {
            _items[i] = null;
        }
        _size = 0;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _size)
            throw new ArgumentOutOfRangeException(nameof(index));
        int moveCount = _size - index - 1;
        if (moveCount > 0)
        {
            Array.Copy(_items, index + 1, _items, index, moveCount);
        }
        _size--;
        _items[_size] = null;
    }
}

public class Program
{
    public static void Main()
    {
        MyDynamicList shapes = new MyDynamicList();
        shapes.Add(new MyPoint());
        shapes.Add(new Segment());
        shapes.Add(new MyRectangle());
        shapes.Add(new MyEllipse());
        shapes.Add(new MyCircle());

        // Вызываем метод Move() для каждой фигуры
        for (int i = 0; i < shapes.Count; i++)
        {
            shapes[i].Move();
        }

        // Удаляем последний элемент и выводим информацию
        dynamic lastShape = shapes.RemoveLast();
        Console.WriteLine("Последняя фигура удалена.");

        // Очищаем список
        shapes.Clear();
        Console.WriteLine("Список очищен.");

        // Добавляем элементы снова
        shapes.Add(new MyPoint());
        shapes.Add(new Segment());

        // Удаляем элемент по индексу
        shapes.RemoveAt(0);
        Console.WriteLine("Элемент с индексом 0 удален.");

        // Вызываем метод Move() для оставшихся фигур
        for (int i = 0; i < shapes.Count; i++)
        {
            shapes[i].Move();
        }
    }
}
