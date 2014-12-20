using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	Transform camera;
	public bool started;
	public GameObject healthText;
	public GameObject healthSlider;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindWithTag("MainCamera").transform;
		AudioListener.volume = 1F;
		started = false;
		GameObject.FindWithTag ("Player").GetComponent<PlayerScript>().healthBarSlider.enabled = false;

		healthText = GameObject.FindWithTag("HealthText");
		healthSlider = GameObject.FindWithTag("HealthSlider");
		healthText.SetActive(false);
		healthSlider.SetActive(false);
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
		//GameObject.FindWithTag ("Player").GetComponent<PlayerScript>().healthBarSlider.enabled = false;
		if (!started) 
		{
			if (Input.GetKeyDown (KeyCode.Return)) 
			{
				started = true;
				camera.position = new Vector3(0,0, -10);
				healthSlider.SetActive(true);
				healthText.SetActive(true);
				GameObject.FindWithTag("HealthSlider").SetActive(true);
				GameObject.FindWithTag ("Player").GetComponent<PlayerScript>().healthBarSlider.enabled = true;
				GameObject.FindWithTag ("Player").GetComponent<PlayerScript>().started = true;
			}
		}
	}
}
