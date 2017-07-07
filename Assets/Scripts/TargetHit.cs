using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetHit : MonoBehaviour {

	public GameObject targetPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col) {
		int x = Random.Range (-10, -4);
		int y = Random.Range (1, 7);
		int z = Random.Range (-4, 10);
		Vector3 loc = new Vector3 (x, y, z);

		transform.localPosition = loc;

		GameObject.Find ("Manager").GetComponent<HandController> ().audSource.PlayOneShot (GameObject.Find ("Manager").GetComponent<HandController> ().dingSound);
		GameObject.Find ("LevelControl").GetComponent<LevelControl> ().score += 1;
		GameObject.Find ("Score").GetComponent<Text> ().text = "Score: " + GameObject.Find ("LevelControl").GetComponent<LevelControl> ().score;
	}
}
