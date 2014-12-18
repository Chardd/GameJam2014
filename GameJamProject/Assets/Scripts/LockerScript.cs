using UnityEngine;
using System.Collections;

public class LockerScript : MonoBehaviour {

    public float transitionDuration = 2.5f;
    public Transform player;
    GameObject playerObj;
    bool isOpen = false;
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerObj = GameObject.FindWithTag("Player");          
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    // If player collides with the door
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(isOpen == false){
            return;
        }
        //bool playerInTransition = playerObj.GetComponent<PlayerScript>().GetInTransition();

        if (collider.gameObject.tag == "Player")
        {
            isOpen = true;


            playerObj.GetComponent<PlayerScript>().SetInTransition(true);
            Debug.Log("OPEN");
        }
    }


}



