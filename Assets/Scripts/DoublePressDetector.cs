using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DoublePressDetector : MonoBehaviour
{
    public float doublePressInterval;

    public bool HasBeenPressedTwice { get; private set; }

    private readonly static KeyCode[] arrows = new[] { KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftArrow };

    private SizedList<(KeyCode keyCode, float keyDownAt)> pressedKeys = new(2);

    private void Update()
    {
        var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if (direction.sqrMagnitude == 0 && HasBeenPressedTwice)
        {
            HasBeenPressedTwice = false;
            return;
        }

        if (!Input.anyKeyDown) return;

        var keyDownCode = arrows.First(arrow => Input.GetKeyDown(arrow));

        var otherDirection = pressedKeys.Any() && pressedKeys.Any(x => x.keyCode != keyDownCode);

        if (otherDirection) pressedKeys.Clear();
        
        pressedKeys.Add((keyDownCode, Time.time));

        if (pressedKeys.Count() < 2) return;

        var first = pressedKeys[0];
        var second = pressedKeys[1];
        HasBeenPressedTwice = second.keyDownAt - first.keyDownAt < doublePressInterval;
    }
}

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
