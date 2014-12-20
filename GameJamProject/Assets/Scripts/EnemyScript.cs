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
	// This enemy's stunned status
	public bool stunned = false;
	public bool recovering = false;
	// Used for sprite animation transition
	private Animator anim;
	private float vert;
	private float horz;


	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag("Player").transform;
		anim = GetComponent<Animator>();
		vert = enemy.position.y;
		horz = enemy.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (stunned);
		if (!stunned) 
		{
			// How close the individual enemy is the player
			//Debug.Log (Vector2.Distance(target.position, enemyPosition));

			// If they get within a given distance to the player
			if (Vector3.Distance (target.position, enemy.position) < triggerDistance) {
					enemy.position = Vector3.MoveTowards (enemy.position, target.position, speed * Time.deltaTime);
					//Debug.Log("DETECT");
				//Animation handling maths
				float curVert = enemy.position.y;
				float curHorz = enemy.position.x;
				//float diffVert = vert - curVert;
				//float diffHorz = horz - curHorz;
				//Debug.Log (diffVert);
				//Debug.Log (diffHorz);
				if (curVert == vert)
				{
					anim.SetFloat ("Vertical", 0);
				}
				if (curVert > vert)
				{
					anim.SetFloat ("Vertical", 1);
				}
				if (curVert < vert)
				{
					anim.SetFloat ("Vertical", -1);
				}
				if (curHorz == horz)
				{
					anim.SetFloat ("Horizontal", 0);
				}
				if (curHorz > horz)
				{
					anim.SetFloat ("Horizontal", 1);
				}
				if (curHorz < horz)
				{
					anim.SetFloat ("Horizontal", -1);
				}
				vert = curVert;
				horz = curHorz;
			} else {
					//Debug.Log("LOITER");
			}
		} 
		else if (!recovering)
		{
			recovering = true;
			anim.SetBool ("Stunned", true);
			StartCoroutine(Recovering ());
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
