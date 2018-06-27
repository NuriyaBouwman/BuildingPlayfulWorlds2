using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float xMin;
    [SerializeField]
    private float yMin;

    private Transform target;
    
    // Use this for initialization
	void Start () 
	{
		StartCoroutine(MoveCamera ());

        target = GameObject.Find("UnicornPlayer").transform;
	}

	private IEnumerator MoveCamera()
	{
		yield return new WaitForSeconds (.5f);

		while (transform.position.x < 15.0f) //15 is voor tot hoever de camera gaat. Moet later misschien meer worden.
		{
			transform.position += new Vector3(10.0f * Time.deltaTime, .0f, .0f);

			yield return 0;
		}

		yield return new WaitForSeconds (.5f);

		while (transform.position.x > .0f)
		{
			transform.position -= new Vector3(10.0f * Time.deltaTime, .0f, .0f);

			yield return 0;
		}

		LevelManager.Instance.state = GameState.Playing;
	}

	// Update is called once per frame
	void Update () 
	{
		if(LevelManager.Instance.state == GameState.CameraMoving) 
			{
        	return;
        	}

        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z); ;
	}
}
