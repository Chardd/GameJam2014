using UnityEngine;
using System.Collections;

public class RightDoorScript : MonoBehaviour {
	public float transitionDuration = 2.5f; 
	
	public Transform camera;
	public Transform player;
    GameObject playerObj;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindWithTag("MainCamera").transform;
		player = GameObject.FindWithTag("Player").transform;
        playerObj = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// If player collides with the door
	void OnTriggerEnter2D(Collider2D collider){
        bool playerInTransition = playerObj.GetComponent<PlayerScript>().GetInTransition();
        if (playerInTransition == true)
        {
            return;
        }
		if (collider.gameObject.tag == "Player"){
            playerObj.GetComponent<PlayerScript>().SetInTransition(true);
			/*Vector3 temp = camera.position;
			temp.x += 19.1f;
			camera.position = temp;*/
			Debug.Log ("RIGHT");

			StartCoroutine(Transition());

			/*
            Vector2 playerTemp = player.position;
			playerTemp.x += 9.33f;
			player.position = playerTemp;*/
		}
	}
	IEnumerator Transition() { 
		float t = 0.0f; 
        //camera target
		Vector3 startingPos = camera.position; 
		Vector3 cameraTarget = camera.position;
		cameraTarget.x += 19.1f;
        //player target
        // player target
        Vector2 playerPos = player.position;
        Vector2 playerTarget = player.position;
        playerTarget.x += 9.33f;


		while (t < 1.0f) { 
			t += Time.deltaTime * (Time.timeScale/transitionDuration);
			camera.position = Vector3.Lerp(startingPos, cameraTarget, t);
            player.position = Vector2.Lerp(playerPos, playerTarget, t);
			yield return 0;
		}
        playerObj.GetComponent<PlayerScript>().SetInTransition(false);
	}
}