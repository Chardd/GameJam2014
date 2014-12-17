using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
	public float projectileSpeed; //Projectile's speed, DUH!
	private GameObject Player; //Object used for player relativity
	private Vector3 direction; //Used for correct facing when fired.
	private Transform projectileTransform;
	private bool halted;
	private Animator anim;
	private float playerDirection;

	// Use this for initialization
	void Start () {
		projectileTransform = transform; //This forms a quick reference to the objects Transform
		halted = false; //starts in flight
		anim = GetComponent<Animator>(); //for animating the sprite

		Player = GameObject.FindGameObjectWithTag ("Player"); //Find player Object
		direction = Vector3.right;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!halted) { //If the projective has hit something
			float move = projectileSpeed * Time.deltaTime; //Calculated speed
			projectileTransform.Translate (direction * move); //Move the projectile
		} 
	}

	void OnTriggerEnter2D(Collider2D otherObject)
	{ //Handler for hitting something
		if (otherObject.tag == "Obstacle" || otherObject.tag == "Enemy") 
		{
			StartCoroutine (Die ()); //Start to explode
		}
	}

	IEnumerator Die()
	{
		halted = true; //Stop the projectile from moving
		//anim.SetBool ("explode", true); //Change sprite to exploding one
		yield return new WaitForSeconds (0.3f); //Let it explode for a time
		Destroy (gameObject); //Delete the object
	}

}
