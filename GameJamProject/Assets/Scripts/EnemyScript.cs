using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	// Player location
	public Transform target;

	// Change pursuit speed
	public float speed = .01f;
	// distance to trigger pursuit
	public float triggerDistance = 5.0f;
	// This enemy's position
	public Transform enemy;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// How close the individual enemy is the player
		//Debug.Log (Vector2.Distance(target.position, enemyPosition));

		// If they get within a given distance to the player
		if(Vector3.Distance(target.position, enemy.position) < triggerDistance ){
			enemy.position = Vector3.MoveTowards(enemy.position, target.position, speed * Time.deltaTime);
			Debug.Log("DETECT");
		}else{
			Debug.Log("LOITER");
		}

	
	}
}
