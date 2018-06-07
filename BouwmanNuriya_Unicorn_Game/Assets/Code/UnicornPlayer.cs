using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornPlayer : MonoBehaviour {

    private Rigidbody2D unicornRigidbody;

    [SerializeField]
    private float movementSpeed;

    private bool facingRight;

	// Use this for initialization
	void Start ()
    {
        facingRight = true;
        unicornRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);

        Flip(horizontal);
	}

    private void HandleMovement(float horizontal)
    {
        unicornRigidbody.velocity = new Vector2(horizontal * movementSpeed, unicornRigidbody.velocity.y);

    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }
}
