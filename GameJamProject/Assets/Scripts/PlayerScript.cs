using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	//Variables used for Player sprite Animation
	private Rigidbody2D playerBody;
	private Animator anim;
	public float speed; //Movement Speed
	public float directionTracker; //Last direction moved 
	public GameObject ProjectilePrefab; //Projectile game object
	public static bool inTransition = false;
    public GameObject MeleePrefab; //Melee game object
    public bool canAttack;
    public bool weaponUpgraded;
    //test
    public bool tool;
    public bool ghool;


	// Use this for initialization
	void Start () {
		playerBody = this.rigidbody2D;
		anim = GetComponent<Animator>();
		speed = 5;
		directionTracker = 2;
        canAttack = true;
        weaponUpgraded = false;
	}

	//here's an added comment.

	// Update is called once per frame
	void Update () {
		float moveDirHor = Input.GetAxisRaw ("Horizontal"); //Get left/right input
		float moveDirVer = Input.GetAxisRaw ("Vertical"); //Get up/down input
		playerBody.velocity = new Vector2 (moveDirHor * speed, moveDirVer * speed); //Move player sprite
        HandleAttack ();

	}

    //Method handles both attacks, close and ranged
    void HandleAttack()
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

        //Set attack to face the right way
        Quaternion ProjectileRotation = Quaternion.Euler(0, 0, 0); //Quaterion handles rotation
        switch ((int)directionTracker)
        {
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
        } //Instantiates object using prefab, player position and generated quaternion
        
        //Checks for user input, currently Spacebar
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
            if (weaponUpgraded)
            { //Actually fire the projectile
                Instantiate(ProjectilePrefab, transform.position, ProjectileRotation);
            }
            else
            { //If no upgraded, use close range
                
                GameObject weapontest = (GameObject) Instantiate(MeleePrefab);
                weapontest.transform.parent = transform;
                switch ((int)directionTracker)
                {
				case 1: //up
                        weapontest.transform.localPosition = new Vector3(0,1,0);
					break;
				case 2: //right
                        weapontest.transform.localPosition = new Vector3(1,0,0);
					break;
				case 3: //down
                        weapontest.transform.localPosition = new Vector3(0,-1,0);
					break;
				case 4: //left
                        weapontest.transform.localPosition = new Vector3(-1,0,0);
					break;

                }
                weapontest.transform.rotation = ProjectileRotation;
            }
		}

	}
    public bool GetInTransition(){
        return inTransition;
    }
    public void SetInTransition( bool inBool){
        inTransition = inBool;
    }
}
