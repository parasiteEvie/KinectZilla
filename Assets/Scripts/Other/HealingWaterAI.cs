using UnityEngine;
using System.Collections;

public class HealingWaterAI : MonoBehaviour {
	public float UVLength = 255f;
	public float UVTotal = 22f;

	public GameObject SplashSprite;
	public GameObject DripSprite;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().material.SetFloat ("_UVTotal", UVTotal);
	}
	
	// Update is called once per frame
	void OnEnable(){
		DripSprite.SetActive (true);
		SplashSprite.SetActive (false);
		this.GetComponent<SpriteRenderer> ().enabled = false;
	}

	void OnTriggerStay(Collider c) {
		this.GetComponent<SpriteRenderer> ().enabled = true;
			if (c.collider.name == "City") {
				RaycastHit hit;
				Vector3 fwd = c.transform.position - transform.parent.position;
			Physics.Raycast(transform.position, fwd, out hit);
			UVLength = this.gameObject.transform.parent.position.x - hit.point.x;

			UVLength = Mathf.Abs(UVLength);
							Debug.Log ("distance: "+UVLength);
					GetComponent<SpriteRenderer> ().material.SetFloat ("_UVLength", UVLength);
					Debug.Log ("UVLength" + GetComponent<SpriteRenderer> ().material.GetFloat ("_UVLength"));
				SplashSprite.SetActive (true);
				SplashSprite.transform.position =new Vector3(hit.point.x, SplashSprite.transform.position.y, SplashSprite.transform.position.z);
				DripSprite.SetActive (false);
			c.GetComponent<PedastalScript>().HealDamage(Time.deltaTime / 5f);
			}


	}
	void OnTriggerExit(Collider c) {
		if (c.collider.name == "City") {
			RaycastHit hit;
			Vector3 fwd = c.transform.position - transform.parent.position;
			Physics.Raycast(transform.position, fwd, out hit);
			UVLength = this.gameObject.transform.parent.position.x - hit.point.x;
			
			UVLength = Mathf.Abs(UVLength);
			Debug.Log ("distance: "+UVLength);
			GetComponent<SpriteRenderer> ().material.SetFloat ("_UVLength", UVLength);
			Debug.Log ("UVLength" + GetComponent<SpriteRenderer> ().material.GetFloat ("_UVLength"));
			
		}
		SplashSprite.SetActive (true);
		DripSprite.SetActive (false);
		
	}

}
