using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Arrow : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody2D arrowRigidbody;

    private Vector2 direction;

	// Use this for initialization
	void Start ()
    {
        arrowRigidbody = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        arrowRigidbody.velocity = direction * speed;
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
