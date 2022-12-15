using UnityEngine;

public class RotateTowardsInputDirection : MonoBehaviour
{
    public void Update() =>
        transform.eulerAngles = Input.GetAxisRaw("Horizontal") switch
        {
            > 0 => new Vector2(0, 0),
            < 0 => new Vector2(0, 180),
            _ => transform.eulerAngles
        };
}
