using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	private bool black;
	private float blackTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (black) {
			blackTimer += Time.deltaTime;
			if (blackTimer > 2.0f) {
				SceneManager.LoadScene (1);
				Destroy (GameObject.Find ("[VRTK]"));
			}
		}
	}

	public void StartMain() {
		GameObject.Find ("Black").GetComponent<Animation> ().Play ();
		GameObject.Find ("Orb").GetComponent<Animator> ().SetBool ("Fade", true);
		black = true;
	}
}
