using UnityEngine;

public class Chorao : MonoBehaviour
{
    public float speed;

    public float runningSpeed;

    public Vector2 direction;

    public bool IsMoving
    {
        get
        {
            return direction.sqrMagnitude > 0;
        }
    }

    public bool IsRunning
    {
        get
        {
            return doubleKeyDownDetector.HasBeenPressedTwice;
        }
    }

    private float CurrentSpeed
    {
        get
        {
            return IsRunning ? runningSpeed : speed;
        }
    }

    private Rigidbody2D rigbody;

    private DoubleKeyDownDetector doubleKeyDownDetector;

    private void Start()
    {
        rigbody = GetComponent<Rigidbody2D>();
        doubleKeyDownDetector = GetComponent<DoubleKeyDownDetector>();
    }

    private void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        rigbody.MovePosition(CurrentSpeed * Time.fixedDeltaTime * direction + rigbody.position);
    }
}
