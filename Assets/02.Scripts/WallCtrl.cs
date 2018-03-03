using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCtrl : MonoBehaviour {

	public GameObject sparkEffeck;

	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.tag == "BULLET") {
			GameObject spark = (GameObject)Instantiate (sparkEffeck, coll.transform.position, Quaternion.identity);

			Destroy (spark, spark.GetComponent<ParticleSystem> ().duration + 0.2f);

			Destroy (coll.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
