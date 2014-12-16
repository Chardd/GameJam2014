using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	//Variables used for Player sprite Animation
	private Rigidbody2D playerBody;
	private Animator anim;
	public float speed; //Movement Speed

	//Test
	float somethingNew;
	string somethingElse;


	// Use this for initialization
	void Start () {
		playerBody = this.rigidbody2D;
		anim = GetComponent<Animator>();
		speed = 5;
	}
	
	// Update is called once per frame
	void Update () {
		float moveDirHor = Input.GetAxisRaw ("Horizontal"); // Get Left/Right movement
		float moveDirVer = Input.GetAxisRaw ("Vertical"); //Get Up/Down movement
		playerBody.velocity = new Vector2 (moveDirHor * speed, moveDirVer * speed);
	}
}
