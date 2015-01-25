using UnityEngine;
using System.Collections;

public class PedastalScript : MonoBehaviour {

	public float damage;
	public bool destroyed;

	// Use this for initialization
	void Start () {
		destroyed = false;
		damage = 0f;
	}

	//public void OnCollisionStay(
	// Update is called once per frame
	void Update () {
		if (destroyed) {
			foreach (Transform t in this.transform) {
				GameObject citystuff = t.gameObject;
				if(citystuff.particleSystem != null){
					citystuff.particleSystem.emissionRate = 25f;
				}
			}
			return;
		}

		foreach (Transform t in this.transform) {
			GameObject citystuff = t.gameObject;
			if(citystuff.particleSystem != null){
				citystuff.particleSystem.emissionRate = damage * 10f;
			}
		}

		GetComponentInChildren<SpriteRenderer> ().sortingOrder = -Mathf.CeilToInt (transform.position.z);
	}

	public void SetDamage(float t){
		damage += t;
		if (damage > 1f) {
			destroyed = true;
			GameObject gm = GameObject.Find("GAME MANAGER");
			gm.GetComponent<GameManagerScript>().AdvanceLevel();
		}

		Debug.Log ("doing damage over here");
		GetComponentInChildren<Animator> ().SetFloat ("Damage", damage);
	}

	public void HealDamage(float t){
		damage -= t;
		damage = Mathf.Max (damage, 0f);
		Debug.Log ("healing damage" + t);
		GetComponentInChildren<Animator> ().SetFloat ("Damage", damage);
	}
}
