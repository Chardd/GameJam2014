using UnityEngine;
using System.Collections;

public class ItemStaff : MonoBehaviour {
	public PlayerScript script;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D otherObject)
	{ //Handler for hitting something
		if (otherObject.tag == "Player") 
		{
			GameObject Player = GameObject.FindGameObjectWithTag ("Player"); //Find player Object
			Player.GetComponent<PlayerScript>().weaponUpgraded = true;
			Destroy (gameObject);
		}
	}
}
