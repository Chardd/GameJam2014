using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    public Slider healthBarSlider;  //reference for slider
    public Text gameOverText;   //reference for text
    private bool isGameOver = false; //flag to see if game is over
	public bool isAttacking = false; //is the player attacking
	public SpriteRenderer keyDisplay;
	public SpriteRenderer upgradeDisplay;
	public AudioClip deathSound;
	public AudioClip fireSound;
	public AudioClip hurtSound;
	public AudioClip keySound;
	public AudioClip lockerSound;
	public AudioClip meleeSound;
	public AudioClip upgradeSound;
	GameObject camera;
	public bool started = false;


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
        
        lockerContents = FillLockers(lockerContents);// fill lockerList with 2,1,and remaining zeros

        gameOverText.enabled = false; //disable GameOver text on start
		keyDisplay = GameObject.FindWithTag("Key").GetComponent<SpriteRenderer>();
		upgradeDisplay = GameObject.FindWithTag("Upgrade").GetComponent<SpriteRenderer>();
		camera = GameObject.FindWithTag("MainCamera");
	}

	//here's an added comment.

	// Update is called once per frame
	void Update () {
		if (started) 
		{
			float moveDirHor = Input.GetAxisRaw ("Horizontal"); //Get left/right input
			float moveDirVer = Input.GetAxisRaw ("Vertical"); //Get up/down input
			anim.SetFloat ("VerticalMovement", moveDirVer);//Set animation variables
			anim.SetFloat ("HorizontalMovement", moveDirHor);
			playerBody.velocity = new Vector2 (moveDirHor * speed, moveDirVer * speed); //Move player sprite
			HandleAttack ();
		}

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
			StartCoroutine(AttackBool());
            if (weaponUpgraded)
            { //Actually fire the projectile
                Instantiate(ProjectilePrefab, transform.position, ProjectileRotation);
				audio.PlayOneShot(fireSound,1f);
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
				audio.PlayOneShot(meleeSound,1f);
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
				audio.PlayOneShot(lockerSound,1f);
                break;
            case 1:
                weaponUpgraded = true;
                Debug.Log("GOT UPGRADE");
				upgradeDisplay.enabled = true;
				audio.PlayOneShot(upgradeSound,1f);
                break;
            case 2:
                hasKey = true;
                Debug.Log("GOT KEY");
				keyDisplay.enabled = true;
				audio.PlayOneShot(keySound,1f);
                break;
        }
    }
    //Check if player enters/stays on the fire
    void OnTriggerEnter2D(Collider2D collider){
        //if player triggers fire object and health is greater than 0
		if(isAttacking){
			return;
		}
        if(collider.gameObject.name=="Enemy"){
            if(healthBarSlider.value>0){
                healthBarSlider.value -=.1f;  //reduce health
				audio.PlayOneShot(hurtSound,1f);
            }
            else{
                isGameOver = true;    //set game over to true
                gameOverText.enabled = true;//enable GameOver text
				audio.PlayOneShot(deathSound,1f);
				camera.audio.enabled = false;
				gameObject.collider2D.enabled = false;
				gameObject.renderer.enabled = false;
				StartCoroutine(DeathReload());
                //Destroy(gameObject);
            }
        } 
    }
	IEnumerator AttackBool()
	{
		isAttacking = true;
		yield return new WaitForSeconds(1f);
		isAttacking = false;
	}
	IEnumerator DeathReload()
	{

		yield return new WaitForSeconds(3f);
		Application.LoadLevel (0);

	}

}
