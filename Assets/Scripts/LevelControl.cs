using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour {

	public string selected;
	public string selectedWeapon;
	public GameObject gun1Prefab;
	public GameObject gun2Prefab;
	public GameObject gun3Prefab;
	public GameObject bowPrefab;
	public GameObject ballPrefab;
	public GameObject knifePrefab;
	public GameObject targetPrefab;
	public GameObject tableTop;

	private bool readyTog;
	private float readyTimer;

	private bool timerTog;
	private float timerTimer = 20f;

	public int score;

	// Use this for initialization
	void Start () {
		readyTog = true;
		selected = GameObject.Find("Mode").GetComponent<SelectMode> ().selected;
		GameObject.Find ("Title").GetComponent<Text> ().text = "- " + selected + " -";
		//GameObject.Find ("OVRCameraRig").transform.position = new Vector3 (0.23f, 0.718f, -1.78f);


		selectedWeapon = GameObject.Find("Mode").GetComponent<SelectMode> ().selectedWeapon;
		GameObject weap = null;
		if (selectedWeapon.Equals ("Gun1")) {
			weap = GameObject.Find ("Gun1");
		} else if (selectedWeapon.Equals ("Gun2")) {
			weap = GameObject.Find ("Gun2");
		} else if (selectedWeapon.Equals ("Gun3")) {
			weap = GameObject.Find ("Gun3");
		} else if (selectedWeapon.Equals ("Bow")) {
			weap = GameObject.Find ("Bow");
		} else if (selectedWeapon.Equals ("Ball")) {
			weap = GameObject.Find ("Ball");
		} else if (selectedWeapon.Equals ("Knife")) {
			weap = GameObject.Find ("Knife");
		} 

		weap.transform.SetParent (null);
		GameObject.Find ("Weapons").SetActive (false);
	
		weap.transform.position = tableTop.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (readyTog) {
			readyTimer += Time.deltaTime;
			if (readyTimer > 5.0f) {
				int x = Random.Range (-9, -4);
				int y = Random.Range (1, 7);
				int z = Random.Range (0, 10);
				Vector3 loc = new Vector3 (x, y, z);

				var newTarget = (GameObject)Instantiate (targetPrefab, loc, targetPrefab.transform.rotation, GameObject.Find ("Targets").transform);
				timerTog = true;
				readyTog = false;
			}
		}

		if (timerTog) {
			timerTimer -= Time.deltaTime;

			if (timerTimer >= 0) {
				int mins = Mathf.FloorToInt (timerTimer / 60);
				int secs = Mathf.FloorToInt (timerTimer % 60);
				GameObject.Find ("Time").GetComponent<Text> ().text = "Remaining Time: " + string.Format ("{0:0}:{1:00}", mins, secs);
			} else {
				GameObject.Find ("Time").GetComponent<Text> ().text = "Times Up!";
				Destroy(GameObject.Find("Target(Clone)"));

				if (!CheckHighScore(score)) {
					PlayerPrefs.SetInt (selectedWeapon, score);
				}

				StartCoroutine (WaitForExit (3.0f));
				timerTog = false;
			}
		}
	}

	IEnumerator WaitForExit(float time) {
		yield return new WaitForSeconds(time);
		SceneManager.LoadScene (1);
	}

	bool CheckHighScore(int score) {
		if (PlayerPrefs.HasKey (selectedWeapon)) {
			if (score > PlayerPrefs.GetInt (selectedWeapon)) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}
}
