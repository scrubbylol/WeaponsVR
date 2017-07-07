using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.name.Equals("Ball") || col.gameObject.name.Equals("Knife")) {
			col.gameObject.transform.position = GameObject.Find ("Table_Top").transform.position;
			col.gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
	}
}
