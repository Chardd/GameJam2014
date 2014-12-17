using UnityEngine;
using System.Collections;

public class RightDoorScript : MonoBehaviour {
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
			temp.x += 19.1f;
			camera.position = temp;*/
			Debug.Log ("RIGHT");

			StartCoroutine(Transition());

			Vector2 playerTemp = player.position;
			playerTemp.x += 9.33f;
			player.position = playerTemp;
		}
	}
	IEnumerator Transition() { 
		float t = 0.0f; 
		Vector3 startingPos = camera.position; 
		Vector3 cameraTarget = camera.position;
		cameraTarget.x += 19.1f;
		
		while (t < 1.0f) { 
			t += Time.deltaTime * (Time.timeScale/transitionDuration);
			camera.position = Vector3.Lerp(startingPos, cameraTarget, t);
			yield return 0;
		}
	}
}