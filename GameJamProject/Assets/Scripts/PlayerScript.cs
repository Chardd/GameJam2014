using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	//Variables used for Player sprite Animation
	private Rigidbody2D playerBody;
	private Animator anim;
	public float speed; //Movement Speed




	// Use this for initialization
	void Start () {
		playerBody = this.rigidbody2D;
		anim = GetComponent<Animator>();
		speed = 5;
	}
	
	// Update is called once per frame
	void Update () {
		float moveDirHor = Input.GetAxisRaw ("Horizontal"); //Get left/right input
		float moveDirVer = Input.GetAxisRaw ("Vertical"); //Get up/down input
		playerBody.velocity = new Vector2 (moveDirHor * speed, moveDirVer * speed); //Move player sprite
	}
}
