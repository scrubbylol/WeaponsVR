using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadCheck : MonoBehaviour {

	private HandController handControl;

	public GameObject magazine;

	public int bullets;

	// Use this for initialization
	void Start () {
		handControl = GameObject.Find ("Manager").GetComponent<HandController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void ClipRelease(int weapon) {
		if (weapon == 1) {
			if (!GetComponent<MeshCollider> ().bounds.Intersects (GameObject.Find ("Gun1_Load").GetComponent<BoxCollider> ().bounds)) {
				var blt = (GameObject)Instantiate (magazine, GameObject.Find ("Holster").transform.position, magazine.transform.rotation, GameObject.Find ("Holster").transform); 
				blt.transform.localPosition = Vector3.zero;
				blt.transform.localScale = new Vector3 (0.015f, 0.015f, 0.015f);
				blt.GetComponent<VRTK.VRTK_InteractableObject> ().isGrabbable = true;
				blt.GetComponent<MeshCollider> ().enabled = true;
				blt.GetComponent<ReloadCheck> ().bullets = 12;

				if (blt.GetComponent<Rigidbody> () == null) {
					blt.AddComponent<Rigidbody> ().isKinematic = true;
				} else if (blt.GetComponent<Rigidbody> () != null && !blt.GetComponent<Rigidbody> ().isKinematic) {
					blt.GetComponent<Rigidbody> ().isKinematic = true;
				}

				gameObject.transform.SetParent (null);
				GetComponent<VRTK.VRTK_InteractableObject> ().isKinematic = false;
			} else {
				gameObject.transform.SetParent (GameObject.Find ("Gun1").transform);
				gameObject.name = "Gun1_Magazine";
				transform.localPosition = new Vector3 (-0.28529f, -49.6614f, -59.21256f);
				transform.localEulerAngles = new Vector3 (0, 0, 0);
				GetComponent<MeshCollider> ().enabled = false;
				Destroy (GetComponent<Rigidbody> ());

				GameObject.Find ("Gun1").GetComponent<GunStatus> ().hasMag = true;
				handControl.audSource.PlayOneShot (handControl.pmClipInSound);
			}
		}
		else if (weapon == 2) {
			if (!GetComponent<MeshCollider> ().bounds.Intersects (GameObject.Find ("Gun2_Load").GetComponent<BoxCollider> ().bounds)) {
				var blt = (GameObject)Instantiate (magazine, GameObject.Find ("Holster").transform.position, magazine.transform.rotation, GameObject.Find ("Holster").transform); 
				blt.transform.localPosition = Vector3.zero;
				blt.transform.localScale = new Vector3 (0.75f, 0.75f, 0.75f);
				blt.GetComponent<Rigidbody> ().isKinematic = true;
				blt.GetComponent<ReloadCheck> ().bullets = 30;

				gameObject.transform.SetParent (null);
				GetComponent<VRTK.VRTK_InteractableObject> ().isKinematic = false;
			} else {
				gameObject.transform.SetParent (GameObject.Find ("Gun2").transform);
				transform.localPosition = new Vector3 (0, -0.04166f, 0.1096f);
				transform.localEulerAngles = new Vector3 (-90, 0, 0);
				Destroy (GetComponent<Rigidbody> ());

				GameObject.Find ("Gun2").GetComponent<GunStatus> ().hasMag = true;
				handControl.audSource.PlayOneShot (handControl.m4a1ClipInSound);
			}
		}
	}

	public void ClipGrab(int weapon) {
		if (weapon == 1  && !GameObject.Find("Gun1").GetComponent<GunStatus>().hasMag) {
			GetComponent<Rigidbody> ().isKinematic = false;
		}
		else if (weapon == 2  && GameObject.Find("Gun2").GetComponent<GunStatus>().hasMag) {
			handControl.audSource.PlayOneShot (handControl.m4a1ClipOutSound);
			GameObject.Find ("Gun2").GetComponent<GunStatus> ().hasMag = false;
		}
	}
}
