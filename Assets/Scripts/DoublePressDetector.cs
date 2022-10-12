using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using System;

public class DoublePressDetector : MonoBehaviour
{
    public float doublePressInterval;

    public bool HasBeenPressedTwice { get; private set; }

    private readonly static KeyCode[] arrows = new[] { KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftArrow };

    private List<(KeyCode keyCode, float keyDownAt)> lastKeyDown = new List<(KeyCode keyCode, float keyDownAt)>();

    private void Update()
    {
        var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if (direction.sqrMagnitude == 0 && HasBeenPressedTwice)
        {
            HasBeenPressedTwice = false;
            return;
        }

        if (!Input.anyKeyDown) return;

        var keyDownCode = arrows.Single(arrow => Input.GetKeyDown(arrow));

        var otherDirection = lastKeyDown.Any() && lastKeyDown.Any(x => x.keyCode != keyDownCode);

        if (otherDirection) lastKeyDown.Clear();
        
        lastKeyDown.Add((keyDownCode, Time.time));

        if (lastKeyDown.Count() == 3) lastKeyDown.RemoveAt(0);

        if (lastKeyDown.Count() < 2) return;

        var lastCalls = lastKeyDown.TakeLast(2).ToList();
        var first = lastCalls[0];
        var second = lastCalls[1];
        HasBeenPressedTwice = second.keyDownAt - first.keyDownAt < doublePressInterval;
    }
}
