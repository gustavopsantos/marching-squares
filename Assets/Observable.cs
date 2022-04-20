using System;

public class Observable<T>
{
    public event Action<T, T> OnValueChanged;

    private T _value;
    public T Prev { get; private set; }

    public T Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (!_value.Equals(value))
            {
                Prev = _value;
                _value = value;
                OnValueChanged?.Invoke(Prev, _value);
            }
        }
    }

    public Observable(T value)
    {
        Prev = _value = value;
    }
}