using System.Collections.Generic;

public class SizedList<T> : List<T>
{
    private readonly int KeepLastN;

    public SizedList(int keepLastN) : base()
    {
        KeepLastN = keepLastN;
    }

    public new void Add(T item)
    {
        base.Add(item);
        if (Count > KeepLastN) RemoveAt(0);
    }
}
