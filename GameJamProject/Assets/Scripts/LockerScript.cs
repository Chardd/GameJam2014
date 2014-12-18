using UnityEngine;
using System.Collections;

public class LockerScript : MonoBehaviour {

    GameObject playerObj;
    bool isOpen = false;
    
    // Use this for initialization
    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");          
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    // If player collides with the door
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(isOpen == true){
            return;
        }
        if (collider.gameObject.tag == "Player")
        {
            isOpen = true;
            playerObj.GetComponent<PlayerScript>().OpenLocker();
            Debug.Log("OPEN");
        }
    }


}



