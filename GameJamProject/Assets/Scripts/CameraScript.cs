using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	Transform camera;
	public bool started;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindWithTag("MainCamera").transform;
		AudioListener.volume = 1F;
		started = false;
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
		GameObject.FindWithTag ("Player").GetComponent<PlayerScript> ().healthBarSlider.enabled = false;
		if (!started) 
		{
			if (Input.GetKeyDown (KeyCode.Return)) 
			{
				started = true;
				camera.position = new Vector3(0,0, -10);
				GameObject.FindWithTag ("Player").GetComponent<PlayerScript> ().healthBarSlider.enabled = true;
				GameObject.FindWithTag ("Player").GetComponent<PlayerScript> ().started = true;
			}
		}
	}
}
