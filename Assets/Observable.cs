public class Observable<T>
{
    private T _value;
    public T Prev { get; private set; }

    public T Value
    {
        get { return _value; }
        set
        {
            if (!_value.Equals(value))
            {
                Prev = _value;
                _value = value;
            }
        }
    }

    public Observable(T value)
    {
        Prev = _value = value;
    }
}