using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DoublePressDetector : MonoBehaviour
{
    public float doublePressInterval;

    public bool HasBeenPressedTwice { get; private set; }

    private readonly static KeyCode[] observeKeyCodes = new[] { KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftArrow };

    private readonly SizedList<(KeyCode keyCode, float keyDownAt)> pressed = new(2);

    private void Update()
    {
        var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        var iddle = direction.sqrMagnitude == 0;

        if (iddle && HasBeenPressedTwice)
        {
            HasBeenPressedTwice = false;
            return;
        }

        if (!Input.anyKeyDown) return;

        var keyDown = observeKeyCodes.First(arrow => Input.GetKeyDown(arrow));

        var otherDirection = pressed.Any() && pressed.Any(x => x.keyCode != keyDown);
        if (otherDirection) pressed.Clear();
        
        pressed.Add((keyDown, Time.time));

        if (pressed.Count() < 2) return;

        var first = pressed[0];
        var second = pressed[1];
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
