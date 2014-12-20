using UnityEngine;
using System.Collections;

public class LockedDoorScript : MonoBehaviour {
	public float transitionDuration = 2.5f; 
	
	public Transform camera;
	public Transform player;
	public GameObject canvas;
	GameObject playerObj;
	public bool win;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindWithTag("MainCamera").transform;
		player = GameObject.FindWithTag("Player").transform;
		playerObj = GameObject.FindWithTag("Player");
		win = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (win) 
		{
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				Application.LoadLevel (0);
			}
		}
	}
	
	// If player collides with the door
	void OnTriggerEnter2D(Collider2D collider){

		if (collider.gameObject.tag == "Player"){
			if(playerObj.GetComponent<PlayerScript>().hasKey == true)
			{
				playerObj.GetComponent<PlayerScript>().started = false;
				playerObj.GetComponent<PlayerScript>().keyDisplay.enabled = false;
				playerObj.GetComponent<PlayerScript>().upgradeDisplay.enabled = false;
				canvas.SetActive(false);
				camera.position = new Vector3(58.63f,25.48f,-10.0f);
				win = true;

			}
		}
	}
}
