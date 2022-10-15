using System.Linq;
using UnityEngine;

public class TwiceArrowDownDetector : MonoBehaviour
{
    public float DoubleKeyDownInterval;

    public bool HasDetected { get; private set; }

    private readonly static KeyCode[] ArrowsKeyCodes = new[] { KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftArrow };

    private KeyCode LastKeyDown;

    private float? LastKeyDownTime;

    private void Update()
    {
        if (ResetWhenIddle() || !Input.anyKeyDown) return;

        var keyDown = ArrowsKeyCodes.FirstOrDefault(arrow => Input.GetKeyDown(arrow));

        if (keyDown == KeyCode.None) return;

        if (LastKeyDown == keyDown)
            HasDetected = Time.time - LastKeyDownTime < DoubleKeyDownInterval;

        LastKeyDown = keyDown;
        LastKeyDownTime = Time.time;
    }

    private bool ResetWhenIddle()
    {
        var shouldReset = IsIddle() && HasDetected;
        if (shouldReset) HasDetected = false;
        return shouldReset;
    }

    private static bool IsIddle()
    {
        var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        var iddle = direction.sqrMagnitude == 0;
        return iddle;
    }
}
