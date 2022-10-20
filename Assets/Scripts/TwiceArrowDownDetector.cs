using System.Linq;
using UnityEngine;

public class TwiceArrowDownDetector : MonoBehaviour
{
    private static readonly KeyCode[] Arrows = new[]
    {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.UpArrow,
        KeyCode.LeftArrow
    };

    public float DoubleKeyDownInterval;
    public bool HasDetected { get; private set; }

    private KeyCode LastKeyDown;
    private float? LastKeyDownTime;

    private void Update()
    {
        if (HasDetected && IsIddle()) HasDetected = false;

        if (HasDetected) return;

        var keyDown = Arrows.FirstOrDefault(arrow => Input.GetKeyDown(arrow));

        if (keyDown == default) return;

        if (LastKeyDown == keyDown)
            HasDetected = Time.time - LastKeyDownTime < DoubleKeyDownInterval;

        LastKeyDown = keyDown;
        LastKeyDownTime = Time.time;
    }

    private static bool IsIddle() =>
        Input.GetAxisRaw("Horizontal") == 0 &&
        Input.GetAxisRaw("Vertical") == 0;
}
