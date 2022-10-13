using System.Linq;
using UnityEngine;

public class DoubleKeyDownDetector : MonoBehaviour
{
    public float doublePressInterval;

    public bool HasBeenPressedTwice { get; private set; }

    private readonly static KeyCode[] observeKeyCodes = new[] { KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftArrow };

    private KeyCode lastKeyDown;

    private float? lastKeyDownTime;

    private void Update()
    {
        if (ResetWhenIddle() || !Input.anyKeyDown) return;

        var keyDown = observeKeyCodes.First(arrow => Input.GetKeyDown(arrow));

        if (keyDown == KeyCode.None) return;

        if (lastKeyDown == keyDown)
            HasBeenPressedTwice = Time.time - lastKeyDownTime < doublePressInterval;

        lastKeyDown = keyDown;
        lastKeyDownTime = Time.time;
    }

    private bool ResetWhenIddle()
    {
        var reset = IsIddle() && HasBeenPressedTwice;
        if (reset) HasBeenPressedTwice = false;
        return reset;
    }

    private static bool IsIddle()
    {
        var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        var iddle = direction.sqrMagnitude == 0;
        return iddle;
    }
}
