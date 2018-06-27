using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornPlayer : MonoBehaviour {

    private Rigidbody2D unicornRigidbody;
    private Animator unicornAnimator;

    [SerializeField]
    private float movementSpeed;
    private bool facingRight;

    private bool isGrounded;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;
    
    private bool jump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;


	// Use this for initialization
	void Start ()
    {
        facingRight = true;
        unicornRigidbody = GetComponent<Rigidbody2D>();
        unicornAnimator = GetComponent<Animator>();
	}

    void Update()
    {
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleLayers();

        ResetValues();
	}

    private void HandleMovement(float horizontal)
    {
        if (unicornRigidbody.velocity.y < 0)
        {
            unicornAnimator.SetBool("land", true);
        }
        if (isGrounded || airControl)
        {
            unicornRigidbody.velocity = new Vector2(horizontal * movementSpeed, unicornRigidbody.velocity.y);
        }
        if (isGrounded && jump)
        {
            isGrounded = false;
            unicornRigidbody.AddForce(new Vector2(0, jumpForce));
            unicornAnimator.SetTrigger("jump");
        }
        

        unicornAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
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

    private void ResetValues()
    {
        jump = false;
    }

    private bool IsGrounded()
    {
        if (unicornRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        unicornAnimator.ResetTrigger("jump");
                        unicornAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if(!isGrounded)
        {
            unicornAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            unicornAnimator.SetLayerWeight(1, 0);
        }
    }
}
