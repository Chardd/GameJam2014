using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	//Variables used for Player sprite Animation
	private Rigidbody2D playerBody;
	private Animator anim;
	public float speed; //Movement Speed
	public float directionTracker; //Last direction moved 
	public GameObject ProjectilePrefab; //Projectile game object

	// Use this for initialization
	void Start () {
		playerBody = this.rigidbody2D;
		anim = GetComponent<Animator>();
		speed = 5;
		directionTracker = 2;
	}

	// Update is called once per frame
	void Update () {
		float moveDirHor = Input.GetAxisRaw ("Horizontal"); //Get left/right input
		float moveDirVer = Input.GetAxisRaw ("Vertical"); //Get up/down input
		playerBody.velocity = new Vector2 (moveDirHor * speed, moveDirVer * speed); //Move player sprite
		HandleProjectile ();

	}

	//Method takes care of all things missle
	void HandleProjectile()
	{ //First figure out what direction the player is facing
		if (Input.GetAxisRaw ("Horizontal") == -1) //Left
		{
			directionTracker = 4;
		}
		else if (Input.GetAxisRaw ("Horizontal") == 1) //Right
		{
			directionTracker = 2;
		}
		else if (Input.GetAxisRaw ("Vertical") == 1) //Up
		{
			directionTracker = 1;
		}
		else if (Input.GetAxisRaw ("Vertical") == -1) //Down
		{
			directionTracker = 3;
		}

		//Actually fire the projectile
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			Quaternion ProjectileRotation = Quaternion.Euler(0, 0, 0);
			switch ((int)directionTracker)
			{ //Rotates the projectile before letting it loose
				case 1: //up
					ProjectileRotation = Quaternion.Euler(0, 0, 90);
					break;
				case 2: //right
					ProjectileRotation = Quaternion.Euler(0, 0, 0);
					break;
				case 3: //down
					ProjectileRotation = Quaternion.Euler(0, 0, 270);
					break;
				case 4: //left
					ProjectileRotation = Quaternion.Euler(0, 0, 180);
					break;

			} //Instantiates 
			Instantiate(ProjectilePrefab, transform.position, ProjectileRotation);
		}

	}
}
