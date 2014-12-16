using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public Transform camPosition;
	public Transform playerPosition;
	Vector3 door = new Vector3(5,0,0);
	Vector3 camPos2 = new Vector3(13,0,-10);
	// Use this for initialization
	void Start () {
		playerPosition = GameObject.FindWithTag("Player").transform;
		camPosition = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerPosition.position.x > 5){
			camPosition.position = camPos2;
		}
	}
}
