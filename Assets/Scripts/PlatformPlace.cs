using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformPlace : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col) {
		if (GameObject.Find ("Manager").GetComponent<HandController> ().currentWeapon.Equals ("") && !col.gameObject.name.Equals("CustomHandCollider")) {
			if (col.gameObject.name.Equals ("Gun1")) {
				
			} else if (col.gameObject.name.Equals ("Gun2")) {

			} else if (col.gameObject.name.Equals ("Gun3")) {

			} else if (col.gameObject.name.Equals ("Ball")) {

			} else if (col.gameObject.name.Equals ("Bow")) {

			} else if (col.gameObject.name.Equals ("Knife")) {

			}

			GameObject.Find ("Mode").GetComponent<SelectMode> ().selectedWeapon = col.gameObject.name;
			DontDestroyOnLoad (GameObject.Find("Mode"));
			//DontDestroyOnLoad (GameObject.Find("Manager").gameObject);
			SceneManager.LoadScene ("Level");
		}
	}
}
