using UnityEngine;
using System.Collections;

public class StrongEnemyScript : MonoBehaviour {

	// Player location
	public Transform target;
	// Change pursuit speed
	public float speed = 1.0f;
	// distance to trigger pursuit
	public float triggerDistance = 10.0f;
	// This enemy's position
	public Transform enemy;
	// Used for sprite animation transition
	private Animator anim;
	
	
	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag("Player").transform;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		// How close the individual enemy is the player
		//Debug.Log (Vector2.Distance(target.position, enemyPosition));
		
		// If they get within a given distance to the player
		if (Vector3.Distance (target.position, enemy.position) < triggerDistance) {
			enemy.position = Vector3.MoveTowards (enemy.position, target.position, speed * Time.deltaTime);
			//Debug.Log("DETECT");
		} else {
			//Debug.Log("LOITER");
		}
	}
}
