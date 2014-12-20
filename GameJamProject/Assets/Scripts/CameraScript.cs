using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	Transform camera;
	public bool started;
	private bool loaded;
	public GameObject canvas;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindWithTag("MainCamera").transform;
		AudioListener.volume = 1F;
		started = false;
		loaded = false;
	}
	
	// Update is called once per frame
	void Update () {
		StartScreen ();
	}
	public void MoveCamera(string direction){
		Vector3 temp = transform.position;
		switch(direction){
		case "up":
			temp.x += 12.7f;
			transform.position = temp;
			break;

		}
	}

	void StartScreen()
	{
		if (!loaded) 
		{
			canvas.SetActive (false);
			loaded = true;
		}
		if (!started) 
		{
			if (Input.GetKeyDown (KeyCode.Return)) 
			{
				started = true; 
				camera.position = new Vector3(0,0, -10);
				canvas.SetActive(true);
				GameObject.FindWithTag ("Player").GetComponent<PlayerScript> ().started = true;
			}
		}
	}
}
