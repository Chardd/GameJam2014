using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	Transform camera;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindWithTag("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {

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
}
