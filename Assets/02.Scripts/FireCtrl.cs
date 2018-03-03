using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour {

	public GameObject bullet;

	public Transform firePos;
	public AudioClip fireSfx;
	public MeshRenderer muzzleFlash;

	private AudioSource source = null;
	// Use this for initialization
	void Start () {
		
		source = GetComponent<AudioSource> ();

		muzzleFlash.enabled = false;
	}
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Fire ();
		}
	}
	void CreateBullet(){
		Instantiate (bullet, firePos.position, firePos.rotation);
	}
	void Fire(){
		CreateBullet ();
		source.PlayOneShot (fireSfx, 0.9f);
		StartCoroutine (this.ShowMuzzleFlash());
	}
	
	// Update is called once per frame

	IEnumerator ShowMuzzleFlash(){
		
		float scale = Random.Range (1.0f, 2.0f);
		muzzleFlash.transform.localScale = Vector3.one * scale;

		Quaternion rot = Quaternion.Euler (0, 0, Random.Range (0, 360));
		muzzleFlash.transform.localRotation = rot;

		muzzleFlash.enabled = true;
		yield return new WaitForSeconds (Random.Range (0.05f, 0.3f));

		muzzleFlash.enabled = false;
	}
}
