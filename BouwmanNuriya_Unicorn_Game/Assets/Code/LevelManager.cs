using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	CameraMoving,
	Playing
}

public class LevelManager : MonoBehaviour 
{
	private static LevelManager instance;
	public static LevelManager Instance
	{
		get 
		{
			return instance;
		}
	}

	public GameObject obstaclePrefab;

	private List<GameObject> obstacles;

	public GameState state = GameState.CameraMoving;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		obstacles = new List<GameObject> ();

		for(int i = 0; i < 5; ++i)
		{
			GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(i, 2, 0), Quaternion.identity);

			obstacles.Add (obstacle); 
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
