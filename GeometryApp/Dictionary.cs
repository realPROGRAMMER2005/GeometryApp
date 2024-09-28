public class MyDictionary
{
  private dynamic[] _keys;
  private dynamic[] _values;
  private int _size;

  public MyDictionary()
  {
    _keys = new dynamic[4];
    _values = new dynamic[4];
    _size = 0;
  }

  public int Count
  {
    get { return _size; }
  }

  public dynamic this[dynamic key]
  {
    get
    {
      int index = Array.IndexOf(_keys, key, 0, _size);
      if (index < 0)
        throw new KeyNotFoundException($"Ключ '{key}' не найден.");
      return _values[index];
    }
    set
    {
      int index = Array.IndexOf(_keys, key, 0, _size);
      if (index >= 0)
      {
        _values[index] = value; // Изменяем существующее значение
      }
      else
      {
        // Добавляем новый ключ-значение
        if (_size == _keys.Length)
        {
          int newCapacity = _keys.Length * 2;
          dynamic[] newKeys = new dynamic[newCapacity];
          dynamic[] newValues = new dynamic[newCapacity];
          Array.Copy(_keys, newKeys, _keys.Length);
          Array.Copy(_values, newValues, _values.Length);
          _keys = newKeys;
          _values = newValues;
        }
        _keys[_size] = key;
        _values[_size] = value;
        _size++;
      }
    }
  }

  public dynamic GetElementByKey(dynamic key)
  {
    int index = Array.IndexOf(_keys, key, 0, _size);
    if (index < 0)
      throw new KeyNotFoundException($"Ключ '{key}' не найден.");
    return _values[index];
  }

  public dynamic[] GetKeys()
  {
    dynamic[] keysCopy = new dynamic[_size];
    Array.Copy(_keys, keysCopy, _size);
    return keysCopy;
  }

  public void Clear()
  {
    for (int i = 0; i < _size; i++)
    {
      _keys[i] = null;
      _values[i] = null;
    }
    _size = 0;
  }

  public void RemoveAt(dynamic key)
  {
    int index = Array.IndexOf(_keys, key, 0, _size);
    if (index < 0)
      throw new KeyNotFoundException($"Ключ '{key}' не найден.");

    int moveCount = _size - index - 1;
    if (moveCount > 0)
    {
      Array.Copy(_keys, index + 1, _keys, index, moveCount);
      Array.Copy(_values, index + 1, _values, index, moveCount);
    }
    _keys[_size - 1] = null;
    _values[_size - 1] = null;
    _size--;
  }

  public dynamic[] GetElements()
  {
    dynamic[] valuesCopy = new dynamic[_size];
    Array.Copy(_values, valuesCopy, _size);
    return valuesCopy;
  }
}
