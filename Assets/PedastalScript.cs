using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PedastalScript : MonoBehaviour {

	public float damage;
	public bool destroyed;
	public Text rebuildingTextStatic;
	public Text rebuildingText;

	public int rebuildCounter;
	public int rebuildCounterMAX;
	public int deathCounter;
	
	// Use this for initialization
	void Start () {
		destroyed = false;
		damage = 0f;
		GetComponentInChildren<SpriteRenderer>().sortingOrder = (int)-transform.position.z;
		GetComponent<Canvas>().sortingOrder = (int)-transform.position.z + 1;
		rebuildCounter = 0;
		rebuildCounterMAX = 0;
		deathCounter = 0;
		rebuildingTextStatic.enabled = false;
		rebuildingText.enabled = false;
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
		if (damage > 1f && !this.destroyed) {
			this.destroyed = true;
			GameObject gm = GameObject.Find("GAME MANAGER");
			gm.GetComponent<GameManagerScript>().AdvanceLevel();
			GetComponentInChildren<Animator> ().SetBool("Destroyed", true);
			deathCounter += 1;
			rebuildCounterMAX = Random.Range(15, 30) * deathCounter;
			return;
		}

		GetComponentInChildren<Animator> ().SetFloat ("Damage", damage);
	}

	public void HealDamage(float t){
		damage -= t;
		damage = Mathf.Max (damage, 0f);
		Debug.Log ("healing damage" + t);
		GetComponentInChildren<Animator> ().SetFloat ("Damage", damage);
	}

	public void RebuildCity(){
		if(destroyed && rebuildCounter == 0){
			rebuildingTextStatic.enabled = true;
			rebuildingText.enabled = true;
			rebuildCounter = rebuildCounterMAX;
		}
		rebuildCounter -= 1;
		rebuildCounter = Mathf.Max (rebuildCounter, 0);
		rebuildingText.text = ""+rebuildCounter;
		if(rebuildCounter == 0){
			damage = 0f;
			GetComponentInChildren<Animator> ().SetBool("Destroyed", false);
			GetComponentInChildren<Animator> ().SetFloat ("Damage", damage);
			rebuildingTextStatic.enabled = false;
			rebuildingText.enabled = false;
			destroyed = false;

			//play sound for rebuilt city
		}

	}
}
