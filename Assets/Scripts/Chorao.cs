using UnityEngine;

public class Chorao : MonoBehaviour
{
    public float Speed;

    public float RunningSpeed;

    public Vector2 Direction { get; private set; }

    public bool IsRolling { get; private set; }

    public bool IsMoving { get => Direction.sqrMagnitude > 0; }

    public bool IsRunning { get => TwiceArrowDownDetector.HasDetected; }

    private float CurrentSpeed { get => IsRunning ? RunningSpeed : Speed; }

    private Rigidbody2D Rigbody;

    private TwiceArrowDownDetector TwiceArrowDownDetector;

    private void Start()
    {
        Rigbody = GetComponent<Rigidbody2D>();
        TwiceArrowDownDetector = GetComponent<TwiceArrowDownDetector>();
    }

    private void Update()
    {
        Direction = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        IsRolling = Input.GetKeyUp(KeyCode.LeftShift);
    }

    private void FixedUpdate() => Rigbody.MovePosition(CurrentSpeed * Time.fixedDeltaTime * Direction + Rigbody.position);
}
