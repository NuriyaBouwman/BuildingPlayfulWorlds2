using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    [SerializeField]
    public float startingTime;

    private Text theText;

    public GameObject GameOverScreen;

    public UnicornPlayer player;

	// Use this for initialization
	void Start ()
    {
        theText = GetComponent<Text>();
        player = FindObjectOfType<UnicornPlayer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        startingTime -= Time.deltaTime;

        if (startingTime <= 0)
        {
            GameOverScreen.SetActive(true);
            player.gameObject.SetActive(false);
        }
        theText.text = "" + Mathf.Round (startingTime);
	}
}
