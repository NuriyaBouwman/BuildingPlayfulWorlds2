using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public AudioSource magicSource;

	// Use this for initialization
	void Start ()
    {
        magicSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Star")
        {
            magicSource.Play();
        }
    }
}
