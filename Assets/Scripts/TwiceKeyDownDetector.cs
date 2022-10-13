using System.Linq;
using UnityEngine;

public class TwiceKeyDownDetector : MonoBehaviour
{
    public float doubleKeyDownInterval;

    public bool HasDetected { get; private set; }

    private readonly static KeyCode[] observeKeyCodes = new[] { KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftArrow };

    private KeyCode lastKeyDown;

    private float? lastKeyDownTime;

    private void Update()
    {
        if (ResetWhenIddle() || !Input.anyKeyDown) return;

        var keyDown = observeKeyCodes.FirstOrDefault(arrow => Input.GetKeyDown(arrow));

        if (keyDown == KeyCode.None) return;

        if (lastKeyDown == keyDown)
            HasDetected = Time.time - lastKeyDownTime < doubleKeyDownInterval;

        lastKeyDown = keyDown;
        lastKeyDownTime = Time.time;
    }

    private bool ResetWhenIddle()
    {
        var reset = IsIddle() && HasDetected;
        if (reset) HasDetected = false;
        return reset;
    }

    private static bool IsIddle()
    {
        var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        var iddle = direction.sqrMagnitude == 0;
        return iddle;
    }
}
