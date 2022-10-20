using UnityEngine;

public class RotateTowardsInputDirection : MonoBehaviour
{
    public void Update() => transform.eulerAngles = GetEulerAngles();

    private Vector2 GetEulerAngles()
    {
        var x = Input.GetAxisRaw("Horizontal");
        if (x  > 0) return new Vector2(0, 0);
        if (x < 0) return new Vector2(0, 180);
        return transform.eulerAngles;
    }
}
