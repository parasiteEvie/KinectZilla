using UnityEngine;
using System.Collections;

public class PedastalScript : MonoBehaviour {

	float damage;
	bool destroyed;

	// Use this for initialization
	void Start () {
		destroyed = false;
		damage = 0f;
	}

	//public void OnCollisionStay(
	// Update is called once per frame
	void Update () {
		if (destroyed) {
			renderer.material.color = Color.black;
			foreach (Transform t in this.transform) {
				GameObject citystuff = t.gameObject;
				citystuff.renderer.material.color = Color.black;
				if(citystuff.particleSystem != null){
					citystuff.particleSystem.emissionRate = 25f;
				}
			}
			return;
		}

		renderer.material.color = Color.Lerp (Color.white, Color.red, damage);

		foreach (Transform t in this.transform) {
			GameObject citystuff = t.gameObject;
			citystuff.renderer.material.color = Color.Lerp (Color.white, Color.red, damage);
			if(citystuff.particleSystem != null){
				citystuff.particleSystem.emissionRate = damage * 10f;
			}
		}
	}

	public void SetDamage(float t){
		damage += t;
		if (damage > 1f) {
			destroyed = true;
		}
	}

	public void HealDamage(float t){
		damage -= t;
		damage = Mathf.Max (damage, 0f);
		Debug.Log ("healing damage" + t);
	}
}
