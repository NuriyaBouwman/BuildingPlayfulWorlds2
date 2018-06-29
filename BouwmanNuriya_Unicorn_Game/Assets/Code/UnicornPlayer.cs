using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornPlayer : MonoBehaviour {

    private Animator unicornAnimator;

    [SerializeField]
    private Transform arrowPos;

    [SerializeField]
    private float movementSpeed;
    private bool facingRight;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;
   

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private GameObject arrowPrefab;

    public Rigidbody2D UnicornRigidbody { get; set; }

    public bool Jump { get; set; }
    public bool OnGround { get; set; }
    public bool Throw { get; set; }

    private static UnicornPlayer instance;
    public static UnicornPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<UnicornPlayer>();
            }
            return instance;
        }
    }

    // Use this for initialization
    void Start ()
    {
        facingRight = true;
        UnicornRigidbody = GetComponent<Rigidbody2D>();
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

        OnGround = IsGrounded();

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleLayers();
	}

    private void HandleMovement(float horizontal)
    {
       if (UnicornRigidbody.velocity.y < 0)
        {
            unicornAnimator.SetBool("land", true);
        }
       if (!Throw && (OnGround || airControl))
        {
            UnicornRigidbody.velocity = new Vector2(horizontal * movementSpeed, UnicornRigidbody.velocity.y);
        }
       if (Jump && UnicornRigidbody.velocity.y == 0)
        {
            UnicornRigidbody.AddForce(new Vector2(0, jumpForce));
        }

        unicornAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            unicornAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            unicornAnimator.SetTrigger("throw");
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

    private bool IsGrounded()
    {
        if (UnicornRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if(!OnGround)
        {
            unicornAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            unicornAnimator.SetLayerWeight(1, 0);
        }
    }

    public void ThrowArrow(int value)
    {

        if (OnGround && value == 0)
        {
            if (facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(arrowPrefab, arrowPos.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                tmp.GetComponent<Arrow>().Initialize(Vector2.right);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(arrowPrefab, arrowPos.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                tmp.GetComponent<Arrow>().Initialize(Vector2.left);

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Star")
        {
            GameManager.Instance.CollectedStars++;
            Destroy(other.gameObject);
        }
    }
}
