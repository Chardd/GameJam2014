using UnityEngine;
using System.Collections;

public class LockerScript : MonoBehaviour {

    GameObject playerObj;
    bool isOpen = false;
    public Sprite openLocker;
    SpriteRenderer spriteRenderer;
    
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            OpenSprite();
            Debug.Log("OPEN");
        }
    }

    void OpenSprite()
    {
        spriteRenderer.sprite = openLocker;
        //gameObject.GetComponent<SpriteRenderer> ().sprite = openLocker;
    }
    

}



