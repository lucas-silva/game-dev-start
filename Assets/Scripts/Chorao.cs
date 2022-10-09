using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chorao : MonoBehaviour
{
    public float speed;

    public Vector2 direction;

    private Rigidbody2D rigbody;

    private void Start()
    {
        rigbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        rigbody.MovePosition(direction * speed * Time.fixedDeltaTime + rigbody.position);
    }
}
