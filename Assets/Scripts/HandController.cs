using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandController : MonoBehaviour {
	public AudioSource audSource;

	public AudioClip pistol1Sound;
	public AudioClip m4a1Sound;
	public AudioClip m4a1ClipOutSound;
	public AudioClip m4a1ClipInSound;
	public AudioClip pmClipOutSound;
	public AudioClip pmClipInSound;
	public AudioClip pistolEmptySound;
	public AudioClip rifleEmptySound;
	public AudioClip pmDrawSound;
	public AudioClip m4a1DrawSound;
	public AudioClip objectGrabSound;
	public AudioClip knifeDeploySound;
	public AudioClip laserChargeSound;
	public AudioClip laserShootSound;
	public AudioClip bowReleaseSound;
	public AudioClip dingSound;

	public GameObject bullet1Prefab;
	public GameObject shell1Prefab;
	public GameObject shell2Prefab;
	public GameObject arrowPrefab;
	public GameObject laserBeamPrefab;

	private bool holdM4a1Fire;
	private bool holdLaserFire;
	public bool bowPullString = true;
	public float m4a1Timer;
	public float laserTimer;
	public float m4a1FireRate;

	public bool laserCharged;

	public string currentWeapon;
	public Transform bowPivot1;

	private Vector3 bowStr1InitPos;
	private Vector3 bowStr2InitPos;

	private Vector3 bowStr1InitRot;
	private Vector3 bowStr2InitRot;

	private Vector3 bowStr1PullPos;
	private Vector3 bowStr2PullPos;

	private Vector3 bowStr1PullRot;
	private Vector3 bowStr2PullRot;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (holdM4a1Fire) {
			m4a1Timer += Time.deltaTime;
			if (m4a1Timer > m4a1FireRate) {
				if (GameObject.Find ("Gun2").GetComponent<GunStatus> ().hasMag) {
					if (GameObject.Find ("Gun2").GetComponentInChildren<ReloadCheck> ().bullets > 0) {
						var blt = (GameObject)Instantiate (bullet1Prefab, GameObject.Find ("Gun2_Tip").transform.position, GameObject.Find ("Gun2_Tip").transform.rotation);
						blt.GetComponent<Rigidbody> ().AddRelativeForce (Vector3.up * 6000f);
						blt.transform.eulerAngles = GameObject.Find ("Gun2_Tip").transform.eulerAngles;
						//blt.transform.eulerAngles = GameObject.Find ("Gun2").transform.localEulerAngles;
						Destroy(blt, 4.0f);

						var shl = (GameObject)Instantiate (shell2Prefab, GameObject.Find ("Gun2_Receiver").transform.position, shell2Prefab.transform.rotation);
						shl.GetComponent<Rigidbody> ().AddForce ((Vector3.right * 55f) + (Vector3.up * 35f));
						shl.transform.rotation = Random.rotation;
						Destroy (shl, 60f);

						//GameObject.Find ("Gun2").GetComponent<Animator> ().SetBool ("Fire", true);
						GameObject.Find ("Gun2_Muzzle").GetComponent<ParticleSystem> ().Play ();
						GameObject.Find ("Gun2_Smoke").GetComponent<ParticleSystem> ().Play ();

						audSource.PlayOneShot (m4a1Sound, 0.25f);
						GameObject.Find ("Gun2").GetComponentInChildren<ReloadCheck> ().bullets -= 1;
					} else {
						audSource.PlayOneShot (rifleEmptySound);
					}
				} else {
					audSource.PlayOneShot (rifleEmptySound);
				}

				m4a1Timer = 0;
			}
		}

		if (bowPullString) {
			var str1 = GameObject.Find ("Bow_StringPiv1");
			var str2 = GameObject.Find ("Bow_StringPiv2");
			var mid = GameObject.Find ("RightController");
			var bow = GameObject.Find ("Bow");

			//Debug.Log (Mathf.Abs (mid.transform.position.z - bow.transform.position.z));
			float dis = Mathf.Abs (mid.transform.position.z - bow.transform.position.z) * 75;
			float scl = (Mathf.Abs (mid.transform.position.z - bow.transform.position.z) / 2.05f) + 1;

			str1.transform.localEulerAngles = new Vector3 (str1.transform.localEulerAngles.x, str1.transform.localEulerAngles.y, (dis));
			str2.transform.localEulerAngles = new Vector3 (str2.transform.localEulerAngles.x, str2.transform.localEulerAngles.y, -(dis));

			str1.transform.localScale = new Vector3 (scl, str1.transform.localScale.y, str1.transform.localScale.z);
			str2.transform.localScale = new Vector3 (scl, str2.transform.localScale.y, str2.transform.localScale.z);

			// Arrow
			GameObject.Find("Bow_Arrow").transform.localPosition = new Vector3(GameObject.Find("Bow_Arrow").transform.localPosition.x, (dis/3)-11, GameObject.Find("Bow_Arrow").transform.localPosition.z);
		}

		if (holdLaserFire) {
			laserTimer += Time.deltaTime;
			if (laserTimer >= 0.1f && GameObject.Find ("Gun3_Charge").transform.localScale.x < 0.475f) {
				GameObject.Find ("Gun3_Charge").transform.localScale = new Vector3 (GameObject.Find ("Gun3_Charge").transform.localScale.x + 0.03f, GameObject.Find ("Gun3_Charge").transform.localScale.y + 0.03f, GameObject.Find ("Gun3_Charge").transform.localScale.z + 0.03f);
				laserTimer = 0;
			} else if (GameObject.Find ("Gun3_Charge").transform.localScale.x > 0.4755f) {
				laserCharged = true;
				var chargeCol = GameObject.Find ("Gun3_Charge").GetComponent<ParticleSystem> ().main;
				chargeCol.startColor = Color.red;
			}
		}
	}

	public void PullBow() {
		bowPullString = true;
		var arrow = (GameObject)Instantiate (arrowPrefab, GameObject.Find ("Bow_Mid").transform);
		arrow.transform.localPosition = new Vector3 (0.0276f, -11.68f, 1.25f);
		arrow.transform.localEulerAngles = new Vector3 (-5, 0, 0);
		arrow.name = "Bow_Arrow";
		GameObject.Find ("Bow_String").GetComponent<Rigidbody> ().isKinematic = true;
	}

	public void ReleaseBow() {
		bowPullString = false;

		GameObject.Find ("Bow_String").transform.localPosition = new Vector3 (0.03f, 3.42f, 0.13f);

		var str1 = GameObject.Find ("Bow_StringPiv1");
		var str2 = GameObject.Find ("Bow_StringPiv2");

		str1.transform.localEulerAngles = Vector3.zero;
		str2.transform.localEulerAngles = Vector3.zero;
		str1.transform.localScale = Vector3.one;
		str2.transform.localScale = Vector3.one;

		// Arrow
		var arrow = GameObject.Find("Bow_Arrow");
		arrow.GetComponent<Rigidbody> ().isKinematic = false;
		arrow.GetComponent<MeshCollider> ().isTrigger = false;
		arrow.GetComponent<Rigidbody> ().AddRelativeForce (Vector3.down*30, ForceMode.Impulse);
		arrow.transform.SetParent (null);
		arrow.name = "Bow_Arrow(Old)";

		audSource.PlayOneShot (bowReleaseSound);
	}

	public void FireGun(int gun) {
		if (gun == 1 && GameObject.Find("Gun1").GetComponent<GunStatus> ().hasMag) {
			if (GameObject.Find("Gun1").GetComponentInChildren<ReloadCheck> ().bullets > 0) {
				var blt = (GameObject)Instantiate (bullet1Prefab, GameObject.Find ("Gun1_Tip").transform.position, GameObject.Find ("Gun1_Tip").transform.rotation);
				blt.GetComponent<Rigidbody> ().AddRelativeForce (Vector3.forward * 6000f);
				blt.transform.eulerAngles = GameObject.Find ("Gun1").transform.eulerAngles;
				Destroy (blt, 4.0f);

				var shl = (GameObject)Instantiate (shell1Prefab, GameObject.Find ("Gun1_Receiver").transform.position, shell1Prefab.transform.rotation);
				shl.GetComponent<Rigidbody> ().AddRelativeForce ((Vector3.right * 25f) + (Vector3.up * 35f));
				shl.transform.rotation = Random.rotation;
				Destroy (shl, 60f);

				GameObject.Find ("Gun1").GetComponent<Animator> ().SetBool ("Fire", true);
				GameObject.Find ("Gun1_Muzzle").GetComponent<ParticleSystem> ().Play ();
				GameObject.Find ("Gun1_Smoke").GetComponent<ParticleSystem> ().Play ();

				audSource.PlayOneShot (pistol1Sound, 0.25f);
				GameObject.Find ("Gun1").GetComponentInChildren<ReloadCheck> ().bullets -= 1;
			} else {
				audSource.PlayOneShot (pistolEmptySound);
			}
		} else if (gun == 1 && !GameObject.Find("Gun1").GetComponent<GunStatus> ().hasMag) {
			audSource.PlayOneShot (pistolEmptySound);
		} else if (gun == 2) {
			holdM4a1Fire = true;
		} else if (gun == 6) {
			holdLaserFire = true;
			GameObject.Find ("Gun3_ChargeLight").GetComponent<ParticleSystem> ().Play ();
			audSource.PlayOneShot (laserChargeSound);
		}
	}

	public void StopGunFire(int gun) {
		if (gun == 2) {
			holdM4a1Fire = false;
		}
		else if (gun == 6) {
			if (laserCharged) {
				var beam = (GameObject)Instantiate (laserBeamPrefab, GameObject.Find ("Gun3_Tip").transform.position, GameObject.Find ("Gun3_Tip").transform.rotation);
				audSource.PlayOneShot (laserShootSound,0.5f);
				laserCharged = false;
			} else {
				if (audSource.isPlaying) {
					audSource.Stop ();
				}
			}



			GameObject.Find ("Gun3_ChargeLight").GetComponent<ParticleSystem> ().Stop ();
			holdLaserFire = false;
			GameObject.Find ("Gun3_Charge").transform.localScale = Vector3.zero;

			var chargeCol = GameObject.Find ("Gun3_Charge").GetComponent<ParticleSystem> ().main;
			chargeCol.startColor = Color.white;
		}
	}

	public void WeaponEquipped(int weapon) {
		if (weapon == 1) {
			GameObject.Find ("Weapon_Equipped").GetComponent<Text> ().text = "PM-40 Equipped!";
			GameObject.Find ("Weapon_Equipped").GetComponent<Animator> ().SetBool ("Equipped", true);
			currentWeapon = "Gun1";

			if (GameObject.Find ("Gun1").GetComponent<VRTK.VRTK_InteractableObject> ().GetGrabbingObject ().Equals (GameObject.Find ("RightController"))) {
				GameObject.Find ("Gun1").GetComponent<GunStatus> ().handGrabbed = "Right";
			} else {
				GameObject.Find ("Gun1").GetComponent<GunStatus> ().handGrabbed = "Left";
			}

			audSource.PlayOneShot (pmDrawSound);
		}
		else if (weapon == 2) {
			GameObject.Find ("Weapon_Equipped").GetComponent<Text> ().text = "M4A1 Equipped!";
			GameObject.Find ("Weapon_Equipped").GetComponent<Animator> ().SetBool ("Equipped", true);
			currentWeapon = "Gun2";

			audSource.PlayOneShot (m4a1DrawSound);
		}
		else if (weapon == 3) {
			GameObject.Find ("Weapon_Equipped").GetComponent<Text> ().text = "Ball Equipped!";
			GameObject.Find ("Weapon_Equipped").GetComponent<Animator> ().SetBool ("Equipped", true);
			currentWeapon = "Ball";

			audSource.PlayOneShot (objectGrabSound);
		}
		else if (weapon == 4) {
			GameObject.Find ("Weapon_Equipped").GetComponent<Text> ().text = "Knife Equipped!";
			GameObject.Find ("Weapon_Equipped").GetComponent<Animator> ().SetBool ("Equipped", true);
			currentWeapon = "Knife";

			audSource.PlayOneShot (knifeDeploySound);
		}
		else if (weapon == 5) {
			GameObject.Find ("Weapon_Equipped").GetComponent<Text> ().text = "Bow Equipped!";
			GameObject.Find ("Weapon_Equipped").GetComponent<Animator> ().SetBool ("Equipped", true);
			currentWeapon = "Bow";

			GameObject.Find ("Bow_String").GetComponent<VRTK.VRTK_InteractableObject> ().isGrabbable = true;
			audSource.PlayOneShot (objectGrabSound);
		}
		else if (weapon == 6) {
			GameObject.Find ("Weapon_Equipped").GetComponent<Text> ().text = "Laser Gun Equipped!";
			GameObject.Find ("Weapon_Equipped").GetComponent<Animator> ().SetBool ("Equipped", true);
			currentWeapon = "Gun3";

			audSource.PlayOneShot (pmDrawSound);
		}

	}

	public void WeaponUnequipped(int weapon) {
		if (weapon == 1) {
			GameObject.Find ("Weapon_Equipped").GetComponent<Text> ().text = "PM-40 Unequipped!";
			GameObject.Find ("Weapon_Equipped").GetComponent<Animator> ().SetBool ("Equipped", true);
			GameObject.Find ("Gun1").GetComponent<GunStatus> ().handGrabbed = "";
		}
		else if (weapon == 2) {
			GameObject.Find ("Weapon_Equipped").GetComponent<Text> ().text = "M4A1 Unequipped!";
			GameObject.Find ("Weapon_Equipped").GetComponent<Animator> ().SetBool ("Equipped", true);
		}
		else if (weapon == 3) {
			GameObject.Find ("Weapon_Equipped").GetComponent<Text> ().text = "Ball Unequipped!";
			GameObject.Find ("Weapon_Equipped").GetComponent<Animator> ().SetBool ("Equipped", true);
		}
		else if (weapon == 4) {
			GameObject.Find ("Weapon_Equipped").GetComponent<Text> ().text = "Knife Unequipped!";
			GameObject.Find ("Weapon_Equipped").GetComponent<Animator> ().SetBool ("Equipped", true);
		}
		else if (weapon == 5) {
			GameObject.Find ("Weapon_Equipped").GetComponent<Text> ().text = "Bow Unequipped!";
			GameObject.Find ("Weapon_Equipped").GetComponent<Animator> ().SetBool ("Equipped", true);

			GameObject.Find ("Bow_String").transform.localPosition = new Vector3 (0.03f, 3.42f, 0.13f);
			GameObject.Find ("Bow_String").GetComponent<VRTK.VRTK_InteractableObject> ().isGrabbable = false;
		}
		else if (weapon == 6) {
			GameObject.Find ("Weapon_Equipped").GetComponent<Text> ().text = "Laser Gun Unequipped!";
			GameObject.Find ("Weapon_Equipped").GetComponent<Animator> ().SetBool ("Equipped", true);
		}

		currentWeapon = "";
	}
}
