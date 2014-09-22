using UnityEngine;
using System.Collections;

public class DustScript : MonoBehaviour {
	public float dustTimer = .75f;

	// Use this for initialization
	void Start () {
		//GetComponent<Animator>().Play ("dustcloud");
	}
	
	// Update is called once per frame
	void Update () {
		dustTimer -= Time.deltaTime;

		if (dustTimer < 0f) {
			Destroy(this.gameObject);
				}
	}
}
