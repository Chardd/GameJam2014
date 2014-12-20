using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	// Player location
	public Transform target;

	// Change pursuit speed
	public float speed = .03f;
	// distance to trigger pursuit
	public float triggerDistance = 5.0f;
	// This enemy's position
	public Transform enemy;
	// This enemy's stunned status
	public bool stunned = false;
	public bool recovering = false;
	// Used for sprite animation transition
	private Animator anim;
	private float vert;
	private float horz;
	public bool started = false;


	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag("Player").transform;
		anim = GetComponent<Animator>();
		vert = enemy.position.y;
		horz = enemy.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindWithTag ("Player").GetComponent<PlayerScript> ().started == true)
		{
			//Debug.Log (stunned);
			if (!stunned) {
					// How close the individual enemy is the player
					//Debug.Log (Vector2.Distance(target.position, enemyPosition));

					// If they get within a given distance to the player
					if (Vector3.Distance (target.position, enemy.position) < triggerDistance) {
							enemy.position = Vector3.MoveTowards (enemy.position, target.position, speed * Time.deltaTime);
							//Debug.Log("DETECT");
							//Animation handling maths
							float curVert = enemy.position.y;
							float curHorz = enemy.position.x;
							float diffVert = Vector3.Distance (new Vector3 (0, vert, 0), new Vector3 (0, curVert, 0));
							float diffHorz = Vector3.Distance (new Vector3 (0, horz, 0), new Vector3 (0, curHorz, 0));
							Debug.Log ("Stat");
							Debug.Log (diffVert);
							Debug.Log (diffHorz);

							if (diffHorz > diffVert) {
									anim.SetFloat ("Vertical", 0);
									if (curHorz < horz) {
											anim.SetFloat ("Horizontal", -1);
									} else if (curHorz > horz) {
											anim.SetFloat ("Horizontal", 1);
									} else {
											anim.SetFloat ("Horizontal", 0);
									}
							} else {
									anim.SetFloat ("Horizontal", 0);
									if (curVert < vert) {
											anim.SetFloat ("Vertical", -1);
									} else if (curVert > vert) {
											anim.SetFloat ("Vertical", 1);
									} else {
											anim.SetFloat ("Vertical", 0);
									}
							}

							vert = curVert;
							horz = curHorz;
					} else {
							//Debug.Log("LOITER");
							anim.SetFloat ("Horizontal", 0);
							anim.SetFloat ("Vertical", 0);
					}
			} else if (!recovering) {
					recovering = true;
					anim.SetBool ("Stunned", true);
					anim.SetFloat ("Horizontal", 0);
					anim.SetFloat ("Vertical", 0);
					StartCoroutine (Recovering ());

			}
		}
	}

	IEnumerator Recovering()
	{
		yield return new WaitForSeconds (1f); //duration of attack
		stunned = false;
		recovering = false;
		anim.SetBool ("Stunned", false);
	}
}
