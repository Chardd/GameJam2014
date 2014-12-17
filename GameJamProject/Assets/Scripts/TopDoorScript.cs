using UnityEngine;
using System.Collections;

public class TopDoorScript : MonoBehaviour {


	public Transform camera;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindWithTag("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// If player collides with the door
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.tag == "Player"){
			Vector3 temp = camera.position;
			temp.y += 12.725f;
			camera.position = temp;
			Debug.Log ("UP");
			//camera.MoveCamera("up");
		}
	}
}
