using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMode : MonoBehaviour {

	public string selected;
	public string selectedWeapon;

	// Use this for initialization
	void Start () {
		LoadPlayerPrefs ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void setMode(string mode) {
		selected = mode;
	}

	void LoadPlayerPrefs() {
		GameObject.Find ("Highscore").GetComponent<Text> ().text = "High Scores:\nPistol - " + PlayerPrefs.GetInt ("Gun1") + "\nRifle - " + PlayerPrefs.GetInt ("Gun2") + "\nLaser - " + PlayerPrefs.GetInt ("Gun3") + "\nBow - " + PlayerPrefs.GetInt ("Bow") + "\nKnife - " + PlayerPrefs.GetInt ("Knife") + "\nBall - " + PlayerPrefs.GetInt ("Ball");
	}
}
