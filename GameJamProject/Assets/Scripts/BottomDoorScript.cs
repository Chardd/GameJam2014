using UnityEngine;
using System.Collections;

public class BottomDoorScript : MonoBehaviour {
	public float transitionDuration = 2.5f;

	public Transform camera;
	public Transform player;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindWithTag("MainCamera").transform;
		player = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// If player collides with the door
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.tag == "Player"){
			/*Vector3 temp = camera.position;
			temp.y -= 12.725f;
			camera.position = temp;*/
			Debug.Log ("DOWN");

			StartCoroutine(Transition());

			Vector2 playerTemp = player.position;
			playerTemp.y -= 7.85f;
			player.position = playerTemp;
		}
	}

	IEnumerator Transition() { 
		float t = 0.0f; 
		Vector3 startingPos = camera.position; 
		Vector3 cameraTarget = camera.position;
		cameraTarget.y -= 12.725f;
		
		while (t < 1.0f) { 
			t += Time.deltaTime * (Time.timeScale/transitionDuration);
			camera.position = Vector3.Lerp(startingPos, cameraTarget, t);
			yield return 0;
		}
	}
}
