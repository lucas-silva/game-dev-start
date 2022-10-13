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
            return twiceKeyDownDetector.HasDetected;
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

    private TwiceKeyDownDetector twiceKeyDownDetector;

    private void Start()
    {
        rigbody = GetComponent<Rigidbody2D>();
        twiceKeyDownDetector = GetComponent<TwiceKeyDownDetector>();
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
