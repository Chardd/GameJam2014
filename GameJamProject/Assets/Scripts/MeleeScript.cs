using UnityEngine;
using System.Collections;

public class MeleeScript : MonoBehaviour {

	private GameObject Player; //Object used for player relativity
	private Vector3 direction; //Used for correct facing when fired.
	private Transform projectileTransform;
	private Animator anim;
	private float playerDirection;
	
	// Use this for initialization
	void Start () {
		projectileTransform = transform; //This forms a quick reference to the objects Transform
		anim = GetComponent<Animator>(); //for animating the sprite
		Player = GameObject.FindGameObjectWithTag ("Player"); //Find player Object
	}
	
	// Update is called once per frame
	void Update () 
	{
		StartCoroutine (Die()); //Time to live
	}
	
	void OnTriggerEnter2D(Collider2D otherObject)
	{ //Handler for hitting something
		if (otherObject.tag == "Enemy") 
		{
			Destroy (otherObject.gameObject);
		}
	}
	
	IEnumerator Die()
	{
		//anim.SetBool ("expend", true); //Change sprite to fading one
		yield return new WaitForSeconds (0.2f); //duration of attack
		Destroy (gameObject); //Delete the object
	}
}
