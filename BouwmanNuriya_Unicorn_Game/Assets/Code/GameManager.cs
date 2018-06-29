using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    [SerializeField]
    private GameObject starPrefab;

    [SerializeField]
    private Text starTxt;

    private int collectedStars;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public int CollectedStars
    {
        get
        {
            return collectedStars;
        }

        set
        {
            starTxt.text = value.ToString();
            collectedStars = value;
        }
    }


    // Use this for initialization
    void Start ()
    { 		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
