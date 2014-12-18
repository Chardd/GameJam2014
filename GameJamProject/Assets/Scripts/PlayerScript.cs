using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
    public bool weaponUpgraded = false; //Is weapon upgraded
    public bool hasKey = false; //Has player found the key
    public int lockerTotal; //total number of lockers on the 
    public int[] lockerContents;
    public int lockersOpened = 0; //How many have been opened



	// Use this for initialization
	void Start () {
		playerBody = this.rigidbody2D;
		anim = GetComponent<Animator>();
		speed = 5;
		directionTracker = 2;
        canAttack = true;
        weaponUpgraded = false;
        lockerTotal = CountLockers(); // set lockerTotal to number of lockers
        lockerContents = new int[lockerTotal]; //set size of array
        Debug.Log(lockerTotal);
        lockerContents = FillLockers(lockerContents);// fill lockerList with 2,1,and remaining zeros

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


    int CountLockers(){
        GameObject[] foundLockers;
        foundLockers = GameObject.FindGameObjectsWithTag("Locker");
        return foundLockers.Length;
    }


    // Fill a list with 0 except for 2(key) and 1(staff)
    int[] FillLockers(int[] arr){
        // make all the lockers empty
        for(int i = 0; i < arr.Length; i++){
            arr[i] = 0;
        }

        //drop in 
        bool isSame = true;
        int tempPos = Random.Range(0,arr.Length-1);
        //int tempPos = rand.Next(0, arr.Length);
        arr[tempPos] = 2; //drop in the key
        int upgradePos = Random.Range(0,arr.Length-1);
        while(isSame){
            if(tempPos != upgradePos){
                isSame = false;
                break;
            }
            upgradePos = Random.Range(0,arr.Length-1);
        }
        arr[upgradePos] = 1; //drop in the upgrade
        return arr;
    }

    public void OpenLocker(){
        int grabbed = lockerContents[lockersOpened];
        lockersOpened++;
        switch(grabbed){
            case 0:
                Debug.Log("EMPTY");
                break;
            case 1:
                weaponUpgraded = true;
                Debug.Log("GOT UPGRADE");
                break;
            case 2:
                hasKey = true;
                Debug.Log("GOT KEY");
                break;
        }

    }

}
