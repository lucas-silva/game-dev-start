using UnityEngine;

public class Chorao : MonoBehaviour
{
    public float Speed;
    public float RunningSpeed;
    public bool IsRolling { get; private set; }
    public bool IsMoving { get => Direction.sqrMagnitude > 0; }
    public bool IsWalking { get => IsMoving && !IsRunning; }
    public bool IsRunning { get => TwiceArrowDownDetector.HasDetected; }

    private float CurrentSpeed { get => IsRunning ? RunningSpeed : Speed; }
    private Vector2 Direction;
    private Rigidbody2D Rigbody;
    private TwiceArrowDownDetector TwiceArrowDownDetector;

    private void Start()
    {
        Rigbody = GetComponent<Rigidbody2D>();
        TwiceArrowDownDetector = GetComponent<TwiceArrowDownDetector>();
    }

    private void Update()
    {
        IsRolling = Input.GetKeyUp(KeyCode.LeftShift);
        Direction = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    }

    private void FixedUpdate() => Rigbody.MovePosition(
        Time.fixedDeltaTime * CurrentSpeed * Direction + Rigbody.position 
    );
}
